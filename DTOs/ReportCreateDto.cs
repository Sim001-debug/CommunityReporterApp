using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityReporterApp.DTOs
{
    public class ReportCreateDto
    {
        public Guid UserId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
