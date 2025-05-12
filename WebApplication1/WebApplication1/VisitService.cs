using namespace WebApplication1;
public class VisitsService : IVisitsService
{
    private readonly WorkshopContext _context;

    public VisitsService(WorkshopContext context)
    {
        _context = context;
    }

    public async Task<VisitResponseDto?> GetVisitByIdAsync(int id)
    {
        var visit = await _context.Visits
            .Include(v => v.Client)
            .Include(v => v.Mechanic)
            .Include(v => v.VisitServices)
                .ThenInclude(vs => vs.Service)
            .FirstOrDefaultAsync(v => v.VisitId == id);

        if (visit == null) return null;

        return new VisitResponseDto
        {
            Date = visit.Date,
            Client = new ClientDto
            {
                FirstName = visit.Client.FirstName,
                LastName = visit.Client.LastName,
                DateOfBirth = visit.Client.DateOfBirth
            },
            Mechanic = new MechanicDto
            {
                MechanicId = visit.MechanicId,
                LicenceNumber = visit.Mechanic.LicenceNumber
            },
            VisitServices = visit.VisitServices.Select(vs => new VisitServiceDto
            {
                Name = vs.Service.Name,
                ServiceFee = vs.Service.ServiceFee
            }).ToList()
        };
    }

    public async Task<string?> AddVisitAsync(VisitRequestDto dto)
    {
        if (await _context.Visits.AnyAsync(v => v.VisitId == dto.VisitId))
            return "Visit already exists.";

        var client = await _context.Clients.FindAsync(dto.ClientId);
        if (client == null) return "Client not found.";

        var mechanic = await _context.Mechanics
            .FirstOrDefaultAsync(m => m.LicenceNumber == dto.MechanicLicenceNumber);
        if (mechanic == null) return "Mechanic not found.";

        var serviceNames = dto.Services.Select(s => s.ServiceName).ToList();
        var services = await _context.Services
            .Where(s => serviceNames.Contains(s.Name))
            .ToListAsync();

        if (services.Count != dto.Services.Count)
            return "One or more services not found.";

        var visit = new Visit
        {
            VisitId = dto.VisitId,
            Date = DateTime.Now,
            ClientId = client.ClientId,
            MechanicId = mechanic.MechanicId,
            VisitServices = services.Select(s => new VisitService
            {
                ServiceId = s.ServiceId
            }).ToList()
        };

        _context.Visits.Add(visit);
        await _context.SaveChangesAsync();
        return null;
    }
}
