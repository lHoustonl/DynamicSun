using DynamicSun.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicSun.DataAccess.DataContext.Configurations;

internal class WeatherArchiveConfiguration : IEntityTypeConfiguration<WeatherArchive>
{
    public void Configure(EntityTypeBuilder<WeatherArchive> builder)
    {
        builder.ToTable("weather_archive");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id);

        builder.Property(w => w.Date)
            .HasColumnType("timestamp with time zone");

        builder.Property(w => w.Time);

        builder.Property(w => w.Temperature);

        builder.Property(w => w.RelativeHumidity);

        builder.Property(w => w.DewPointTemperature);

        builder.Property(w => w.AtmosphericPressure);

        builder.Property(w => w.WindDirection)
            .HasMaxLength(256);

        builder.Property(w => w.WindSpeed);

        builder.Property(w => w.Cloudiness);

        builder.Property(w => w.CloudBase);

        builder.Property(w => w.Visibility);

        builder.Property(w => w.WeatherPhenomena)
            .HasMaxLength(256);
    }
}