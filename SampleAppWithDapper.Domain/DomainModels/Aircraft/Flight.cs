using System;
using System.Collections.Generic;
using System.Text;

namespace SampleAppWithDapper.Domain.DomainModels.Aircraft
{
    public class Flight : BaseEntity
    {
        public int ScheduledFlightId { get; set; }
        public ScheduledFlight ScheduledFlight { get; set; }
        public DateTime Day { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime? ActualDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public DateTime? ActualArrival { get; set; }
    }
}
