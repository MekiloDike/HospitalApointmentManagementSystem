using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApointmentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Route("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] SearchOptions searchOptions )
        {
            var allDoctors = await _doctorService.GetAllDoctors(searchOptions);
            
            if(allDoctors.Success)
            {
                if (allDoctors.Body == null)
                {
                    return NotFound(allDoctors);
                }
                return Ok(allDoctors);

            }
            else
            { return BadRequest(allDoctors); }
        }

        [HttpGet]
        [Route("GetDoctorByID")]
        public async Task<IActionResult> GetDoctor(int id)
        {
            var doctorId = await _doctorService.GetDoctorById(id);
            if (doctorId.Success)
            {           
            return Ok(doctorId);
            }
            return BadRequest(doctorId);
        }

        [HttpPost]
        [Route("Register_Doctor")]
        public async Task<IActionResult> RegisterDoctor(AddDoctorDto addDoctorDto)
        {
            var registerDoctor = await _doctorService.RegisterDoctor(addDoctorDto);
            if(registerDoctor.Success)
            {
                return Ok(registerDoctor);
            }
            return BadRequest(registerDoctor);
        }
    }
}
