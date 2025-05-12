using namespace WebApplication1;
public class Visit
{
    public int VisitId { get; set; }
    public DateTime Date { get; set; }

    public int ClientId { get; set; }
    public Client Client { get; set; } = null!;

    public int MechanicId { get; set; }
    public Mechanic Mechanic { get; set; } = null!;

    public ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}