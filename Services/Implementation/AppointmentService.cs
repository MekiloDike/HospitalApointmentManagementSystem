using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Repository.Interface;
using HospitalApointmentManagementSystem.Services.Interface;

namespace HospitalApointmentManagementSystem.Services.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        public AppointmentService(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
        }

        public async Task<GenResponse<bool>> BookAppointment(BookAppointmentDto bookAppointment)
        {
            var Createappointment = await _appointmentRepo.BookAppointment(bookAppointment);
            return Createappointment;
        }
    }
}
