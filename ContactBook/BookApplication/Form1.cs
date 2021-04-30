using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace BookApplication
{
    public partial class Form1 : Form
    {
        BusinessLogicLayer bll = default;
        int pageSize;
        int offset;

        public Form1()
        {
            InitializeComponent();
            InitComboBox();
            toolStripStatusLabel1.Text = "0";
            pageSize = 3;
            offset = 0;
            LoadContacts();
        }

        public void InitComboBox()
        {
            comboBox1.Items.AddRange(new string[] { "Name", "Phone", "Address" });
            comboBox1.SelectedItem = "Name";
        }

        private void LoadContacts()
        {
            ContactDB contacts = new ContactDB();

            bll = new BusinessLogicLayer(contacts);

            RefreshPage();

            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        public void RefreshPage()
        {
            toolStripStatusLabel1.Text = bll.GetContacts(pageSize, offset, comboBox1.SelectedItem.ToString(), textBox1.Text).Count.ToString();
            bindingSource1.DataSource = bll.GetContacts(pageSize, offset, comboBox1.SelectedItem.ToString(), textBox1.Text);
        }

        private void Refresh(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = bll.GetContacts(pageSize, offset, comboBox1.SelectedItem.ToString(), textBox1.Text).Count.ToString();
            bindingSource1.DataSource = bll.GetContacts(pageSize, offset, comboBox1.SelectedItem.ToString(), textBox1.Text);
        }

        private void AddContact(object sender, EventArgs e)
        {
            CreateContactForm createContactForm = new CreateContactForm();

            if (createContactForm.ShowDialog() == DialogResult.OK)
            {
                CreateContactCommand command = new CreateContactCommand();
                command.Name = createContactForm.nameBox.Text;
                command.Phone = createContactForm.phoneBox.Text;
                if(!int.TryParse(command.Phone, out int result))
                {
                    MessageBox.Show("Enter a valid phone number!");
                    return;
                }
                command.Address = createContactForm.addressBox.Text;
                bll.CreateContact(command);
                RefreshPage();
            }
        }

        private void PreviousPage(object sender, EventArgs e)
        {
            offset = Math.Max(0, offset - pageSize);
            RefreshPage();
        }

        private void NextPage(object sender, EventArgs e)
        {
            offset += pageSize;
            RefreshPage();
        }

        private void DeleteContact(object sender, EventArgs e)
        {
            var contact = dataGridView1.Rows[int.Parse(bindingNavigatorPositionItem.Text) - 1].Cells[0];
            bll.DeleteContact(contact.Value.ToString());
            bindingSource1.RemoveCurrent();
        }

        private void UpdateContact(object sender, EventArgs e)
        {
            var wrapper = dataGridView1.Rows[int.Parse(bindingNavigatorPositionItem.Text) - 1];
            ContactDTO contact = new ContactDTO { 
                Id = wrapper.Cells[0].Value.ToString(),
                Name = wrapper.Cells[1].Value.ToString(),
                Phone = wrapper.Cells[2].Value.ToString(),
                Address = wrapper.Cells[3].Value.ToString()
            };
            bll.UpdateContact(contact);
        }

        private void ShowContactDetails(object sender, EventArgs e)
        {
            var wrapper = dataGridView1.Rows[int.Parse(bindingNavigatorPositionItem.Text) - 1];
            ContactDTO contact = new ContactDTO
            {
                Id = wrapper.Cells[0].Value.ToString(),
                Name = wrapper.Cells[1].Value.ToString(),
                Phone = wrapper.Cells[2].Value.ToString(),
                Address = wrapper.Cells[3].Value.ToString()
            };
            ContactDetail detail = new ContactDetail(contact);
            detail.Show();
        }

        private void PatternChanged(object sender, EventArgs e)
        {
            RefreshPage();
        }

        private void Go(object sender, EventArgs e)
        {
            RefreshPage();
        }
    }
}
