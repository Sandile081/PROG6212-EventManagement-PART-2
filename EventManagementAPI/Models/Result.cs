using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public string Time { get; set; }=string.Empty;
        public string FinishingPosition { get; set; } = string.Empty;
        public int OrganiserID { get; set; }
        public int ParticipantID { get; set; }
    }
}
