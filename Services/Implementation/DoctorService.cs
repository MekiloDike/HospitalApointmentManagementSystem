using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Repository.Interface;
using HospitalApointmentManagementSystem.Services.Interface;

namespace HospitalApointmentManagementSystem.Services.Implementation
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepo _repo;
        public DoctorService( IDoctorRepo doctorRepo)
        {
            _repo = doctorRepo;
        }

        public async Task<GenResponse<PaginatedResponse<List<GetDoctorDto>>>> GetAllDoctors(SearchOptions searchOption)
        {
            var allDoctors = await  _repo.GetAllDoctors(searchOption);
            return allDoctors;
        }

        public async Task<GenResponse<GetDoctorDto>> GetDoctorById(int doctor)
        {
            var doctorId = await _repo.GetDoctorById(doctor);
            return doctorId;
        }

        public async Task<GenResponse<bool>> RegisterDoctor(AddDoctorDto doctorDto)
        {
            var registerDoctor = await _repo.AddDoctor(doctorDto);
            return registerDoctor;
        }
    }
}
