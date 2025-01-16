using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApointmentManagementSystem.Models
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        public string SpecializationName { get; set; }
        public string Description { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
       
    }
}
