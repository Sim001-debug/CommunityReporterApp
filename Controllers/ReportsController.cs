using CommunityReporterApp.Data;
using CommunityReporterApp.Models;
using CommunityReporterApp.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommunityReporterApp.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        public readonly IMediator _mediator;
        public readonly ILogger<ReportsController> _logger;
        public readonly AppDbContext _context;

        public ReportsController(IMediator mediator, ILogger<ReportsController> logger, AppDbContext context)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        [HttpGet("owner/{owner}")]
        public async Task<ActionResult> GetReportsByOwner([FromRoute] string owner)
        {
            try
            {
                var reportList = await _mediator.Send(new GetReportsByOwnerQuery(owner));

                if(reportList == null || !reportList.Any())
                {
                    _logger.LogWarning("No reports found for owner: {Owner}",owner);
                    return NotFound($"No reports found for owner: {owner}" );
                }

                return Ok(reportList);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occured while fetching reports for owner: {Owner}", owner);
                return StatusCode(500, "An error occured while processing your request.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateReport([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest("Report cannot be null");
            }

            // Assign a new unique ID
            report.ReportId = Guid.NewGuid();

            // If owner is not provided, default it to "Anonymous"
            if (string.IsNullOrWhiteSpace(report.Owner))
            {
                report.Owner = "Anonymous";
            }

            try
            {
                _context.Reports.Add(report);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetReportsByOwner), new { owner = report.Owner }, report);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating report");
                return StatusCode(500, "Internal Server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetAllReports()
        {
            try
            {
                var reports = await _context.Reports.ToListAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all reports");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReportById(Guid id)
        {
            try
            {
                var report = await _context.Reports.FindAsync(id);
                if (report == null)
                {
                    return NotFound();
                }

                return Ok(report);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error retrieving report by ID");
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize(Roles = "AdminOnly")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReportsStatus(Guid id, [FromBody] UpdateReportStatusRequest request)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
                return NotFound("Report not found.");

            report.Status = request.Status;
            report.UpdatedDate = DateTime.UtcNow;

            _context.Reports.Update(report);
            await _context.SaveChangesAsync();

            return Ok(report);
        }

        //[Authorize(Roles = "AdminOnly")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
                return NotFound($"Report with ID {id} not found.");

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok($"Report with ID {id} deleted successfully.");
        }
    }
}
