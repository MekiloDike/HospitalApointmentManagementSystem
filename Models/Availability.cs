using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.Models
{
    public class Availability
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek Day{  get; set; }
        public DateTime DateTime { get; set; }
        public string Duration {  get; set; }
        public bool IsAvailable {  get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        
    }
}
