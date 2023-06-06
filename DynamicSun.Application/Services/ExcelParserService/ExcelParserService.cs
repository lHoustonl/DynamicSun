using DynamicSun.Domain;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DynamicSun.Application.Services.ExcelParserService;

public class ExcelParserService : IExcelParserService
{
    public List<WeatherArchive> ImportData(Stream stream)
    {
        List<WeatherArchive> data = new List<WeatherArchive>();

        using (var workbook = new XSSFWorkbook(stream))
        {
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                ISheet sheet = workbook.GetSheetAt(i);

                for (int j = 4; j <= sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);

                    if (row != null)
                    {
                        WeatherArchive weatherData = new WeatherArchive();

                        weatherData.Date = DateTime.Parse(row.GetCell(0).StringCellValue).ToUniversalTime();

                        weatherData.Time = TimeSpan.Parse(row.GetCell(1).StringCellValue);

                        weatherData.Temperature = row.GetCell(2).NumericCellValue;

                        weatherData.RelativeHumidity = GetNumericCellValue(row, 3);

                        weatherData.DewPointTemperature = row.GetCell(4).NumericCellValue;

                        weatherData.AtmosphericPressure = GetNumericCellValue(row, 5);

                        weatherData.WindDirection = row.GetCell(6).StringCellValue;

                        weatherData.WindSpeed = GetNumericCellValue(row, 7);

                        weatherData.Cloudiness = GetNumericCellValue(row, 8);

                        weatherData.CloudBase = GetNumericCellValue(row, 9);

                        weatherData.Visibility = GetNumericCellValue(row, 10);

                        weatherData.WeatherPhenomena = row.GetCell(11)?.StringCellValue;

                        data.Add(weatherData);
                    }
                }
            }
        }

        return data;
    }

    private int GetNumericCellValue(IRow row, int rowNumber)
    {
        if (string.IsNullOrWhiteSpace(row.GetCell(rowNumber).ToString()))
            return 0;

        if (row.GetCell(rowNumber).ToString() is "менее 100 м")
            return 0;

        return Convert.ToInt32(row.GetCell(rowNumber).NumericCellValue);
    }
}