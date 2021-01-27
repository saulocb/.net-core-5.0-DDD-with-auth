using Bouncer.Domain.Entities;
using Bouncer.ViewModels.AppObject;

namespace Bouncer.Application.AppServices
{
    public class ShiftAppService : BaseAppService<Shift_vw, Shift>
    {
        public ShiftAppService(IBusiness<Shift> business) : base(business)
        {
        }
    }
}
