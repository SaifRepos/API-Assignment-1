using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Assignment_I.Models
{
    [Table("Customers")]
    public class Customer

    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        public String? Location { get; set; }

        public String? Gender { get; set; }

    }
}
