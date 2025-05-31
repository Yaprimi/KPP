using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class FashionMagazine : GlossyMagazine
    {
        public string Trends { get; set; }

        public FashionMagazine() : base()
        {
            Trends = "Невідомо";
        }

        public FashionMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string targetAudience, string trends)
            : base(name, price, periodicity, publisher, topic, targetAudience)
        {
            Trends = trends;
        }

        public override string ToString()
        {
            return base.ToString() + $", Тренди: {Trends}";
        }
    }
}
