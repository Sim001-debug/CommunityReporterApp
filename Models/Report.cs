using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityReporterApp.Models
{
    public class Report
    {
        public Guid ReportId {  get; set; }

        [Required]
        public Guid? UserId { get; set; }
        
        [Required]
        public string Category { get; set; }

        [Required]
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
        public string Status { get; set; } = "Reported";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Owner { get;  set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }
    }
}
