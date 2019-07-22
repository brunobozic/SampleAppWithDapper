using System;
using Dapper.Contrib.Extensions;
using SampleAppWithDapper.Domain.Interfaces;

namespace SampleAppWithDapper.Domain
{
    public class BaseEntity : ISoftDeletable, ICreationAudited, IModificationAuditable, IDeletionAuditable
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        public string ModifiedBy { get; set; }
        public DateTimeOffset? DeletedUtc { get; set; }
        public string DeletedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public interface IDeletionAuditable
    {
        DateTimeOffset? DeletedUtc { get; set; }
        string DeletedBy { get; set; }
    }

    public interface IModificationAuditable
    {
        DateTimeOffset? ModifiedUtc { get; set; }
        string ModifiedBy { get; set; }
    }

    public interface ICreationAudited
    {
        DateTimeOffset CreatedUtc { get; set; }
        string CreatedBy { get; set; }
    }
}
