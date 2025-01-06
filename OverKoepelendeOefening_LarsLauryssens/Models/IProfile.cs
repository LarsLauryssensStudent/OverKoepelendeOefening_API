namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public interface IProfile
    {
        Guid Id { get; set; }
        DateTime BirthDate { get; set; }
        string Biography { get; set; }
        string Location { get; set; }
        Guid UserId { get; set; }

    }
}
