namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public interface IPost
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }

    }
}
