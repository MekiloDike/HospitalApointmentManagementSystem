using HospitalApointmentManagementSystem.DTO;

namespace HospitalApointmentManagementSystem.Repository.Interface
{
    public interface IPatientRepo
    {
        //create patient
        public Task<GenResponse<bool>> RegisterPatient(AddPatientDto patientDto);
    }
}
