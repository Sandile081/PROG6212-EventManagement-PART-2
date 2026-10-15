using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Organiser
    {
        [Key]
        public int OrganiserID { get; set; }
        public string ORname { get; set; } = string.Empty;
        public string ORsurname { get; set; } = string.Empty;
        public string ORusername { get; set; } = string.Empty;
        public string ORpassword { get; set; } = string.Empty;
    }
}
