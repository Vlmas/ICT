using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApplication
{
    public abstract class BaseContact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return Name + ": " + Phone + ", " + Address;
        }
    }
}
