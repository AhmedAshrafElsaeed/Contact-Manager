using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logic logic = Logic.Instance; 
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("\n1) Add  2) Delete  3) Edit  4) Actions  5) View  6) Exit");
                Console.Write("Choice: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        logic.AddContact();
                        break;
                    case 2:
                        logic.DeleteContact();
                        break;
                    case 3:
                        logic.EditContact();
                        break;
                    case 4:
                        logic.ViewActions();
                        break;
                    case 5:
                        logic.ViewContacts();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
            }
        }
    }
}
