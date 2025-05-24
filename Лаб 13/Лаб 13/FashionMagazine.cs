using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class FashionMagazine : GlossyMagazine
    {
        public string Trends { get; set; }
        public string ExclusiveInterviews { get; set; }

        public FashionMagazine() : base()
        {
            Trends = "Невідомо";
            ExclusiveInterviews = "Невідомо";
        }

        public FashionMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string targetAudience, string trends, string exclusiveInterviews)
            : base(name, price, periodicity, publisher, topic, targetAudience)
        {
            Trends = trends;
            ExclusiveInterviews = exclusiveInterviews;
        }

        public override string ToString()
        {
            return base.ToString() + $", Тренди: {Trends}, Ексклюзивні інтерв'ю: {ExclusiveInterviews}";
        }
    }
}
