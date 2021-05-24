using System.Threading.Tasks;
using GoLocal.Shared.Locate.Models;

namespace GoLocal.Shared.Locate.Interfaces
{
    public interface ILocateService
    {
        Task<Place> GetPosition(params string[] value);
    }
}