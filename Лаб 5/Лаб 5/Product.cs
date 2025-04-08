using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    // Базовий клас
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        // Конструктор за замовчуванням
        public Product()
        {
            Name = "Невідомо";
            Price = 0;
        }

        // Конструктор з параметрами
        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        // Перевизначений метод ToString()
        public override string ToString()
        {
            return $"Товар: {Name}, Ціна: {Price} грн";
        }
    }
}
