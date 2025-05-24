using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class FictionBook : Book
    {
        public string Genre { get; set; }
        public string TargetAudience { get; set; }

        public FictionBook() : base()
        {
            Genre = "Невідомо";
            TargetAudience = "Невідомо";
        }

        public FictionBook(string name, decimal price, string author, string publisher,
            string genre, string targetAudience)
            : base(name, price, author, publisher)
        {
            Genre = genre;
            TargetAudience = targetAudience;
        }

        public override string ToString()
        {
            return base.ToString() + $", Жанр: {Genre}, Цільова аудиторія: {TargetAudience}";
        }
    }
}
