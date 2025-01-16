using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Repository.Interface;
using HospitalApointmentManagementSystem.Services.Interface;

namespace HospitalApointmentManagementSystem.Services.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;
        public PatientService(IPatientRepo patientRepo)
        { 
            _patientRepo = patientRepo;
        }
        public async Task<GenResponse<bool>> RegisterPatient(AddPatientDto patientDto)
        {

            var addPatient = await _patientRepo.RegisterPatient(patientDto);

            return addPatient;
        }

       
    }
}
