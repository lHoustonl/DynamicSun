namespace DynamicSun.Domain;

public sealed class WeatherArchive
{
    public int Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public TimeSpan Time { get; set; }
    public double Temperature { get; set; }
    public int? RelativeHumidity { get; set; }
    public double DewPointTemperature { get; set; }
    public int? AtmosphericPressure { get; set; }
    public string WindDirection { get; set; } = null!;
    public int? WindSpeed { get; set; }
    public int? Cloudiness { get; set; }
    public int? CloudBase { get; set; }
    public int? Visibility { get; set; }
    public string? WeatherPhenomena { get; set; }
}