using System;
using Zadatak_1_Client.ServiceReferenceArticle;

namespace Zadatak_1_Client
{
    class UserMenu
    {
        public static void DisplayMenu()
        {
            string option = "";
            do
            {
                Console.WriteLine("1. Add article");
                Console.WriteLine("2. View articles");
                Console.WriteLine("3. Purchase articles");
                Console.WriteLine("4. Update article");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Choose option:");
                option = Console.ReadLine();
                switch (option)
                {
                    case "1":                        
                        break;
                    case "2":
                        int counterOfItem = 0;
                        Console.WriteLine("\nArticles:");
                        using (Service1Client service = new Service1Client())
                        {
                            foreach (Article item in service.ViewArticles())
                            {
                                Console.WriteLine(++counterOfItem +". Name: " + item.Name + ", Quantity: " + item.Quantity + ", Price: " + item.Price);
                            }
                        }
                        Console.WriteLine();
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again:");
                        break;
                }
            } while (option != "5");
        }
    }
}
