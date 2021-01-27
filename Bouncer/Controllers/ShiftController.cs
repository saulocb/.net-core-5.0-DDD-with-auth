using Bounceer.Api.Controllers;
using Bouncer.Application.AppServices;
using Bouncer.Common.InternalObjects;
using Bouncer.ViewModels.AppObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bouncer.Api.Controllers
{
    [Authorize(Roles = "ADMINISTRATOR")]
    public class ShiftController : BaseController
    {
        private readonly ShiftAppService _appService;
        public ShiftController(ShiftAppService appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var result = await _appService.FindByIdAsync(id);
            return ReturnResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Shift_vw request)
        {
            var result = await _appService.AddAsync(request);
            return ReturnResult(result);
        }

        [HttpPut]
        public IActionResult Put(Shift_vw request)
        {
            _appService.Update(request);
            return ReturnResult(new AppResult());
        }
    }
}
