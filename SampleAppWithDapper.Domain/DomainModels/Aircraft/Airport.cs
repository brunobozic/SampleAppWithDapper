using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAppWithDapper.Domain.DomainModels.Aircraft
{
    public class Airport : BaseEntity
    {
        public Airport()
        {
        }

        public string Code { get; set; }
        public string City { get; set; }
        public string ProvinceState { get; set; }
        public string Country { get; set; }
    }
}
