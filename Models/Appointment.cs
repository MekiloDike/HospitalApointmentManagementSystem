using HospitalApointmentManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime ScheduleTime { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Duration { get; set; }
        public MedicalReport? MedicalHistory { get; set; } = new MedicalReport();
        public AppointmentType AppointmentType { get; set; }
        

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
