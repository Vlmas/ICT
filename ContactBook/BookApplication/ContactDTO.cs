using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication
{
    public class ContactDTO : BaseContact
    {
        public string Id { get; set; }

        public override string ToString()
        {
            return base.ToString() + ", " + Id;
        }
    }
}
