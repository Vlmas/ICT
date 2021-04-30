using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication
{
    interface DataAccessLayer
    {
        ContactDTO GetContactById(string id);

        string CreateContact(ContactDTO contact);

        bool DeleteContactById(string id);

        List<ContactDTO> GetAllContacts();

        bool UpdateContact(ContactDTO contact);

        List<ContactDTO> GetContacts(int pageSize, int offset);
    }
}
