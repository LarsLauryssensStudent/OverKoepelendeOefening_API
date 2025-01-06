
using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Profile : IProfile
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(120)]
        public string Biography { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string Location { get; set; } = string.Empty;

        //Foreign key
        [Required]
        public Guid UserId { get; set; }
        //NavObject
        public User? user { get; set; };
    }
}
