namespace HospitalApointmentManagementSystem.DTO
{
    public class GenResponse<T> 
    {
        public bool  Success { get; set; }
        public string? Message { get; set; }
        public T? Body { get; set; } 
    }
}
