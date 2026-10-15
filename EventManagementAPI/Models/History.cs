using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class History
    {
        [Key]
        public int HistoryID { get; set; }
        public int ResultID { get; set; }
    }
}
