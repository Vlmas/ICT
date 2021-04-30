using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookApplication
{
    public partial class ContactDetail : Form
    {
        public ContactDetail()
        {
            InitializeComponent();
        }

        public ContactDetail(ContactDTO contact)
        {
            InitializeComponent();
            //MessageBox.Show(contact.Name + " " + contact.Phone);
            name.Text = contact.Name;
            phone.Text = contact.Phone;
            address.Text = contact.Address;
        }
    }
}
