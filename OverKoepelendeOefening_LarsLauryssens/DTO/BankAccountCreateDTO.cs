using System.ComponentModel.DataAnnotations;

namespace OverKoepelendeOefening_LarsLauryssens.DTO
{
    public class BankAccountCreateDTO
    {

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public decimal Balance { get; set; }
    }
}
