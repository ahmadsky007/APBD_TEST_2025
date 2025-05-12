public class Mechanic
{
    public int MechanicId { get; set; }
    public string LicenceNumber { get; set; } = null!;

    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
}