using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class Biography : NonFictionBook
    {
        public string AboutPerson { get; set; }

        public Biography() : base()
        {
            AboutPerson = "Невідомо";
        }

        public Biography(string name, decimal price, string author, string publisher,
            string topic, string scientificValue, string aboutPerson)
            : base(name, price, author, publisher, topic, scientificValue)
        {
            AboutPerson = aboutPerson;
        }

        public override string ToString()
        {
            return base.ToString() + $", Про особу: {AboutPerson}";
        }
    }
}
