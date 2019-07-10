using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAppWithDapper.Domain.DomainModels.Aircraft
{
    public class Aircraft : BaseEntity
    {
        public Aircraft()
        {
        }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string RegistrationNumber { get; set; }

        public int FirstClassCapacity { get; set; }

        public int RegularClassCapacity { get; set; }

        public int CrewCapacity { get; set; }

        public DateTime ManufactureDate { get; set; }

        public int NumberOfEngines { get; set; }

        public int EmptyWeight { get; set; }

        public int MaxTakeoffWeight { get; set; }

        public byte[] RowVer { get; set; }
    }
}
