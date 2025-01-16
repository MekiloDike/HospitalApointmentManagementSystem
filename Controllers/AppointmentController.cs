using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApointmentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        
        [HttpPost]
        [Route("BookAppointment")]
        public async Task<IActionResult> BookAppointment(BookAppointmentDto bookAppointment)
        {
            var createAppointment = await _appointmentService.BookAppointment(bookAppointment);
            if (createAppointment.Success)
            {
            return Ok(createAppointment);
            }
                return BadRequest("appointment is not successfull");
        }
    }
}
