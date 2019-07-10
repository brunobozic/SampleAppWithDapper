using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAppWithDapper.Domain.DomainModels.Aircraft
{
    public class AirportSchedule : BaseEntity
    {
        public AirportSchedule()
        {
        }

        public Airport Airport { get; set; }
        public DateTime Day { get; set; }
        public IEnumerable<Flight> Departures { get; set; }
        public IEnumerable<Flight> Arrivals { get; set; }
    }
}
