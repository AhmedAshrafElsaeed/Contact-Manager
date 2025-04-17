using System;
using System.Collections.Generic;
using System.Linq;
namespace ContactManager
{
    public class Logic
    {
        private Logic() { }
        public enum ActionType
        {
            Added,
            Removed,
            Edited
        }
        private static readonly Lazy<Logic> _instance = new Lazy<Logic>(() => new Logic());
        public static Logic Instance => _instance.Value;

        private readonly List<Contact> _contacts = new List<Contact>();
        private readonly Dictionary<string, Contact> _byName = new Dictionary<string, Contact>();
        private readonly Stack<(ActionType, string Name)> _actions = new Stack<(ActionType, string Name)>();

        /// add contact
        public void AddContact()
        {
            var phone = Prompt("Phone Number");
            if(!IsDigitOnly(phone) || string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Invalid phone Number");
                return;
            }
            var name = Prompt("Name");
            if(string.IsNullOrWhiteSpace(name) || _byName.ContainsKey(name))
            {
                Console.WriteLine("Invalid or duplicate name.");
                return;
            }
            var mail = Prompt("Mail");
            if (string.IsNullOrWhiteSpace(mail))
            {
                Console.WriteLine("Invalid mail.");
                return;
            }
            var contact = new Contact(name, phone, mail);
            _contacts.Add(contact);
            _byName[name] = contact;
            _actions.Push((ActionType.Added, name));
            Console.WriteLine("Contact added successfully");
        }
        /// delete contact 
        public void DeleteContact()
        {
            var name = Prompt("Name to delete");
            if(!_byName.TryGetValue(name,out var contact))
            {
                Console.WriteLine("Contact not found");
                return;
            }
            _contacts.Remove(contact);
            _byName.Remove(name);
            _actions.Push((ActionType.Removed, name));
            Console.WriteLine("Contact removed successfully.");
        }

        public void EditContact()
        {
            var name = Prompt("Name to edit");
            if(!_byName.TryGetValue(name ,out var old))
            {
                Console.WriteLine("Contact not found.");
                return;
            }
            Console.WriteLine("1) Phone   2) Name   3) Mail");
            if(!int.TryParse(Console.ReadLine(),out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Invalid choice.");
                return;
            }
            Contact updated;
            switch (choice)
            {
                case 1:
                    string newPhone = Prompt("New phone");
                    updated = new Contact(old, null , newPhone, null);
                    break;
                case 2:
                    string newName = Prompt("New name");
                    updated = new Contact(old, newName, null, null);
                    break;
                case 3:
                    string newMail = Prompt("New mail");
                    updated = new Contact(old, null, null, newMail);
                    break;
                default:
                    updated = old;
                    break;
            }
            int index = _contacts.IndexOf(old);
            _contacts[index] = updated;
            _byName.Remove(old.Name);
            _byName[updated.Name] = updated;
            _actions.Push((ActionType.Edited, updated.Name));
        }
        public void ViewContacts()
        {
            if (_contacts.Count == 0)
            {
                Console.WriteLine("No contacts yet.");
                return;
            }
            foreach (Contact c in _contacts)
            {
                Console.WriteLine("Name: {0} | Phone: {1} | Mail: {2}", c.Name, c.PhoneNumber, c.Mail);
            }

        }
        public void ViewActions()
        {
            if (_actions.Count == 0)
            {
                Console.WriteLine("No actions logged.");
                return;
            }
            foreach(var action in _actions)
            {
                Console.WriteLine("{0} – {1}", action.Item1, action.Item2);
            }
        }
        private static string Prompt(string message)
        {
            Console.WriteLine($"{message} : ");
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }
        private static bool IsDigitOnly(string phone) => phone.All(char.IsDigit);
    }
}
