using System;
using System.Collections.Generic;
using System.Linq;
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
                        Console.WriteLine("Enter a name of article: ");
                        string inputForName = Console.ReadLine();
                        MainMenu(inputForName);
                        //validation for unique name
                        while (CheckIfNameUnique(inputForName) == false)
                        {
                            Console.WriteLine("This article already exists. Try again:");
                            inputForName = Console.ReadLine();
                            MainMenu(inputForName);
                        }
                        Console.WriteLine("Enter a quantity of article:");
                        string inputForQuantity = Console.ReadLine();
                        MainMenu(inputForQuantity);
                        bool conversionQuantity = Int32.TryParse(inputForQuantity, out int quantity);
                        //validation for quantity
                        while (!conversionQuantity || quantity <= 0)
                        {
                            Console.WriteLine("You didn't entered positive number. Try again:");
                            inputForQuantity = Console.ReadLine();
                            MainMenu(null);
                            conversionQuantity = Int32.TryParse(inputForQuantity, out quantity);
                        }
                        Console.WriteLine("Enter a price of article:");
                        string inputForPrice = Console.ReadLine();
                        MainMenu(null);
                        bool conversionPrice = Double.TryParse(inputForPrice, out double price);
                        //validation for price
                        while (!conversionPrice || price <= 0)
                        {
                            Console.WriteLine("You didn't entered positive number. Try again:");
                            inputForPrice = Console.ReadLine();
                            MainMenu(null);
                            conversionPrice = Double.TryParse(inputForPrice, out price);
                        }
                        using (Service1Client service = new Service1Client())
                        {
                            Article newArticle = new Article
                            {
                                Name = inputForName,
                                Quantity = quantity,
                                Price = price
                            };
                            bool isAdded = service.AddArticle(newArticle);
                            if (isAdded)
                            {
                                Console.WriteLine("Article successfully added.");
                            }
                            else
                            {
                                Console.WriteLine("Article cannot be added.");
                            }
                        }
                        break;
                    case "2":
                        int counterOfItem = 0;
                        Console.WriteLine("\nArticles:");
                        using (Service1Client service = new Service1Client())
                        {
                            foreach (Article item in service.ViewArticles())
                            {
                                Console.WriteLine(++counterOfItem + ". Name: " + item.Name + ", Quantity: " + item.Quantity + ", Price: " + item.Price);
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
        public static void MainMenu(string input)
        {
            if (input == ".")
            {
                Program.Main(null);
            }
        }
        public static bool CheckIfNameUnique(string name)
        {
            using (Service1Client service = new Service1Client())
            {
                List<Article> list = service.ViewArticles().ToList();
                if (list != null && list.Count > 0)
                {
                    var article = list.Where(x => x.Name == name).FirstOrDefault();
                    if (article != null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
