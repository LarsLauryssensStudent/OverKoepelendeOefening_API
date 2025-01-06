using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class User : IUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //Navobject voor 1 op 1 relatie
        public Profile? Profile { get; set; } = null!;
    }
}
