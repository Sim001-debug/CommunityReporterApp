using CommunityReporterApp.Data;
using CommunityReporterApp.Models;

namespace CommunityReporterApp.SeedData
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Check if data already exists
                if (!context.Reports.Any())
                {
                    context.Reports.AddRange(
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            Category = "Crime",
                            Description = "A burglary was reported in the area, the house was broken into at night.",
                            ImageUrl = "http://example.com/crime-burglary.jpg",
                            Latitude = -26.2041,
                            Longitude = 28.0473,
                            Status = "Reported",
                            CreatedAt = DateTime.UtcNow,
                            Owner = "china"
                        },
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            Category = "Electricity Issue",
                            Description = "Power outage in the neighborhood for over 6 hours, affecting several households.",
                            ImageUrl = "http://example.com/electricity-outage.jpg",
                            Latitude = -26.2041,
                            Longitude = 28.0473,
                            Status = "Under Review",
                            CreatedAt = DateTime.UtcNow,
                            Owner = "admin"
                        },
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            Category = "Potholes",
                            Description = "A large pothole in the middle of the street causing damage to vehicles.",
                            ImageUrl = "http://example.com/pothole-street.jpg",
                            Latitude = -26.2041,
                            Longitude = 28.0473,
                            Status = "Resolved",
                            CreatedAt = DateTime.UtcNow,
                            Owner = "admin"
                        },
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            Category = "Crime",
                            Description = "A robbery occurred at the local store, and several items were stolen.",
                            ImageUrl = "http://example.com/robbery-store.jpg",
                            Latitude = -26.2041,
                            Longitude = 28.0473,
                            Status = "Under Review",
                            CreatedAt = DateTime.UtcNow,
                            Owner = "china"
                        },
                        new Report
                        {
                            ReportId = Guid.NewGuid(),
                            UserId = Guid.NewGuid(),
                            Category = "Electricity Issue",
                            Description = "Streetlights in the area are not working, posing a safety risk during the night.",
                            ImageUrl = "http://example.com/streetlight-failure.jpg",
                            Latitude = -26.2041,
                            Longitude = 28.0473,
                            Status = "Reported",
                            CreatedAt = DateTime.UtcNow,
                            Owner = "admin"
                        }
                    );

                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
