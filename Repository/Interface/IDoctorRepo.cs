using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Models;
using System.Net;

namespace HospitalApointmentManagementSystem.Repository.Interface
{
    public interface IDoctorRepo
    {
        public Task<GenResponse<bool>> AddDoctor(AddDoctorDto doctorDto);
        public Task<GenResponse<GetDoctorDto>> GetDoctorById(int doctorId);
        public Task<GenResponse<PaginatedResponse<List<GetDoctorDto>>>> GetAllDoctors(SearchOptions searchOption);
        public void AddDoctorAvailabilityById();
        public void GetDoctorAvailabilityById();
        public void UPdateDoctorAvailability();
    }
}
