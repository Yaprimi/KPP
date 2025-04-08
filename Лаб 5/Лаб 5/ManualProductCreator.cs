using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public static class ManualProductCreator
    {
        public static Novel CreateNovelFromInput()
        {
            Console.WriteLine("\nСтворення нового роману:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть жанр: ");
            string genre = Console.ReadLine();

            Console.Write("Введіть цільову аудиторію: ");
            string targetAudience = Console.ReadLine();

            Console.Write("Введіть кількість сторінок: ");
            int pageCount = int.Parse(Console.ReadLine());

            Console.Write("Введіть стиль оповіді: ");
            string narrativeStyle = Console.ReadLine();

            return new Novel(name, price, author, publisher, genre, targetAudience, pageCount, narrativeStyle);
        }

        public static Biography CreateBiographyFromInput()
        {
            Console.WriteLine("\nСтворення нової біографії:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
            string scientificValue = Console.ReadLine();

            Console.Write("Введіть про яку особу: ");
            string aboutPerson = Console.ReadLine();

            Console.Write("Введіть структуру (Хронологічна/Тематична/інше): ");
            string structure = Console.ReadLine();

            return new Biography(name, price, author, publisher, topic, scientificValue, aboutPerson, structure);
        }

        public static AstrophysicsMagazine CreateAstrophysicsMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового астрофізичного журналу:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
            string periodicity = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
            string scientificValue = Console.ReadLine();

            Console.Write("Введіть останні відкриття: ");
            string recentDiscoveries = Console.ReadLine();

            Console.Write("Введіть авторитетні дослідження: ");
            string authoritativeResearch = Console.ReadLine();

            return new AstrophysicsMagazine(name, price, periodicity, publisher, topic, scientificValue,
                                         recentDiscoveries, authoritativeResearch);
        }

        public static FashionMagazine CreateFashionMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового модного журналу:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть періодичність (Щомісячний/Двомісячний/інше): ");
            string periodicity = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть цільову аудиторію: ");
            string targetAudience = Console.ReadLine();

            Console.Write("Введіть тренди: ");
            string trends = Console.ReadLine();

            Console.Write("Введіть ексклюзивні інтерв'ю: ");
            string exclusiveInterviews = Console.ReadLine();

            return new FashionMagazine(name, price, periodicity, publisher, topic,
                                     targetAudience, trends, exclusiveInterviews);
        }

        public static Product CreateProductFromUserChoice()
        {
            Console.WriteLine("Оберіть тип продукту для створення:");
            Console.WriteLine("1. Роман");
            Console.WriteLine("2. Біографія");
            Console.WriteLine("3. Астрофізичний журнал");
            Console.WriteLine("4. Модний журнал");
            Console.WriteLine("5. Підручник");
            Console.WriteLine("6. Збірка оповідань");
            Console.WriteLine("7. Туристичний журнал");
            Console.WriteLine("8. Медичний журнал");
            Console.Write("Введіть ваш вибір (1-8): ");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 8)
            {
                Console.Write("Некоректне введення. Будь ласка, введіть число від 1 до 8: ");
            }

            switch (choice)
            {
                case 1: return CreateNovelFromInput();
                case 2: return CreateBiographyFromInput();
                case 3: return CreateAstrophysicsMagazineFromInput();
                case 4: return CreateFashionMagazineFromInput();
                case 5: return CreateTextbookFromInput();
                case 6: return CreateShortStoryCollectionFromInput();
                case 7: return CreateTravelMagazineFromInput();
                case 8: return CreateMedicalMagazineFromInput();
                default: return null;
            }
        }

        public static Textbook CreateTextbookFromInput()
        {
            Console.WriteLine("\nСтворення нового підручника:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
            string scientificValue = Console.ReadLine();

            Console.Write("Введіть предмет: ");
            string subject = Console.ReadLine();

            Console.Write("Введіть для якого класу/курсу: ");
            string gradeLevel = Console.ReadLine();


            return new Textbook(name, price, author, publisher, topic, scientificValue, subject, gradeLevel);
        }

        public static ShortStoryCollection CreateShortStoryCollectionFromInput()
        {
            Console.WriteLine("\nСтворення нової збірки оповідань:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть жанр: ");
            string genre = Console.ReadLine();

            Console.Write("Введіть цільову аудиторію: ");
            string targetAudience = Console.ReadLine();

            Console.Write("Введіть кількість оповідань: ");
            int storyCount = int.Parse(Console.ReadLine());

            Console.Write("Чи є збірка тематичною (Так/Ні): ");
            bool isThematic = Console.ReadLine().ToLower() == "так";


            return new ShortStoryCollection(name, price, author, publisher, genre, targetAudience, storyCount, isThematic);
        }

        public static TravelMagazine CreateTravelMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового туристичного журналу:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
            string periodicity = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть цільову аудиторію: ");
            string targetAudience = Console.ReadLine();

            Console.Write("Введіть основний регіон: ");
            string primaryRegion = Console.ReadLine();

            Console.Write("Чи містить путівник (Так/Ні): ");
            bool hasTravelGuides = Console.ReadLine().ToLower() == "так";


            return new TravelMagazine(name, price, periodicity, publisher, topic, targetAudience, primaryRegion, hasTravelGuides);
        }

        public static MedicalMagazine CreateMedicalMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового медичного журналу:");

            Console.Write("Введіть назву: ");
            string name = Console.ReadLine();

            Console.Write("Введіть ціну: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
            string periodicity = Console.ReadLine();

            Console.Write("Введіть видавництво: ");
            string publisher = Console.ReadLine();

            Console.Write("Введіть тему: ");
            string topic = Console.ReadLine();

            Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
            string scientificValue = Console.ReadLine();

            Console.Write("Введіть медичну спеціальність: ");
            string medicalSpecialty = Console.ReadLine();

            Console.Write("Чи є рецензованим (Так/Ні): ");
            bool isPeerReviewed = Console.ReadLine().ToLower() == "так";


            return new MedicalMagazine(name, price, periodicity, publisher, topic, scientificValue, medicalSpecialty, isPeerReviewed);
        }
    }
}
