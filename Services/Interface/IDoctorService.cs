using HospitalApointmentManagementSystem.DTO;

namespace HospitalApointmentManagementSystem.Services.Interface
{
    public interface IDoctorService
    {
        public Task<GenResponse<bool>> RegisterDoctor(AddDoctorDto doctorDto);
        public Task<GenResponse<GetDoctorDto>> GetDoctorById(int doctorId);
        public Task<GenResponse<PaginatedResponse<List<GetDoctorDto>>>> GetAllDoctors(SearchOptions searchOption);
    }
}
