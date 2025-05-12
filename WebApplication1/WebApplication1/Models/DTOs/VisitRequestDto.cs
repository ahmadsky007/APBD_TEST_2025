public class VisitRequestDto
{
    public int VisitId { get; set; }
    public int ClientId { get; set; }
    public string MechanicLicenceNumber { get; set; } = null!;
    public List<ServiceInputDto> Services { get; set; } = new();
}

public class ServiceInputDto
{
    public string ServiceName { get; set; } = null!;
    public decimal ServiceFee { get; set; }
}