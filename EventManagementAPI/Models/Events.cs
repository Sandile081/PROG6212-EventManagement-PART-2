using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
        public class Events
    {
        [Key]
        public int EventID { get; set;}
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public int OrganiserID { get; set; }
        public int CategoryID { get; set; }
        public int LocationID { get; set; }

    }


}
