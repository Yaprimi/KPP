using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class AstrophysicsMagazine : ScienceMagazine
    {
        public string RecentDiscoveries { get; set; }

        public AstrophysicsMagazine() : base()
        {
            RecentDiscoveries = "Невідомо";
        }

        public AstrophysicsMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string scientificValue, string recentDiscoveries)
            : base(name, price, periodicity, publisher, topic, scientificValue)
        {
            RecentDiscoveries = recentDiscoveries;
        }

        public override string ToString()
        {
            return base.ToString() + $", Останні відкриття: {RecentDiscoveries}";
        }
    }
}
