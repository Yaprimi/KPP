using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class TravelMagazine : GlossyMagazine
    {
        public string PrimaryRegion { get; set; } // основний регіон, який висвітлює журнал
        public bool HasTravelGuides { get; set; } // чи містить практичні поради

        public TravelMagazine() : base()
        {
            PrimaryRegion = "Невідомо";
            HasTravelGuides = false;
        }

        public TravelMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string targetAudience, string primaryRegion, bool hasTravelGuides)
            : base(name, price, periodicity, publisher, topic, targetAudience)
        {
            PrimaryRegion = primaryRegion;
            HasTravelGuides = hasTravelGuides;
        }

        public override string ToString()
        {
            return base.ToString() + $", Регіон: {PrimaryRegion}, Путівник: {(HasTravelGuides ? "Так" : "Ні")}";
        }
    }
}
