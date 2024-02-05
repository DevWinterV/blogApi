using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WEB_API.Dtos.Contact;
using WEB_API.Dtos.Post;
using WEB_API.Models;

namespace WEB_API.Services.ContactsService
{

    public class ContactService : IContactService
    { 
        private readonly IMapper _mapper;
        private readonly ContactContext _contactContext;
            public ContactService(IMapper maper, ContactContext contactContext)
            {
                _mapper = maper;
                _contactContext = contactContext;
            }

        public async Task<ServerBaseReponse<bool>> AddContact(ContactRequest contacts)
        {
            var seviceReponse = new ServerBaseReponse<bool>();
            try
            {
                var newcontact = _mapper.Map<Contacts>(contacts);
                _contactContext.Contacts.Add(newcontact);
                await _contactContext.SaveChangesAsync();
                seviceReponse.Message = "Add Success";

            }
            catch (Exception ex)
            {
                seviceReponse.Success = false;
                seviceReponse.Message = ex.Message;
            }
            return seviceReponse;
        }



        public async Task<ServerBaseReponse<bool>> UpdateContact(ContactRequest request)
        {
            var serviceResponse = new ServerBaseReponse<bool>();
            try
            {
                Contacts c = new Contacts();
                
                var contact = await _contactContext.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (contact != null)
                {
                    contact.Name = request.Name;
                    contact.Email = request.Email;
                    contact.Phone = request.Phone;
                    contact.Address = request.Address;
                    await _contactContext.SaveChangesAsync();
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Update success";
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Contact not found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }


        public async Task<ServerBaseReponse<List<ContactResponse>>> GetContacts()
        {
            var seviceReponse = new ServerBaseReponse<List<ContactResponse>>();
            var contact = await _contactContext.Contacts.ToListAsync();
            seviceReponse.Data = contact.Select(x => _mapper.Map<ContactResponse>(x)).ToList();
            seviceReponse.Success = true;
            seviceReponse.Message = "OK";
            return seviceReponse;
        }

        public async Task<ServerBaseReponse<bool>> DeleteContact(int id)
        {
            var seviceReponse = new ServerBaseReponse<bool>();
            if(id == null)
            {
                return seviceReponse;
            }
            var contact = await _contactContext.Contacts.FirstOrDefaultAsync(x => x.Id.Equals(id));
            _contactContext.Contacts.Remove(contact);
            await _contactContext.SaveChangesAsync();   
            seviceReponse.Message = "Delete Success";
            seviceReponse.Success = true;
            seviceReponse.Data = false;
            return seviceReponse;
        }

        public async Task<ServerBaseReponse<ContactResponse>> GetContactById(int id)
        {
            var seviceReponse = new ServerBaseReponse<ContactResponse>();
            var contact = new Contacts();
            contact = await _contactContext.Contacts.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if(contact == null)
            {
                seviceReponse.Message = "Not Found Contact!";
                seviceReponse.Success = true;
                seviceReponse.Data = null;
                return seviceReponse;
            }
            seviceReponse.Message = "Find Success";
            seviceReponse.Success = true;
            var newcontact = _mapper.Map<ContactResponse>(contact);
            seviceReponse.Data = newcontact;
            return seviceReponse;
        }
    }
}
