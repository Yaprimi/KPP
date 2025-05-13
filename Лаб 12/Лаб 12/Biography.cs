using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Biography : NonFictionBook
    {
        public string AboutPerson { get; set; }
        public string Structure { get; set; }

        public Biography() : base()
        {
            AboutPerson = "Невідомо";
            Structure = "Невідомо";
        }

        public Biography(string name, decimal price, string author, string publisher,
            string topic, string scientificValue, string aboutPerson, string structure)
            : base(name, price, author, publisher, topic, scientificValue)
        {
            AboutPerson = aboutPerson;
            Structure = structure;
        }

        public override string ToString()
        {
            return base.ToString() + $", Про особу: {AboutPerson}, Структура: {Structure}";
        }
    }
}
