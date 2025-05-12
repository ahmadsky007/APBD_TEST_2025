public class Client
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}