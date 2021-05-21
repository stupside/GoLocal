using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Domain.Entities;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GoLocal.Artisan.Application.Queries.Commands.GetCommand
{
    public class GetCommandQueryHandler : AbstractRequestHandler<GetCommandQuery>
    {
        private readonly Context _context;

        public GetCommandQueryHandler(Context context)
        {
            _context = context;
        }

        public override  Task<Result> Handle(GetCommandQuery request, CancellationToken cancellationToken)
        {
             throw new NotImplementedException();
        }
    }
}