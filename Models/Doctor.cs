using HospitalApointmentManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Photograph { get; set; }
        public Gender Sex { get; set; }
        public List<Availability> Availability { get; set; } = new List<Availability>();
        public List<Appointment> Appointment { get; set; } = new List<Appointment>();
        [ForeignKey("Specialization")]
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
    }
}
