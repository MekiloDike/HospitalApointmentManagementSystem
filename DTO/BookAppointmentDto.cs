using HospitalApointmentManagementSystem.Enums;

namespace HospitalApointmentManagementSystem.DTO
{
    public class BookAppointmentDto
    {
        public int PatientId { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string SpecializationName { get; set;}
        public int DoctorId { get; set;}
        public DateTime ScheduledTime { get; set;}
        public string Duration { get; set;}
    }
}
