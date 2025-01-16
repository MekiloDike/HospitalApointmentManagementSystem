using HospitalApointmentManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;

namespace HospitalApointmentManagementSystem.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public Gender Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string MedicalDiagonisedHistory { get; set; }

        public List<Appointment> Appointment { get; set; } = new List<Appointment>();
    }
}
