using System.Threading.Tasks;
using GoLocal.Bus.Authorizer.Commons.Responses;

namespace GoLocal.Bus.Authorizer.Authorizers
{
    public interface IAuthorizerHandler
    {
        Task<AuthorizerResult> Handle<TRequest>(TRequest request);
    }
}