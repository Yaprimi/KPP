using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    [Serializable]
    public class Novel : FictionBook
    {
        public int PageCount { get; set; }
        public string NarrativeStyle { get; set; }

        public Novel() : base()
        {
            PageCount = 0;
            NarrativeStyle = "Невідомо";
        }

        public Novel(string name, decimal price, string author, string publisher,
            string genre, string targetAudience, int pageCount, string narrativeStyle)
            : base(name, price, author, publisher, genre, targetAudience)
        {
            PageCount = pageCount;
            NarrativeStyle = narrativeStyle;
        }

        public override string ToString()
        {
            return base.ToString() + $", Кількість сторінок: {PageCount}, Стиль оповіді: {NarrativeStyle}";
        }
    }
}
