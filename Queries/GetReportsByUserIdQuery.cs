using CommunityReporterApp.Data;
using CommunityReporterApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityReporterApp.Queries
{
    public class GetReportsByUserIdQuery : IRequest<List<Report>>
    {
        public GetReportsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }

    public class GetReportsByUserIdQueryHandler : IRequestHandler<GetReportsByUserIdQuery, List<Report>>
    {
        private readonly AppDbContext _context;

        public GetReportsByUserIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Report>> Handle(GetReportsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reports
                .Where(r => r.UserId == request.UserId)
                .ToListAsync(cancellationToken);
        }
    }
}
