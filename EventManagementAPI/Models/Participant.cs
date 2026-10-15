using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Participant
    {
        [Key]
        public int ParticipantID { get; set; }
        public string Pname { get; set; } = string.Empty;
        public string Psurname { get; set; } = string.Empty;
        public string Pusername { get; set; } = string.Empty;
        public string Ppassword { get; set; } = string.Empty;
    }
}
