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

        public async Task<GenResponse<bool>> AddDoctorAvailabilityById(AddAvailabilityDto availabilityDto)
        {
            var doctorAvailabilityId = await  _repo.AddDoctorAvailabilityById(availabilityDto);
            return doctorAvailabilityId;
        }

        public async Task<GenResponse<PaginatedResponse<List<GetDoctorDto>>>> GetAllDoctors(SearchOptions searchOption)
        {
            var allDoctors = await  _repo.GetAllDoctors(searchOption);
            return allDoctors;
        }

        public async Task<GenResponse<List<GetAvailabilityDto>>> GetDoctorAvailabilityById(int doctorId)
        {
            var doctorAvailabilityId = await _repo.GetDoctorAvailabilityById(doctorId);
            return doctorAvailabilityId;
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

        public async Task<GenResponse<bool>> UPdateDoctorAvailability(UpdateAvailabilityDto availabilityDto, int availabilityId)
        {
            var doctorAvailabiltyId = await _repo.UPdateDoctorAvailability(availabilityDto, availabilityId);
            return doctorAvailabiltyId;
        }
    }
}
