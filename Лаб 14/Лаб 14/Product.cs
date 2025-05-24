using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public abstract class Product : IName<Product>
    {

        public event EventHandler<decimal> PriceChanged;

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Назва не може бути порожньою або містити лише пробіли.");
                }

                if (value.Length > 100)
                {
                    throw new ArgumentException("Назва не може перевищувати 100 символів.");
                }

                if (value.Any(ch => char.IsControl(ch) || char.IsSymbol(ch) || char.IsPunctuation(ch)))
                {
                    throw new ArgumentException("Назва не може містити спецсимволи.");
                }

                _name = value;
            }
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set
            {
                if (_price != value)
                {
                    decimal oldPrice = _price;
                    _price = value;
                    PriceChanged?.Invoke(this, oldPrice); // Сповіщення про зміну ціни
                }
            }
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product() : this("Невідомо", 0) { }

        public int CompareTo(object? obj)
        {
            if (obj is IName<Product> other)
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
