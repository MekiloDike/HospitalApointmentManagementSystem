using HospitalApointmentManagementSystem.DbConnection;
using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Models;
using HospitalApointmentManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace HospitalApointmentManagementSystem.Repository.Implementation
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _appDbContext;
        private readonly string _prefix;
        public DoctorRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _prefix = "Dr.";
        }

        public async Task<GenResponse<PaginatedResponse<List<GetDoctorDto>>>> GetAllDoctors(SearchOptions searchOption)
        {
            //implement search by name, by specialization and email
            //add pagination
            //sort by name asc
            //filter by specialization, 
            var getDoctorslist = new List<GetDoctorDto>();

            var entities = _appDbContext.Doctors
                .Include(x => x.Specialization)
                .Include(x => x.Appointment)
                .Include(x => x.Availability)
                .AsQueryable();


            if (entities == null)
            {
                return new GenResponse<PaginatedResponse<List<GetDoctorDto>>> { Success = true, Message = "No result found" };
            }
            //search
            if (!string.IsNullOrEmpty(searchOption.keyWord))
            {
                entities = entities.Where(x => x.Name.Contains(searchOption.keyWord) || x.Email.Contains(searchOption.keyWord) || x.Specialization.SpecializationName.Contains(searchOption.keyWord));
            }
            //filtering
            if (!string.IsNullOrEmpty(searchOption.filterOption))
            {
                entities = entities.Where(x => x.Specialization.SpecializationName == searchOption.filterOption);
            }
            //pagination
            var total = entities.Count();
            entities = entities.Skip((searchOption.pageNumber - 1) * searchOption.pageSize).Take(searchOption.pageSize);

            foreach (var entity in entities)
            {
                var specialization = new SpecializationDto
                {
                    SpecializationName = entity.Specialization.SpecializationName,
                    Description = entity.Specialization.Description,
                };

                var appointment = entity.Appointment.Select(x => new AppointmentDto
                {
                    AppointmentType = x.AppointmentType,
                    Duration = x.Duration,
                    DateCreated = x.DateCreated,
                    ScheduleTime = x.ScheduleTime,
                    Status = x.Status,
                });

                var availability = entity.Availability.Select(x => new GetAvailabilityDto
                {
                    DateTime = x.DateTime,
                    Day = x.Day,
                    Duration = x.Duration,
                    IsAvailable = x.IsAvailable,
                }).ToList();
                //map models to Dto
                var doctorDto = new GetDoctorDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    PhoneNumber = entity.PhoneNumber,
                    Sex = entity.Sex.ToString(),
                    SpecializationName = entity.Specialization.SpecializationName,
                    Description = entity.Specialization.Description,
                    Appointment = appointment.ToList(),
                    Availability = availability,
                };
                getDoctorslist.Add(doctorDto);
            }
            //sorting
            getDoctorslist.OrderBy(x => x.Name).ToList();
            return new GenResponse<PaginatedResponse<List<GetDoctorDto>>> { Success = true, Message = "Successful", Body = new PaginatedResponse<List<GetDoctorDto>> { Records = getDoctorslist, TotalRecord = total } };


        }

        public async Task<GenResponse<GetDoctorDto>> GetDoctorById(int doctorId)
        {
            var entity = await _appDbContext.Doctors.Include(x => x.Specialization)
                .Include(x => x.Appointment)
                .Include(x => x.Availability)
                .FirstOrDefaultAsync(x => x.Id == doctorId);
            if (entity == null)
            {
                return new GenResponse<GetDoctorDto> { Success = false, Message = $"Doctor with id{doctorId} does not exist" };
            }

            /*var appointmentList = new List<AppointmentDto>();
            foreach (var a in entity.Appointment)
            {
                var appoint = new AppointmentDto
                {
                    ScheduleTime = a.ScheduleTime,
                    Status = a.Status,
                    AppointmentType = a.AppointmentType,
                    DateCreated = a.DateCreated,
                    Duration = a.Duration,
                };
                appointsList.Add(appoint);
            }*/
            var appointmentList = entity.Appointment.Select(x => new AppointmentDto
            {
                ScheduleTime = x.ScheduleTime,
                AppointmentType = x.AppointmentType,
                DateCreated = x.DateCreated,
                Status = x.Status,
                Duration = x.Duration,

            }).ToList();

            var availability = entity.Availability.Select(x => new GetAvailabilityDto
            {
                DateTime = x.DateTime,
                Day = x.Day,
                Duration = x.Duration,
                IsAvailable = x.IsAvailable,
            });

            var doctorDto = new GetDoctorDto
            {
                Name = entity.Name,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Sex = entity.Sex.ToString(),
                SpecializationName = entity.Specialization.SpecializationName,
                Description = entity.Specialization.Description,
                Appointment = appointmentList,
                Availability = availability.ToList(),
            };
            return new GenResponse<GetDoctorDto> { Success = true, Message = "Successful", Body = doctorDto };
        }

        public async Task<GenResponse<bool>> AddDoctor(AddDoctorDto doctorDto)
        {
            var result = new GenResponse<bool>();
            // check if email exist
            var doctorEntity = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.Email == doctorDto.Email);
            if (doctorEntity != null)
            {
                return new GenResponse<bool> { Success = false, Message = $"Doctor with email: {doctorDto.Email} already Exist", Body = false };
            }
            var specialization = await _appDbContext.Specializations.FirstOrDefaultAsync(s => s.Id == doctorDto.SpecializationId);
            if (specialization == null)
            {
                return new GenResponse<bool> { Success = false, Message = $"speciliazation does not exist", Body = false };
            }

            var doctor = new Doctor
            {
                Name = _prefix + doctorDto.Name,
                Sex = doctorDto.Sex,
                Email = doctorDto.Email,
                PhoneNumber = doctorDto.PhoneNumber,
                Specialization = specialization,
            };
            await _appDbContext.Doctors.AddAsync(doctor);
            var isSaved = await _appDbContext.SaveChangesAsync() > 0;
            return new GenResponse<bool> { Success = isSaved, Message = "Doctor successfully registered", Body = isSaved };
        }

        public async Task<GenResponse<bool>> AddDoctorAvailabilityById(AddAvailabilityDto availabilityDto)
        {
            // check if doctor exist
            // map the availablities details
            // add and save changes

            var doctorEntity = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == availabilityDto.DoctorId);
            if (doctorEntity == null)
            {
                return new GenResponse<bool> { Success = false, Message = "doctor with Id does not exist", Body = false };
            }

            var availabilty = new Availability
            {
                DateTime = availabilityDto.DateTime,
                Day = availabilityDto.Day,
                Duration = availabilityDto.Duration,
                IsAvailable = availabilityDto.IsAvailable,
                DoctorId = availabilityDto.DoctorId,
                Doctor = doctorEntity
            };

            await _appDbContext.Availabilities.AddAsync(availabilty);
            var isSaved = await _appDbContext.SaveChangesAsync() > 0;
            return new GenResponse<bool> { Body = isSaved, Success = isSaved, Message = "doctors availability successfully saved" };
        }





        public async Task<GenResponse<List<GetAvailabilityDto>>> GetDoctorAvailabilityById(int doctorId)
        {
            var doctorEntity = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);

            if (doctorEntity == null)
            {
                return new GenResponse<List<GetAvailabilityDto>> { Success = false, Message = "doctor does not exist" };
            }

            var availabilityEntity = await _appDbContext.Availabilities
                //.Include(x => x.Doctor)
                .Where(x => x.DoctorId == doctorId && x.IsAvailable).ToListAsync();

            if (availabilityEntity == null)
            {
                return new GenResponse<List<GetAvailabilityDto>> { Success = false, Message = "doctor does not exist" };
            }
            var availabilityList = new List<GetAvailabilityDto>();

            foreach (var availability in availabilityEntity)
            {

                var availabilityDto = new GetAvailabilityDto
                {
                    DateTime = availability.DateTime,
                    Day = availability.Day,
                    Duration = availability.Duration,
                    IsAvailable = availability.IsAvailable,
                    DoctorId = availability.DoctorId,
                    AvalabilityId = availability.Id
                };
                availabilityList.Add(availabilityDto);
            }

            /*var availabilityDto = await _appDbContext.Availabilities
           //.Include(x => x.Doctor)
           .Where(x => x.DoctorId == doctorId)
           .Select(a => new GetAvailabilityDto
           {
               DateTime = a.DateTime,
               Day = a.Day,
               Duration = a.Duration,
               IsAvailable = a.IsAvailable,
               DoctorId = a.DoctorId,
               AvalabilityId= a.Id
           })
           .ToListAsync();
*/
            return new GenResponse<List<GetAvailabilityDto>> { Body = availabilityList, Success = true };

        }



        public async Task<GenResponse<bool>> UPdateDoctorAvailability(UpdateAvailabilityDto availabilityDto, int availabilityId)
        {
            var entity = await _appDbContext.Availabilities.FirstOrDefaultAsync(x => x.Id == availabilityId);
            if (entity == null)
            {
                return new GenResponse<bool> { Success = false, Message = " availability not found", Body = false };
            }

            entity.Duration = availabilityDto.Duration;
            entity.DateTime = availabilityDto.DateTime;
            entity.Day = availabilityDto.Day;
            entity.IsAvailable = availabilityDto.IsAvailable;

            _appDbContext.Update(entity);
            var isSaved = await _appDbContext.SaveChangesAsync() > 0;
            return new GenResponse<bool> { Success = isSaved, Message = "doctor availability updated successfully", Body = isSaved };
        }

    }
}