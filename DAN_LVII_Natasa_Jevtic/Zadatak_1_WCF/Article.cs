
namespace Zadatak_1_WCF
{
    public class Article
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        public Article(string name, int quantity, double price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Article()
        {

        }
    }
}
