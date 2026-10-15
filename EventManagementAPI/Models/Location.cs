namespace EventManagementAPI.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Province { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
    }
}
