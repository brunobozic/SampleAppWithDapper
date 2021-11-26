using System;

namespace SampleAppWithDapper.Domain.DomainModels.Identity
{
    public class ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Id { get; set; }
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
        public string NormalizedUserName { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
