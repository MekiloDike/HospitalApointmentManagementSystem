using HospitalApointmentManagementSystem.Enums;
using HospitalApointmentManagementSystem.Models;

namespace HospitalApointmentManagementSystem.DTO
{
    public class AddDoctorDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Gender Sex { get; set; }
        public int SpecializationId { get; set; }
       // public SpecializationDto SpecializationDto { get; set; }

    }
    public class PaginatedResponse<T>
    {
        public T? Records { get; set; }
        public int TotalRecord { get; set; }
    }
    public class GetDoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        //public SpecializationDto SpecializationDto { get; set; }
        public string SpecializationName { get; set; }
        public string Description { get; set; }
        public List<GetAvailabilityDto>? Availability { get; set; }
        public List<AppointmentDto>? Appointment { get; set; }
    }

    public class SpecializationDto
    {
        public string SpecializationName { get; set; }
        public string Description { get; set; }
    }

    public class AddAvailabilityDto
    {
        public int DoctorId { get; set; }        
        public DayOfWeek Day { get; set; }
        public DateTime DateTime { get; set; }
        public string Duration { get; set; }
        public bool IsAvailable { get; set; }

    }public class GetAvailabilityDto
    {
        public int DoctorId { get; set; }   
        public int AvalabilityId { get; set; }           
        public DayOfWeek Day { get; set; }
        public DateTime DateTime { get; set; }
        public string Duration { get; set; }
        public bool IsAvailable { get; set; }

    }public class UpdateAvailabilityDto
    {
        public DayOfWeek Day { get; set; }
        public DateTime DateTime { get; set; }
        public string Duration { get; set; }
        public bool IsAvailable { get; set; }
    }

    public class AppointmentDto
    {
        public DateTime ScheduleTime { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string Duration { get; set; }
        public AppointmentType AppointmentType { get; set; }

    }



}
