using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Zadatak_1_WCF
{    
    public class Service1 : IService1
    {
        readonly string articleFolder = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files";
        readonly string locationFile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files\Articles.txt";
        static int counterForBill;

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
