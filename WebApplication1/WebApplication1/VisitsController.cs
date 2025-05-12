using Microsoft.AspNetCore.Mvc;
using namespace WebApplication1;
[ApiController]
[Route("api/visits")]
public class VisitsController : ControllerBase
{
    private readonly IVisitsService _service;

    public VisitsController(IVisitsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetVisit(int id)
    {
        var visit = await _service.GetVisitByIdAsync(id);
        return visit == null ? NotFound() : Ok(visit);
    }

    [HttpPost]
    public async Task<IActionResult> AddVisit([FromBody] VisitRequestDto dto)
    {
        var error = await _service.AddVisitAsync(dto);
        return error switch
        {
            null => CreatedAtAction(nameof(GetVisit), new { id = dto.VisitId }, null),
            "Visit already exists." => Conflict(error),
            "Client not found." => NotFound(error),
            "Mechanic not found." => NotFound(error),
            "One or more services not found." => NotFound(error),
            _ => BadRequest(error)
        };
    }
}