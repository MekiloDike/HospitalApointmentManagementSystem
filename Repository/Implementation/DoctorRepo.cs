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

                var availability = entity.Availability.Select(x => new AvailabilityDto
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
            return new GenResponse<PaginatedResponse<List<GetDoctorDto>>> { Success = true, Message = "Successful", Body = new PaginatedResponse<List<GetDoctorDto>> { Records = getDoctorslist, TotalRecord = total} };


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

            var availability = entity.Availability.Select(x => new AvailabilityDto
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

    }
}