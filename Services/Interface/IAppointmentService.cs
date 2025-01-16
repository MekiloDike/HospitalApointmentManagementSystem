using HospitalApointmentManagementSystem.DbConnection;
using HospitalApointmentManagementSystem.DTO;

namespace HospitalApointmentManagementSystem.Services.Interface
{
    public interface IAppointmentService
    {
        public Task<GenResponse<bool>> BookAppointment(BookAppointmentDto bookAppointment);
    }
}
