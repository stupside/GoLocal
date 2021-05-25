using System;
using System.Threading;
using System.Threading.Tasks;
using GoLocal.Persistence.EntityFramework;
using GoLocal.Shared.Bus.Commons.Mediator;
using GoLocal.Shared.Bus.Results;

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