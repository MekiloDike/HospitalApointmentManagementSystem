using HospitalApointmentManagementSystem.Enums;
using HospitalApointmentManagementSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.DTO
{
    public class AddPatientDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? MedicalDiagonisedHistory { get; set; }
    }

    public class PatientUpdateDto
    {
        public string Address { get; set; }
    } 
    
    public class GetPatientAppointmentsDto
    {
        public string PatientName { get; set; }
        public string MedicalDiagonisedHistory { get; set; }
        public MedicalReport MedicalHistory { get; set; } = new MedicalReport();
        public List<AppointmentDetails> Appointments { get; set; }
    }
    public class AppointmentDetails
    {
        public DateTime ScheduleTime { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string Duration { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string DoctorName { get; set; }
    }

   
}
