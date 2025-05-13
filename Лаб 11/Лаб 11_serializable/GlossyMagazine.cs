using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    [Serializable]
    public class GlossyMagazine : Magazine
    {
        public string Topic { get; set; }
        public string TargetAudience { get; set; }

        public GlossyMagazine() : base()
        {
            Topic = "Невідомо";
            TargetAudience = "Невідомо";
        }

        public GlossyMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string targetAudience)
            : base(name, price, periodicity, publisher)
        {
            Topic = topic;
            TargetAudience = targetAudience;
        }

        public override string ToString()
        {
            return base.ToString() + $", Тема: {Topic}, Цільова аудиторія: {TargetAudience}";
        }
    }
}
