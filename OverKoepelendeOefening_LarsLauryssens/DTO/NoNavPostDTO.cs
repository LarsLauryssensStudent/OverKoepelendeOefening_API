using OverKoepelendeOefening_LarsLauryssens.Models;

namespace OverKoepelendeOefening_LarsLauryssens.DTO
{
    public class NoNavPostDTO : IPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
