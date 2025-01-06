using System.ComponentModel.DataAnnotations;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.DTO
{
    public class ProfileUserDTO : IProfile
    {
        [Key]
        public Guid Id {  get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Biography { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
