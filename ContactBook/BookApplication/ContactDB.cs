using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace BookApplication
{
    class ContactDB : DataAccessLayer, IDisposable
    {
        SQLiteConnection connection = default;
        const string source = @"URI=file:test.db";

        public ContactDB()
        {
            connection = new SQLiteConnection(source);
            connection.Open();
            PrepareDB();
        }

        public void Dispose()
        {
            connection.Close();
        }

        private void ExecuteNonQuery(string commandText)
        {
            var command = new SQLiteCommand(connection) { CommandText = commandText };
            command.ExecuteNonQuery();
        }

        private void PrepareDB()
        {
            //SQLiteConnection.CreateFile("test.db");
            ExecuteNonQuery("DROP TABLE IF EXISTS contacts;");
            ExecuteNonQuery("CREATE TABLE contacts(id STRING PRIMARY KEY, name TEXT, phone TEXT, address TEXT);");
        }

        public string CreateContact(ContactDTO contact)
        {
            string text = string.Format("INSERT INTO contacts(id, name, phone, address) VALUES('{0}', '{1}', '{2}', '{3}');"
                ,contact.Id,
                contact.Name,
                contact.Phone,
                contact.Address);

            ExecuteNonQuery(text);
            return contact.Id;
        }

        public List<ContactDTO> GetAllContacts()
        {
            List<ContactDTO> res = new List<ContactDTO>();

            string selectSql = @"SELECT * FROM contacts;";
            using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                }
            }
            return res;
        }
        //
        public ContactDTO GetContactById(string id)
        {
            return null;
        }
        //
        public bool DeleteContactById(string id)
        {
            string statement = string.Format("DELETE FROM contacts WHERE id = '{0}';", id);
            ExecuteNonQuery(statement);
            return true;
        }
        
        public bool UpdateContact(ContactDTO contact)
        {
            string statement = string.Format("UPDATE contacts SET name = '{0}', phone = '{1}', address = '{2}' WHERE id = '{3}';"
                , contact.Name, contact.Phone, contact.Address, contact.Id);
            ExecuteNonQuery(statement);
            return true;
        }
        
        public List<ContactDTO> GetContacts(int pageSize, int offset)
        {
            List<ContactDTO> res = new List<ContactDTO>();

            string selectSql = string.Format("SELECT * FROM contacts ORDER BY name LIMIT {0} OFFSET {1};", pageSize, offset);
            using (SQLiteCommand command = new SQLiteCommand(selectSql, connection))
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new ContactDTO
                    {
                        Id = reader.GetString(0),
                        Name = reader.GetString(1),
                        Phone = reader.GetString(2),
                        Address = reader.GetString(3)
                    };

                    res.Add(item);
                }
            }
            return res;
        }
    }
}