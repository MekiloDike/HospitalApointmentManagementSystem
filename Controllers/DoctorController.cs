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


        [HttpPost]
        [Route("Add_Doctor_Availability")]
        public async Task<IActionResult> AddDoctorAvailabilityById(AddAvailabilityDto availabilityDto)
        {
            var addDoctorAvailability = await _doctorService.AddDoctorAvailabilityById(availabilityDto);
            if (addDoctorAvailability.Success)
            {
                return Ok(addDoctorAvailability);
            }
            return BadRequest(addDoctorAvailability);
        }

        [HttpGet]
        [Route("GetDoctorAvailabilty")]
        public async Task<IActionResult> GetDoctorAvailabilityById(int doctorId)
        {
            var getDoctorAvailabilty = await _doctorService.GetDoctorAvailabilityById(doctorId);
            if (getDoctorAvailabilty.Success)
            {
                return Ok(getDoctorAvailabilty);
            }
            return BadRequest(getDoctorAvailabilty);
        }

        [HttpPost]
        [Route("UpdateDoctorAvailabilty")]
        public async Task<IActionResult> UpdateDoctorAvailabilty(UpdateAvailabilityDto availabilityDto, int availabiltyId)
        {
            var updateDoctor = await _doctorService.UPdateDoctorAvailability(availabilityDto, availabiltyId);
            if (updateDoctor.Success)
            {
                return Ok(updateDoctor);
            }
            return BadRequest(updateDoctor);
        }
    }
}
