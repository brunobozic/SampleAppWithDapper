namespace SampleAppWithDapper.DataAccess.Repositories.AppUsers
{
    public class UserLoginInfo
    {
        public object LoginProvider { get; internal set; }
        public object ProviderKey { get; internal set; }
    }
}