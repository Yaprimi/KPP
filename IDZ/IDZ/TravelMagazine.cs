using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class TravelMagazine : GlossyMagazine
    {
        public string PrimaryRegion { get; set; }

        public TravelMagazine() : base()
        {
            PrimaryRegion = "Невідомо";
        }

        public TravelMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string targetAudience, string primaryRegion)
            : base(name, price, periodicity, publisher, topic, targetAudience)
        {
            PrimaryRegion = primaryRegion;
        }

        public override string ToString()
        {
            return base.ToString() + $", Регіон: {PrimaryRegion}";
        }
    }
}
