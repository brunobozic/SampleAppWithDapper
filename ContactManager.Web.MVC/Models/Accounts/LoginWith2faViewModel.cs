namespace SampleAppWithDapper.Controllers
{
    public class LoginWith2faViewModel
    {
        public bool RememberMe { get; set; }
        public string TwoFactorCode { get; set; }
        public bool RememberMachine { get; set; }
    }
}