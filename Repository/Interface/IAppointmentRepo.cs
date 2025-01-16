using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Models;
using HospitalApointmentManagementSystem.Services;

namespace HospitalApointmentManagementSystem.Repository.Interface
{
    public interface IAppointmentRepo
    {
        public Task<Availability> GetDoctorAvailability(Availability availability, string DoctorId);
        public Task<GenResponse<bool>> BookAppointment(BookAppointmentDto bookAppointment);
    }
}
