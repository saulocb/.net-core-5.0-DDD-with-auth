using Bouncer.ViewModels.AppObjects;
using System;

namespace Bouncer.ViewModels.AppObject
{
    public class TokenResult 
    {
        public string Value { get; set; }
        public UserApp_vw User { get; set; }
        public DateTime Expires { get; set; }
    }
}
