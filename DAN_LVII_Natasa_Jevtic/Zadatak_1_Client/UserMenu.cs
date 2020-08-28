using System;
using System.Collections.Generic;
using System.Linq;
using Zadatak_1_Client.ServiceReferenceArticle;

namespace Zadatak_1_Client
{
    class UserMenu
    {
        /// <summary>
        /// This method displays menu to user and perform different actions based on user choice.
        /// </summary>
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
                        if (inputForName == ".")
                        {
                            break;
                        }
                        //validation for unique name
                        while (CheckIfNameUnique(inputForName) == false)
                        {
                            Console.WriteLine("This article already exists. Try again:");
                            inputForName = Console.ReadLine();
                            if (inputForName == ".")
                            {
                                break;
                            }
                        }
                        if (inputForName != ".")
                        {
                            Console.WriteLine("Enter a quantity of article:");
                            string inputForQuantity = Console.ReadLine();
                            if (inputForQuantity == ".")
                            {
                                break;
                            }
                            bool conversionQuantity = Int32.TryParse(inputForQuantity, out int quantity);
                            //validation for quantity
                            while (!conversionQuantity || quantity <= 0)
                            {
                                Console.WriteLine("You didn't entered positive number. Try again:");
                                inputForQuantity = Console.ReadLine();
                                if (inputForQuantity == ".")
                                {
                                    break;
                                }
                                conversionQuantity = Int32.TryParse(inputForQuantity, out quantity);
                            }
                            if (inputForQuantity != ".")
                            {
                                Console.WriteLine("Enter a price of article:");
                                string inputForPrice = Console.ReadLine();
                                if (inputForPrice == ".")
                                {
                                    break;
                                }
                                bool conversionPrice = Double.TryParse(inputForPrice, out double price);
                                //validation for price
                                while (!conversionPrice || price <= 0)
                                {
                                    Console.WriteLine("You didn't entered positive number. Try again:");
                                    inputForPrice = Console.ReadLine();
                                    if (inputForPrice == ".")
                                    {
                                        break;
                                    }
                                    conversionPrice = Double.TryParse(inputForPrice, out price);
                                }
                                if (inputForPrice != ".")
                                {
                                    //sending data about article to be created to service
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
                                }
                            }
                        }
                        break;
                    case "2":
                        int counterOfItem = 0;
                        Console.WriteLine("\nArticles:");
                        //retrieving data about articles from service
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
                        string answer = "";
                        string bill = "";
                        string article = "";
                        double totalPrice = 0;
                        do
                        {
                            List<Article> articlesToAdd;
                            using (Service1Client service = new Service1Client())
                            {
                                articlesToAdd = service.ViewArticles().ToList();
                                counterOfItem = 0;
                                foreach (Article item in articlesToAdd)
                                {
                                    Console.WriteLine(++counterOfItem + ". Name: " + item.Name + ", Quantity: " + item.Quantity + ", Price: " + item.Price);
                                }
                            }
                            bool canBuy = false;
                            int articleNumber = 0;
                            Article articleToBuy;

                            do
                            {
                                Console.WriteLine("Choose which article you want to add:");
                                string inputForId = Console.ReadLine();
                                bool conversionForId = Int32.TryParse(inputForId, out articleNumber);
                                //checking for existing article
                                while (!conversionForId || articleNumber <= 0 || articleNumber > counterOfItem)
                                {
                                    Console.WriteLine("Invalid input. Try again:");
                                    inputForId = Console.ReadLine();
                                    conversionForId = Int32.TryParse(inputForId, out articleNumber);
                                }
                                articleToBuy = articlesToAdd.ElementAt(articleNumber - 1);
                                if (articleToBuy.Quantity == 0)
                                {
                                    Console.WriteLine("This article is out of stock. Please choose another.");
                                }
                                else
                                {
                                    canBuy = true;
                                }
                            } while (canBuy == false);

                            Console.WriteLine("Enter quantity of article:");
                            string quantityToBuy = Console.ReadLine();
                            bool conversionForQuantityToBuy = Int32.TryParse(quantityToBuy, out int articleQuantityToBuy);
                            while (!conversionForQuantityToBuy || articleQuantityToBuy <= 0 || articleQuantityToBuy > articleToBuy.Quantity)
                            {
                                Console.WriteLine("Invalid input. Try again:");
                                quantityToBuy = Console.ReadLine();
                                conversionForQuantityToBuy = Int32.TryParse(quantityToBuy, out articleQuantityToBuy);
                            }
                            articleToBuy.Quantity -= articleQuantityToBuy;
                            using (Service1Client service = new Service1Client())
                            {
                                bool isChanged = service.UpdateArticle(articleToBuy);
                            }
                            article += articleToBuy.Name + "-" + (articleQuantityToBuy * articleToBuy.Price) + ",";
                            totalPrice += articleQuantityToBuy * articleToBuy.Price;
                            Console.WriteLine("Do you want to add more articles? ");
                            answer = Console.ReadLine().ToUpper();
                            while (answer != "YES" && answer != "NO")
                            {
                                Console.WriteLine("Please enter yes or no.");
                                answer = Console.ReadLine().ToUpper();
                            }                            

                        } while (answer.ToUpper() == "YES");
                        bill += DateTime.Now.ToString("HH:mm:ss") + "," + article + totalPrice;
                        using (Service1Client service = new Service1Client())
                        {
                            bool isCreated = service.CreateBill(bill);
                            if (isCreated)
                            {
                                Console.WriteLine("Successfully finished purchase.");
                            }
                            else
                            {
                                Console.WriteLine("Purchase cannot be made.");
                            }
                        }
                        Console.WriteLine(bill);
                        break;
                    case "4":
                        List<Article> articles;
                        counterOfItem = 0;
                        using (Service1Client service = new Service1Client())
                        {
                            articles = service.ViewArticles().ToList();
                            foreach (Article item in articles)
                            {
                                Console.WriteLine(++counterOfItem + ". Name: " + item.Name + ", Quantity: " + item.Quantity + ", Price: " + item.Price);
                            }
                        }
                        if (articles == null || articles.Count == 0)
                        {
                            Console.WriteLine("There are no articles.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Choose which article you want to update:");
                            string inputForArticle = Console.ReadLine();
                            if (inputForArticle == ".")
                            {
                                break;
                            }
                            bool conversionArticle = Int32.TryParse(inputForArticle, out int articleId);
                            //checking if article with this ordinal number exist
                            while (!conversionArticle || articleId <= 0 || articleId > counterOfItem)
                            {
                                Console.WriteLine("Invalid input. Try again:");
                                inputForArticle = Console.ReadLine();
                                if (inputForArticle == ".")
                                {
                                    break;
                                }
                                conversionArticle = Int32.TryParse(inputForArticle, out articleId);
                            }
                            if (inputForArticle != null)
                            {
                                Console.WriteLine("Enter a price of article:");
                                string priceEdit = Console.ReadLine();
                                if (priceEdit == ".")
                                {
                                    break;
                                }
                                bool conversionPriceEdit = Double.TryParse(priceEdit, out double editedPrice);
                                while (!conversionPriceEdit || editedPrice <= 0)
                                {
                                    Console.WriteLine("You didn't entered positive number. Try again:");
                                    priceEdit = Console.ReadLine();
                                    if (priceEdit == ".")
                                    {
                                        break;
                                    }
                                    conversionPriceEdit = Double.TryParse(priceEdit, out editedPrice);
                                }
                                if (priceEdit != ".")
                                {
                                    Article articleToEdit = articles.ElementAt(counterOfItem - 1);
                                    articleToEdit.Price = editedPrice;
                                    //sending data about article to be updated to service
                                    using (Service1Client service = new Service1Client())
                                    {
                                        bool isUpdated = service.UpdateArticle(articleToEdit);
                                        if (isUpdated)
                                        {
                                            Console.WriteLine("Article is successfully updated.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Article cannot be updated.");
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "5":
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again:");
                        break;
                }
            } while (option != "5");
        }
        /// <summary>
        /// This method checks if article is unique based on name.
        /// </summary>
        /// <param name="name">Article name.</param>
        /// <returns>True if unique, false if not.</returns>
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
