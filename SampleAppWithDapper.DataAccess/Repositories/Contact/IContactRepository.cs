using System.Threading.Tasks;
using SampleAppWithDapper.DataAccess.MessagePattern;

namespace SampleAppWithDapper.DataAccess.Repositories.Contact
{
    public interface IContactRepository
    {
        Task<PaginatedContactsResponse> GetPaginatedResultsAsync(ContactsGetAllPaginatedRequest requestForPaginatedContacts);
        Task<ContactCreateResponse> ContactCreateAsync(ContactCreateRequest createRequest);
        Task<ContactGetByIdResponse> GetContactByIdAsync(int contactId);
        Task<ContactUpdateResponse> UpdateContactAsync(int id, ContactUpdateRequest updateRequest);
        Task<ContactDeleteResponse> DeleteContactAsync(ContactDeleteRequest deleteRequest);
    }
}
