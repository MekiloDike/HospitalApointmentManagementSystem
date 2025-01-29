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
        public Task<GenResponse<bool>> AddDoctorAvailabilityById(AddAvailabilityDto availabilityDto);
        public Task<GenResponse<List<GetAvailabilityDto>>> GetDoctorAvailabilityById(int doctorId);
        public Task<GenResponse<bool>> UPdateDoctorAvailability(UpdateAvailabilityDto availabilityDto, int availabilityId);
    }
}
