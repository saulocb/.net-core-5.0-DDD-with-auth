using System.Text.Json.Serialization;

namespace Bouncer.ViewModels.AppObjects
{
    public class UserApp_vw : EntityBase_vw<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }   
        public string Phone { get; set; }       
        public string Role { get; set; }       
        public bool Active { get; set; }       
    }

    public class Login_vw : EntityBase_vw<long>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
