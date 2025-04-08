using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Magazine : Product
    {
        public string Periodicity { get; set; } // щомісячний, щотижневий тощо
        public string Publisher { get; set; }

        public Magazine() : base()
        {
            Periodicity = "Невідомо";
            Publisher = "Невідомо";
        }

        public Magazine(string name, decimal price, string periodicity, string publisher)
            : base(name, price)
        {
            Periodicity = periodicity;
            Publisher = publisher;
        }

        public override string ToString()
        {
            return base.ToString() + $", Періодичність: {Periodicity}, Видавництво: {Publisher}";
        }
    }
}
