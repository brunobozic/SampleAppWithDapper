using System.Collections.Generic;

namespace SampleAppWithDapper.Domain.Models
{
    public class UserViewModel : BaseDomain
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool IsAdminRole { get; set; }
        public ICollection<string> Roles { get; set; }  //map from semicolumn delimited from Entity
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public int AccountId { get; set; }

    }
}
