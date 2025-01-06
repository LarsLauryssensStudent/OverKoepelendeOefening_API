using System.ComponentModel.DataAnnotations;
using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.DTO
{
    public class AddUserWithProfileDTO : IUser
    {
        [Key]
        public Guid Id {  get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Username {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }

        //nav object voor de relatie te kunnen leggen
        public ProfileUserDTO profile { get; set; } = null!;
    }
}
