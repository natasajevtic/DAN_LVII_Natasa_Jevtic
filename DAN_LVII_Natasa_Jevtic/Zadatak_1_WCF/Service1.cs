using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zadatak_1_WCF
{    
    public class Service1 : IService1
    {
        readonly string articleFolder = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files";
        readonly string locationFile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files\Articles.txt";
        static int counterForBill;
        /// <summary>
        /// This method write data about new article to txt.
        /// </summary>
        /// <param name="article">Article to be added.</param>
        /// <returns>True if added, false if not.</returns>
        public bool AddArticle(Article article)
        {
            try
            {
                StreamWriter str = new StreamWriter(locationFile, true);
                str.WriteLine(article.Name + "," + article.Quantity + "," + Math.Round(article.Price, 2));
                str.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        /// <summary>
        /// This method writes data about bill to txt file.
        /// </summary>
        /// <param name="bill">Bill to be created.</param>
        /// <returns>True if created, false if not.</returns>
        public bool CreateBill(string bill)
        {
            try
            {
                StreamWriter str = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + string.Format(@"\..\..\Files\Bill_{0}_TimeStamp.txt", ++counterForBill));
                str.WriteLine(bill);
                str.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// This method updates data about article in txt file.
        /// </summary>
        /// <param name="article">Article to be updated.</param>
        /// <returns>True if updated, false if not.</returns>
        public bool UpdateArticle(Article article)
        {
            List<Article> articles = ViewArticles();
            Article articleToUpdate = articles.Where(x => x.Name == article.Name).FirstOrDefault();
            if (articleToUpdate != null)
            {
                articleToUpdate.Price = article.Price;
                articleToUpdate.Quantity = article.Quantity;
            }
            try
            {
                StreamWriter str = new StreamWriter(locationFile);
                foreach (Article item in articles)
                {
                    str.WriteLine(item.Name + "," + item.Quantity + "," + Math.Round(item.Price, 2));
                }
                str.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }
        /// <summary>
        /// This method read all data about articles from txt file.
        /// </summary>
        /// <returns>List of articles.</returns>
        public List<Article> ViewArticles()
        {
            List<Article> articles = new List<Article>();
            if (File.Exists(locationFile))
            {
                try
                {                    
                    string[] lines = File.ReadAllLines(locationFile);
                    List<string> list = new List<string>();
                    for (int i = 0; i < lines.Length; i++)
                    {
                        list = lines[i].Split(',').ToList();
                        Article article = new Article()
                        {
                            Name = list[0],
                            Quantity = Int32.Parse(list[1]),
                            Price = Double.Parse(list[2])
                        };
                        articles.Add(article);
                    }
                    return articles;
                }
                catch (Exception)
                {
                    return articles;                    
                }                
            }
            else
            {
                return articles;
            }
        }
    }
}