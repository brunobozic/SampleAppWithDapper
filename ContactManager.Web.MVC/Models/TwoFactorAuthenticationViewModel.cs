namespace SampleAppWithDapper.Controllers
{
    internal class TwoFactorAuthenticationViewModel
    {
        public bool HasAuthenticator { get; set; }
        public bool Is2faEnabled { get; set; }
        public int RecoveryCodesLeft { get; set; }
    }
}