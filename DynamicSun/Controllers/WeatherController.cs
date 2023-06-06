using DynamicSun.Api.Extensions;
using DynamicSun.Api.Models;
using DynamicSun.Application.Services.ExcelParserService;
using DynamicSun.Application.Services.WeatherService;
using DynamicSun.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace DynamicSun.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetPagedWeatherArchiveListAsync(
        [FromServices] IWeatherService weatherService,
        [FromQuery] int offset = 0,
        [FromQuery][Range(0, 100)] int count = 100,
        [FromQuery] int year = 0,
        [FromQuery] int month = 0,
        CancellationToken cancellationToken = default)
    {
        var total = await weatherService.CountAsync(year, month, cancellationToken);
        if (total == 0)
            return Ok(Page<WeatherArchive>.Empty(offset));

        var result = await weatherService.GetWeatherArchiveListAsync(offset, count, year, month, cancellationToken);
        return Ok(result.AsPage(total, offset));
    }

    [HttpGet("Years")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetWeatherArchiveYearsAsync(
        [FromServices] IWeatherService weatherService,
        CancellationToken cancellationToken = default)
        => Ok(await weatherService.GetWeatherArchiveYearsAsync(cancellationToken));

    [HttpPost]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> UploadWeatherArchiveAsync(
        [FromServices] IWeatherService weatherService,
        [FromServices] IExcelParserService excelParserService,
        [Required] IFormFile file,
        CancellationToken cancellationToken = default)
    {
        List<WeatherArchive> weatherDataList = excelParserService.ImportData(file.OpenReadStream());
        if(weatherDataList.Count == 0)
            return BadRequest();

        await weatherService.UploadArchivesAsync(weatherDataList, cancellationToken);

        return Ok();
    }
}