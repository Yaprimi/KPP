using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class Novel : FictionBook
    {
        public int PageCount { get; set; }

        public Novel() : base()
        {
            PageCount = 0;
        }

        public Novel(string name, decimal price, string author, string publisher,
            string genre, string targetAudience, int pageCount)
            : base(name, price, author, publisher, genre, targetAudience)
        {
            PageCount = pageCount;
        }

        public override string ToString()
        {
            return base.ToString() + $", Кількість сторінок: {PageCount}";
        }
    }
}
