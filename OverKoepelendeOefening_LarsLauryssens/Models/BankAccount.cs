using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class BankAccount
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Balance { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; } = default!;

    }
}
