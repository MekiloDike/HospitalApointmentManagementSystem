using HospitalApointmentManagementSystem.DTO;

namespace HospitalApointmentManagementSystem.Services.Interface
{
    public interface IPatientService
    {
        public Task<GenResponse<bool>> RegisterPatient(AddPatientDto patientDto);
    }
}
