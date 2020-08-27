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
        string articleFolder = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files";
        string locationFile = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\Files\Articles.txt";

        public List<Article> ViewArticles()
        {
            if (File.Exists(locationFile))
            {
                List<Article> articles = new List<Article>();
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
            else
            {
                return null;
            }
        }
    }
}
