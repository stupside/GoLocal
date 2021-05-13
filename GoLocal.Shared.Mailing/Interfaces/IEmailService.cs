using System.Threading;
using System.Threading.Tasks;
using GoLocal.Shared.Mailing.Commons.Models;

namespace GoLocal.Shared.Mailing.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
        
    }
}