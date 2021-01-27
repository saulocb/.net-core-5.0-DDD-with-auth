using Bouncer.Domain.Entities.Auth;
using System.Threading.Tasks;

namespace Bouncer.Application.Interfaces
{
    public interface IUserBusiness : IBusiness<UserApp>
    {
        Task DesativeAsync(long userId);
    }

    public interface ILoginBusiness : IUserBusiness
    {
        Task<UserApp> Login(UserApp user);
        Task LogOut(UserApp user);
    }
}
