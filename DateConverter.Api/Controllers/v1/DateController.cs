using DateConverter.Domain.Queries.v1.Dates.Converter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DateConverter.Api.Controllers.v1;

[ApiController]
[Route("api/v1/dates")]
public sealed class DateController : ControllerBase
{
    private readonly IMediator _mediator;

    public DateController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("convert/{day:int}")]
    public async Task<IActionResult> ConvertDateAsync(int day, CancellationToken cancellationToken)
    {
        var query = new DateConverterQuery
        {
            Day = day
        };

        var response = await _mediator.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("currentDate")]
    public IActionResult GetCurrentDate()
    {
        return Ok(DateTime.Now);
    }
}