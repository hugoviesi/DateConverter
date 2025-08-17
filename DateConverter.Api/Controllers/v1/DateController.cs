using DateConverter.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateConverter.Api.Controllers.v1;

[ApiController]
[Route("api/v1/dates")]
public sealed class DateController : ControllerBase
{
    [HttpPost("convert/{day:int}")]
    public IActionResult ConvertDate(int day)
    {
        var dateFactory = DateFactory.Create(day);

        var date = dateFactory.IsTurnedMonth()
            ? dateFactory.GetDateWithTurnOfMounth()
            : dateFactory.GetDate();

        return Ok(date);
    }

    [HttpGet("currentDate")]
    public IActionResult GetCurrentDate()
    {
        return Ok(DateTime.Now);
    }
}