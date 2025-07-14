using CommunityReporterApp.Data;
using CommunityReporterApp.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommunityReporterApp.Queries
{
    public class GetReportsByOwnerQuery : IRequest<List<Report>>
    {
        public GetReportsByOwnerQuery(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; }
    }

    public class GetReportsByOwnerQueryHandler : IRequestHandler<GetReportsByOwnerQuery, List<Report>>
    {
        private readonly AppDbContext _context;

        public GetReportsByOwnerQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Report>> Handle(GetReportsByOwnerQuery request, CancellationToken cancellationToken)
        {
            return await _context.Reports
                .Where(r => r.Owner == request.Owner)
                .ToListAsync(cancellationToken);
        }
    }
}
