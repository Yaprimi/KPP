using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    // Перший рівень успадкування
    public class Book : Product
    {
        public string Author { get; set; }
        public string Publisher { get; set; }

        public Book() : base()
        {
            Author = "Невідомо";
            Publisher = "Невідомо";
        }

        public Book(string name, decimal price, string author, string publisher)
            : base(name, price)
        {
            Author = author;
            Publisher = publisher;
        }

        public override string ToString()
        {
            return base.ToString() + $", Автор: {Author}, Видавництво: {Publisher}";
        }
    }
}
