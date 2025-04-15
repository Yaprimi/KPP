using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product()
        {
            Name = "Невідомо";
            Price = 0;
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"Товар: {Name}, Ціна: {Price} грн";
        }
    }
}
