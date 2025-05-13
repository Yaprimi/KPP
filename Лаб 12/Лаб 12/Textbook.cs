using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Textbook : NonFictionBook
    {
        public string Subject { get; set; }
        public string GradeLevel { get; set; }

        public Textbook() : base()
        {
            Subject = "Невідомо";
            GradeLevel = "Невідомо";
        }

        public Textbook(string name, decimal price, string author, string publisher,
            string topic, string scientificValue, string subject, string gradeLevel)
            : base(name, price, author, publisher, topic, scientificValue)
        {
            Subject = subject;
            GradeLevel = gradeLevel;
        }

        public override string ToString()
        {
            return base.ToString() + $", Предмет: {Subject}, Для: {GradeLevel}";
        }
    }
}
