public class Service
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = null!;
    public decimal ServiceFee { get; set; }

    public ICollection<VisitService> VisitServices { get; set; } = new List<VisitService>();
}