using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Categories
    {
        [Key]
        public int CategoriesID { get; set; }
        public int Age { get; set; }
        public string categoryType { get; set; } = string.Empty;
        public int MinKM { get; set; }
        public int MaxKM { get; set; }

    }
}
