using Microsoft.AspNet.Identity;
using SampleAppWithDapper.Domain.DomainModels.Identity;

namespace SampleAppWithDapper.DataAccess.Repositories
{
    public interface IUserRepositoryAsync : IUserStore<AppUser>, IUserLoginStore<AppUser>, IUserPasswordStore<AppUser>, IUserSecurityStampStore<AppUser>
    {
    }
}
