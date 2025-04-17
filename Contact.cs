using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    public class Contact
    {
        public string Name { get; private set; }
        public string Mail { get; private set; }
        public string PhoneNumber { get; private set; }
        public Contact(string name , string mail , string phoneNumber)
        {
            this.Name = name;
            this.Mail = mail;
            this.PhoneNumber = phoneNumber;
        }
        public Contact(Contact original, string name, string phoneNumber, string mail)
        {
            Name = name != null ? name : original.Name;
            Mail = mail != null ? mail : original.Mail;
            PhoneNumber = phoneNumber != null ? phoneNumber : original.PhoneNumber; 
        }
    }
}
