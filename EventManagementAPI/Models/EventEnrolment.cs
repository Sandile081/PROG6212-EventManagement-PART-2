using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class EventEnrolment
    {
        [Key]
        public int EventEnrolmentID { get; set; }
        public int EventID { get; set; }
        public int ParticipantID { get; set; }
        public string EnrolmentStatus { get; set; } = string.Empty;
    }

}
