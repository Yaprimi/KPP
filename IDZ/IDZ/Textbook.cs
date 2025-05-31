using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class Textbook : NonFictionBook
    {
        public string Subject { get; set; }

        public Textbook() : base()
        {
            Subject = "Невідомо";
        }

        public Textbook(string name, decimal price, string author, string publisher,
            string topic, string scientificValue, string subject)
            : base(name, price, author, publisher, topic, scientificValue)
        {
            Subject = subject;
        }

        public override string ToString()
        {
            return base.ToString() + $", Предмет: {Subject}";
        }
    }
}
