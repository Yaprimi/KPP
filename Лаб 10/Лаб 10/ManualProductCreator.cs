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
            var novel = new Novel();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    novel.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    novel.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть автора: ");
                    novel.Author = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    novel.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть жанр: ");
                    novel.Genre = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть цільову аудиторію: ");
                    novel.TargetAudience = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть кількість сторінок: ");
                    novel.PageCount = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть стиль оповіді: ");
                    novel.NarrativeStyle = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return novel;
        }

        public static Biography CreateBiographyFromInput()
        {
            Console.WriteLine("\nСтворення нової біографії:");
            var biography = new Biography();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    biography.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    biography.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть автора: ");
                    biography.Author = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    biography.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    biography.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
                    biography.ScientificValue = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть про яку особу: ");
                    biography.AboutPerson = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть структуру (Хронологічна/Тематична/інше): ");
                    biography.Structure = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return biography;
        }

        public static AstrophysicsMagazine CreateAstrophysicsMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового астрофізичного журналу:");
            var magazine = new AstrophysicsMagazine();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    magazine.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    magazine.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
                    magazine.Periodicity = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    magazine.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    magazine.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
                    magazine.ScientificValue = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть останні відкриття: ");
                    magazine.RecentDiscoveries = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть авторитетні дослідження: ");
                    magazine.AuthoritativeResearch = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return magazine;
        }

        public static FashionMagazine CreateFashionMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового модного журналу:");
            var magazine = new FashionMagazine();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    magazine.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    magazine.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть періодичність (Щомісячний/Двомісячний/інше): ");
                    magazine.Periodicity = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    magazine.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    magazine.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть цільову аудиторію: ");
                    magazine.TargetAudience = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тренди: ");
                    magazine.Trends = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ексклюзивні інтерв'ю: ");
                    magazine.ExclusiveInterviews = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return magazine;
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
            var textbook = new Textbook();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    textbook.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    textbook.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть автора: ");
                    textbook.Author = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    textbook.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    textbook.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
                    textbook.ScientificValue = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть предмет: ");
                    textbook.Subject = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть для якого класу/курсу: ");
                    textbook.GradeLevel = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return textbook;
        }

        public static ShortStoryCollection CreateShortStoryCollectionFromInput()
        {
            Console.WriteLine("\nСтворення нової збірки оповідань:");
            var collection = new ShortStoryCollection();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    collection.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    collection.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть автора: ");
                    collection.Author = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    collection.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть жанр: ");
                    collection.Genre = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть цільову аудиторію: ");
                    collection.TargetAudience = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть кількість оповідань: ");
                    collection.StoryCount = int.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Чи є збірка тематичною (Так/Ні): ");
                    collection.IsThematic = Console.ReadLine().ToLower() == "так";
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return collection;
        }

        public static TravelMagazine CreateTravelMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового туристичного журналу:");
            var magazine = new TravelMagazine();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    magazine.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    magazine.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
                    magazine.Periodicity = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    magazine.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    magazine.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть цільову аудиторію: ");
                    magazine.TargetAudience = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть основний регіон: ");
                    magazine.PrimaryRegion = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Чи містить путівник (Так/Ні): ");
                    magazine.HasTravelGuides = Console.ReadLine().ToLower() == "так";
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return magazine;
        }

        public static MedicalMagazine CreateMedicalMagazineFromInput()
        {
            Console.WriteLine("\nСтворення нового медичного журналу:");
            var magazine = new MedicalMagazine();

            while (true)
            {
                try
                {
                    Console.Write("Введіть назву: ");
                    magazine.Name = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть ціну: ");
                    magazine.Price = decimal.Parse(Console.ReadLine());
                    break;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть періодичність (Щомісячний/Квартальний/інше): ");
                    magazine.Periodicity = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть видавництво: ");
                    magazine.Publisher = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть тему: ");
                    magazine.Topic = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть наукову цінність (Висока/Середня/Низька): ");
                    magazine.ScientificValue = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Введіть медичну спеціальність: ");
                    magazine.MedicalSpecialty = Console.ReadLine();
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Чи є рецензованим (Так/Ні): ");
                    magazine.IsPeerReviewed = Console.ReadLine().ToLower() == "так";
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"[Помилка] {ex.Message}");
                    Console.WriteLine("Спробуйте ще раз.");
                }
            }

            return magazine;
        }
    }
}