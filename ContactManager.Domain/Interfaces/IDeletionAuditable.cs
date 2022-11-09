using System;

namespace SampleAppWithDapper.Domain
{
    public interface IDeletionAuditable
    {
        DateTimeOffset? DeletedUtc { get; set; }
        string DeletedBy { get; set; }
    }
}