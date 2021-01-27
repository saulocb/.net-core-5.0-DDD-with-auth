using Bouncer.Common.InternalObjects;
using Bouncer.DI;
using Bouncer.Domain.Entities.Auth;
using Bouncer.ViewModels.AppObjects;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Bouncer.Application.AppServices
{
    public class LoginAppService
    {
        private readonly SignInManager<UserApp> _userManager;

        public LoginAppService() 
        {
            _userManager = AppContainer.Resolve<SignInManager<UserApp>>();
        }

        public async Task<AppResult> Login(Login_vw user)
        {
            var entity = Mapping.MappingWraper.Map<Login_vw, UserApp>(user);
            await _userManager.SignInAsync(entity,true);

            return new AppResult();
        }
        
        public async Task LogOut()
        {
            await _userManager.SignOutAsync();
        }
    }
}
