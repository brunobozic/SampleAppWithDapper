namespace SampleAppWithDapper.Domain.DomainModels.Identity
{
    public class ConfirmationToken
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}