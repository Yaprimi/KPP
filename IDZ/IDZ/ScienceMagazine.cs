using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class ScienceMagazine : Magazine
    {
        public string Topic { get; set; }
        public string ScientificValue { get; set; }

        public ScienceMagazine() : base()
        {
            Topic = "Невідомо";
            ScientificValue = "Невідомо";
        }

        public ScienceMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string scientificValue)
            : base(name, price, periodicity, publisher)
        {
            Topic = topic;
            ScientificValue = scientificValue;
        }

        public override string ToString()
        {
            return base.ToString() + $", Тема: {Topic}, Наукова цінність: {ScientificValue}";
        }
    }
}
