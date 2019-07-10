using System;
using Microsoft.AspNet.Identity;

namespace SampleAppWithDapper.Domain.DomainModels.Identity
{
    public class AppUser : IUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public  string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public string ConfirmationToken { get; set; }

        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Nickname { get; set; }

     
    }
}
