using HospitalApointmentManagementSystem.DbConnection;
using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Enums;
using HospitalApointmentManagementSystem.Models;
using HospitalApointmentManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace HospitalApointmentManagementSystem.Repository.Implementation
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppDbContext _appDbContext;
        public AppointmentRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<GenResponse<bool>> BookAppointment(BookAppointmentDto bookAppointmentDto)
        {
            // check for availability of patient id
            // map all requurec field
            // add to database

            var patient = await _appDbContext.Patients.FirstOrDefaultAsync(x => x.Id == bookAppointmentDto.PatientId);
            if (patient == null)
            {
                return new GenResponse<bool> { Success = false, Message = "Patient does not exist", Body = false };
            }
            var doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.Id == bookAppointmentDto.DoctorId);
            if (doctor == null)
            {
                return new GenResponse<bool> { Success = false, Message = "doctor does not exist", Body = false };

            }            
                //create appointmnet
                var appointment = new Appointment
                {
                    AppointmentType = bookAppointmentDto.AppointmentType,
                    DoctorId = bookAppointmentDto.DoctorId,
                    PatientId = bookAppointmentDto.PatientId,
                    Duration = bookAppointmentDto.Duration,
                    ScheduleTime = bookAppointmentDto.ScheduledTime,
                    Status = Status.Scheduled,
                    MedicalHistory = null


                };
                await _appDbContext.Appointments.AddAsync(appointment);
                var isSaved = await _appDbContext.SaveChangesAsync() > 0;
                return new GenResponse<bool> { Success = isSaved, Message = "Appointment successfully booked", Body = isSaved };
             

           
        }

        public Task<Availability> GetDoctorAvailability(Availability availability, string DoctorId)
        {
            throw new NotImplementedException();
        }
    }
}