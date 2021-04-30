using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication
{
    class BusinessLogicLayer
    {
        DataAccessLayer dal;

        public BusinessLogicLayer() { }

        public BusinessLogicLayer(DataAccessLayer dal)
        {
            this.dal = dal;
        }

        public string CreateContact(CreateContactCommand contact)
        {
            ContactDTO contact1 = new ContactDTO();
            contact1.Id = Guid.NewGuid().ToString();
            contact1.Name = contact.Name;
            contact1.Phone = contact.Phone;
            contact1.Address = contact.Address;
            return dal.CreateContact(contact1);
        }

        public bool DeleteContact(string id)
        {
            return dal.DeleteContactById(id);
        }

        public ContactDTO GetContact(string id)
        {
            return dal.GetContactById(id);
        }

        public List<ContactDTO> GetContacts()
        {
            return dal.GetAllContacts();
        }

        public List<ContactDTO> GetContacts(int pageSize, int offset)
        {
            return dal.GetContacts(pageSize, offset);
        }

        public bool UpdateContact(ContactDTO contact)
        {
            return dal.UpdateContact(contact);
        }
    }
}