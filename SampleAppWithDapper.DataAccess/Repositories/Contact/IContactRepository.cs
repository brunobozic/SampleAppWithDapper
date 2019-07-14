using System.Threading.Tasks;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public interface IContactRepository
    {
        Task<PaginatedContactsResponse> GetPaginatedResultsAsync(ContactsGetAllPaginatedRequest requestForPaginatedContacts);
        Task<ContactCreateResponse> ContactCreateAsync(ContactCreateRequest createRequest);
        Task<ContactGetByIdResponse> GetContactByIdAsync(int contactId);
        Task<ContactUpdateResult> UpdateContactAsync(int id, ContactUpdateRequest updateRequest);
        Task<ContactDeleteResponse> DeleteContactAsync(ContactDeleteRequest deleteRequest);
    }

  
}
