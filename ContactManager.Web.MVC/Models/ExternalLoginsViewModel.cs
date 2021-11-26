using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SampleAppWithDapper.Controllers
{
    internal class ExternalLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public bool ShowRemoveButton { get; internal set; }
        public string StatusMessage { get; internal set; }
        public List<AuthenticationScheme> OtherLogins { get; internal set; }
    }
}