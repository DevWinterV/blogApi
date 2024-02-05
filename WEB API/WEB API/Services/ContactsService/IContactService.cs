using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Post;
using WEB_API.Models;

namespace WEB_API.Services.ContactsService
{
    public interface IContactService
    {
        Task<ServerBaseReponse<List<ContactResponse>>> GetContacts();
        Task<ServerBaseReponse<bool>> UpdateContact(ContactRequest request);
        Task<ServerBaseReponse<bool>> AddContact(ContactRequest contacts);
        Task<ServerBaseReponse<bool>> DeleteContact(int id);
        Task<ServerBaseReponse<ContactResponse>> GetContactById(int id);

    }
}
