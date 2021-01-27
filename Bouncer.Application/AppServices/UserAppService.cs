using Bouncer.Common.InternalObjects;
using Bouncer.DI;
using Bouncer.Domain.Entities.Auth;
using Bouncer.Mapping;
using Bouncer.ViewModels.AppObjects;
using JwtAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bouncer.Application.AppServices
{
    public class UserAppService 
    {
        private readonly UserManager<UserApp> _userManager;

        public UserAppService()
        {
            _userManager = AppContainer.Resolve<UserManager<UserApp>>();
        }

        public async Task DesativateAsync(long userId)
        {
            var user = _userManager.Users.Where(x => x.Id == userId).FirstOrDefault();
            user.Active = false;
            await _userManager.UpdateAsync(user);            
        }

        public async Task<AppResult> AddUserToRole(long userId, string roleName)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
                return new AppResult();
            else
                return new AppResult(result.Errors);
        }

        public async Task<AppResult> SignIn(Login_vw request)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == request.UserName);
            if (user is null)            
                return new AppResult("User not found");
            
            var userSigninResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userSigninResult)
            {
                var vw = MappingWraper.Map<UserApp, UserApp_vw>(user);
                var roles = await _userManager.GetRolesAsync(user);
                var resut = JwtAuthenticator.GenerateToken(vw, roles);

                return new AppResult(resut);
            }

            return new AppResult("Wrong Password");
        }

        public async Task<AppResult> SignUp(UserApp_vw request)
        {
            var user = MappingWraper.Map<UserApp_vw, UserApp>(request);
            user.Active = true;
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return new AppResult(result);

            return new AppResult(result.ToString());
        }
    }
}
