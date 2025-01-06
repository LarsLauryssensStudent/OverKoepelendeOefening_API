using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public class Post : IPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(28)]
        public string Title {  get; set; } = string.Empty;

        [Required]
        [MaxLength(120)]
        public string Description {  get; set; }

        //ForeignKey en NavObject
        public int CategoryId { get; set; }
        //Dit is een navobject dus hoeven we niet in te vullen, hoelang we het niet willen includen hoeven we dit niet in te vullen, anders kan je ook met dto's werken. hier laat ik dan het veld gewoon weg, en gebruik ik deze voor een post in plaats van dit te doen, maar dit doe ik in een verdere stap. ik heb wel een voorbeeld voorzien NoNavPostDTO
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
