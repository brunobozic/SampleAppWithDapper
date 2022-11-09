using System;

namespace SampleAppWithDapper.Domain
{
    public interface IModificationAuditable
    {
        DateTimeOffset? ModifiedUtc { get; set; }
        string ModifiedBy { get; set; }
    }
}