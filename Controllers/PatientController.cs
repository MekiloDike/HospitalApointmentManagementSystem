using HospitalApointmentManagementSystem.DTO;
using HospitalApointmentManagementSystem.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApointmentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        [Route("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient(AddPatientDto addPatientDto)
        {
            var createPatient = await _patientService.RegisterPatient(addPatientDto);
            if (createPatient.Success )
            {
            return Ok(createPatient);
            }
                return BadRequest("patient is not successfully registered");
        }
    }
}
