using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    [Serializable]
    public class NonFictionBook : Book
    {
        public string Topic { get; set; }
        public string ScientificValue { get; set; }

        public NonFictionBook() : base()
        {
            Topic = "Невідомо";
            ScientificValue = "Невідомо";
        }

        public NonFictionBook(string name, decimal price, string author, string publisher,
            string topic, string scientificValue)
            : base(name, price, author, publisher)
        {
            Topic = topic;
            ScientificValue = scientificValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", Тема: {Topic}, Наукова цінність: {ScientificValue}";
        }
    }
}
