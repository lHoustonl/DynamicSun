using DynamicSun.Domain;

namespace DynamicSun.Application.Services.ExcelParserService;

public interface IExcelParserService
{
    List<WeatherArchive> ImportData(Stream stream);
}