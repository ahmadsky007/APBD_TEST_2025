using namespace WebApplication1;
public interface IVisitsService
{
    Task<VisitResponseDto?> GetVisitByIdAsync(int id);
    Task<string?> AddVisitAsync(VisitRequestDto dto);
}