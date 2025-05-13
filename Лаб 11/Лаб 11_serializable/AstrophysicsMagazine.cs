using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    [Serializable]
    public class AstrophysicsMagazine : ScienceMagazine
    {
        public string RecentDiscoveries { get; set; }
        public string AuthoritativeResearch { get; set; }

        public AstrophysicsMagazine() : base()
        {
            RecentDiscoveries = "Невідомо";
            AuthoritativeResearch = "Невідомо";
        }

        public AstrophysicsMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string scientificValue, string recentDiscoveries, string authoritativeResearch)
            : base(name, price, periodicity, publisher, topic, scientificValue)
        {
            RecentDiscoveries = recentDiscoveries;
            AuthoritativeResearch = authoritativeResearch;
        }

        public override string ToString()
        {
            return base.ToString() + $", Останні відкриття: {RecentDiscoveries}, Авторитетні дослідження: {AuthoritativeResearch}";
        }
    }
}
