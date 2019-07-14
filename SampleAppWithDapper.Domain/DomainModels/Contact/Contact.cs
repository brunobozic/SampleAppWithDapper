using System;

namespace SampleAppWithDapper.Domain.DomainModels.Contact
{
    //SELECT RowNumber, TotalCount, [Id], [FirstName], [LastName], [TelephoneNumberAsEntered], [Email], [ModifiedUTC], [ModifiedBy], [CreatedUTC], [ModifiedBy], [IsDeleted]
    //FROM CTEResult
    public class Contact : BaseEntity
    {
        public Contact()
        {
        }

        public Contact(
            int id
            , int rowNumber
            , int totalCount
            , int filteredCount
            , string firstName
            , string lastName
            , string telephoneNumber_entry
            , string email
            , DateTimeOffset? modifiedUtc
            , string modifiedBy
            , DateTimeOffset createdUtc
            , string createdBy
            , bool isDeleted
            )
        {
            Id = id;
            RowNumber = rowNumber;
            TotalCount = totalCount;
            FilteredCount = filteredCount;
            FirstName = firstName;
            LastName = lastName;
            TelephoneNumber_Entry = telephoneNumber_entry;
            EMail = email;
            ModifiedUtc = modifiedUtc;
            ModifiedBy = modifiedBy;
            CreatedUtc = createdUtc;
            CreatedBy = createdBy;
            IsDeleted = isDeleted;
        }

        public int FilteredCount { get; set; }


        public int RowNumber { get; }
        public int TotalCount { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string TelephoneNumber_Entry { get; }
        public string EMail { get; }
        public string Action { get; set; }
    }
}
