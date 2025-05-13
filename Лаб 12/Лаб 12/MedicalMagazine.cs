using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class MedicalMagazine : ScienceMagazine
    {
        public string MedicalSpecialty { get; set; }
        public bool IsPeerReviewed { get; set; }

        public MedicalMagazine() : base()
        {
            MedicalSpecialty = "Невідомо";
            IsPeerReviewed = false;
        }

        public MedicalMagazine(string name, decimal price, string periodicity, string publisher,
            string topic, string scientificValue, string medicalSpecialty, bool isPeerReviewed)
            : base(name, price, periodicity, publisher, topic, scientificValue)
        {
            MedicalSpecialty = medicalSpecialty;
            IsPeerReviewed = isPeerReviewed;
        }

        public override string ToString()
        {
            return base.ToString() + $", Спеціальність: {MedicalSpecialty}, Рецензований: {(IsPeerReviewed ? "Так" : "Ні")}";
        }
    }
}
