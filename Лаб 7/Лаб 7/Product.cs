using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public abstract class Product : IName
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product() : this("Невідомо", 0) { }

        public int CompareTo(object? obj)
        {
            if (obj is IName other)
            {
                return string.Compare(Name, other.Name, StringComparison.Ordinal);
            }
            return -1;
        }

        public override string ToString()
        {
            return $"Товар: {Name}, Ціна: {Price} грн";
        }
    }
}
