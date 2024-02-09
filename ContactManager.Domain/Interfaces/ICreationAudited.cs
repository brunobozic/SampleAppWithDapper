using System;

namespace SampleAppWithDapper.Domain
{
    public interface ICreationAudited
    {
        DateTimeOffset CreatedUtc { get; set; }
        string CreatedBy { get; set; }
    }
}
