using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.DTO
{
    public class UserDetailsDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public ProfileUserDTO profile { get; set; }
    }
}
