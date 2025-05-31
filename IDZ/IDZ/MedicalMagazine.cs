using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public class MedicalMagazine : ScienceMagazine
    {
        public string MedicalSpecialty { get; set; }

        public MedicalMagazine() : base()
        {
            MedicalSpecialty = "Невідомо";
        }

        public MedicalMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string scientificValue, string medicalSpecialty)
            : base(name, price, periodicity, publisher, topic, scientificValue)
        {
            MedicalSpecialty = medicalSpecialty;
        }

        public override string ToString()
        {
            return base.ToString() + $", Спеціальність: {MedicalSpecialty}";
        }
    }
}
