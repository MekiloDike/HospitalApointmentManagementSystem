namespace HospitalApointmentManagementSystem.DTO
{
    public class SearchOptions
    {
        public string? keyWord { get; set; }
        public string? filterOption { get; set; }
        public int pageSize { get; set; } = 10;
        public int pageNumber { get; set; } = 1;
    }
}
