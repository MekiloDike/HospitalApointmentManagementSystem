using HospitalApointmentManagementSystem.DbConnection;
using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Models;
using HospitalApointmentManagementSystem.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace HospitalApointmentManagementSystem.Repository.Implementation
{
    public class PatientRepo : IPatientRepo
    {
        public readonly AppDbContext _appDbContext;
        public PatientRepo(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;
        }

        public  async Task<GenResponse<bool>> RegisterPatient(AddPatientDto patientDto)
        {
            // check if email already exist
            var patientEntity = await _appDbContext.Patients.FirstOrDefaultAsync(x => x.Email == patientDto.Email);
            if (patientEntity != null )
            {
                throw new Exception("Email already exist");
            }

            var patient = new Patient
            {
                Name = patientDto.Name,
                Sex = patientDto.Sex,
                Address = patientDto.Address,
                Age = patientDto.Age,
                PhoneNumber = patientDto.PhoneNumber,
                MedicalDiagonisedHistory = patientDto.MedicalDiagonisedHistory,
                Email = patientDto.Email,
            };
           await _appDbContext.AddAsync( patient );
            var isSaved = _appDbContext.SaveChanges() > 0;
            return new GenResponse<bool> { Success = isSaved,Message = "patient successfully registered",Body = isSaved };
        }
    }
}
