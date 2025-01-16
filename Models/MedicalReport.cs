using HospitalApointmentManagementSystem.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.Models
{
    public class MedicalReport
    {
        [Key]
        public int Id { get; set; }
        public AppointmentType AppointmentType { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public string NoteByDoctor { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }


    }
}
