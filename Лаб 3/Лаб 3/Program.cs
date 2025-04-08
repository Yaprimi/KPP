using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;

// Базовий клас
public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Конструктор за замовчуванням
    public Product()
    {
        Name = "Невідомо";
        Price = 0;
    }

    // Конструктор з параметрами
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    // Перевизначений метод ToString()
    public override string ToString()
    {
        return $"Товар: {Name}, Ціна: {Price} грн";
    }
}

// Перший рівень успадкування
public class Book : Product
{
    public string Author { get; set; }
    public string Publisher { get; set; }

    public Book() : base()
    {
        Author = "Невідомо";
        Publisher = "Невідомо";
    }

    public Book(string name, decimal price, string author, string publisher)
        : base(name, price)
    {
        Author = author;
        Publisher = publisher;
    }

    public override string ToString()
    {
        return base.ToString() + $", Автор: {Author}, Видавництво: {Publisher}";
    }
}

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

// Другий рівень успадкування
public class FictionBook : Book
{
    public string Genre { get; set; }
    public string TargetAudience { get; set; }

    public FictionBook() : base()
    {
        Genre = "Невідомо";
        TargetAudience = "Невідомо";
    }

    public FictionBook(string name, decimal price, string author, string publisher,
        string genre, string targetAudience)
        : base(name, price, author, publisher)
    {
        Genre = genre;
        TargetAudience = targetAudience;
    }

    public override string ToString()
    {
        return base.ToString() + $", Жанр: {Genre}, Цільова аудиторія: {TargetAudience}";
    }
}

public class NonFictionBook : Book
{
    public string Topic { get; set; }
    public string ScientificValue { get; set; } // висока, середня, низька

    public NonFictionBook() : base()
    {
        Topic = "Невідомо";
        ScientificValue = "Невідомо";
    }

    public NonFictionBook(string name, decimal price, string author, string publisher,
        string topic, string scientificValue)
        : base(name, price, author, publisher)
    {
        Topic = topic;
        ScientificValue = scientificValue;
    }

    public override string ToString()
    {
        return base.ToString() + $", Тема: {Topic}, Наукова цінність: {ScientificValue}";
    }
}

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

// Третій рівень успадкування
public class Novel : FictionBook
{
    public int PageCount { get; set; }
    public string NarrativeStyle { get; set; }

    public Novel() : base()
    {
        PageCount = 0;
        NarrativeStyle = "Невідомо";
    }

    public Novel(string name, decimal price, string author, string publisher,
        string genre, string targetAudience, int pageCount, string narrativeStyle)
        : base(name, price, author, publisher, genre, targetAudience)
    {
        PageCount = pageCount;
        NarrativeStyle = narrativeStyle;
    }

    public override string ToString()
    {
        return base.ToString() + $", Кількість сторінок: {PageCount}, Стиль оповіді: {NarrativeStyle}";
    }
}

public class Biography : NonFictionBook
{
    public string AboutPerson { get; set; }
    public string Structure { get; set; } // хронологічна, тематична тощо

    public Biography() : base()
    {
        AboutPerson = "Невідомо";
        Structure = "Невідомо";
    }

    public Biography(string name, decimal price, string author, string publisher,
        string topic, string scientificValue, string aboutPerson, string structure)
        : base(name, price, author, publisher, topic, scientificValue)
    {
        AboutPerson = aboutPerson;
        Structure = structure;
    }

    public override string ToString()
    {
        return base.ToString() + $", Про особу: {AboutPerson}, Структура: {Structure}";
    }
}

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

public class FashionMagazine : GlossyMagazine
{
    public string Trends { get; set; }
    public string ExclusiveInterviews { get; set; }

    public FashionMagazine() : base()
    {
        Trends = "Невідомо";
        ExclusiveInterviews = "Невідомо";
    }

    public FashionMagazine(string name, decimal price, string periodicity, string publisher,
        string topic, string targetAudience, string trends, string exclusiveInterviews)
        : base(name, price, periodicity, publisher, topic, targetAudience)
    {
        Trends = trends;
        ExclusiveInterviews = exclusiveInterviews;
    }

    public override string ToString()
    {
        return base.ToString() + $", Тренди: {Trends}, Ексклюзивні інтерв'ю: {ExclusiveInterviews}";
    }
}

public class Textbook : NonFictionBook
{
    public string Subject { get; set; }
    public string GradeLevel { get; set; }

    public Textbook() : base()
    {
        Subject = "Невідомо";
        GradeLevel = "Невідомо";
    }

    public Textbook(string name, decimal price, string author, string publisher,
        string topic, string scientificValue, string subject, string gradeLevel)
        : base(name, price, author, publisher, topic, scientificValue)
    {
        Subject = subject;
        GradeLevel = gradeLevel;
    }

    public override string ToString()
    {
        return base.ToString() + $", Предмет: {Subject}, Для: {GradeLevel}";
    }
}

public class ShortStoryCollection : FictionBook
{
    public int StoryCount { get; set; }
    public bool IsThematic { get; set; } // чи всі оповідання на одну тему

    public ShortStoryCollection() : base()
    {
        StoryCount = 0;
        IsThematic = false;
    }

    public ShortStoryCollection(string name, decimal price, string author, string publisher,
        string genre, string targetAudience, int storyCount, bool isThematic)
        : base(name, price, author, publisher, genre, targetAudience)
    {
        StoryCount = storyCount;
        IsThematic = isThematic;
    }

    public override string ToString()
    {
        return base.ToString() + $", Кількість оповідань: {StoryCount}, Тематична збірка: {(IsThematic ? "Так" : "Ні")}";
    }
}

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

public class MedicalMagazine : ScienceMagazine
{
    public string MedicalSpecialty { get; set; } // кардіологія, педіатрія тощо
    public bool IsPeerReviewed { get; set; } // чи рецензований

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


public static class RandomProductGenerator
{
    private static readonly Random random = new Random();

    private static readonly string[] Names =
    {
        "Таємниця зірок", "Квантова фізика", "Модні тенденції",
        "Історичні постаті", "Науково-фантастичні пригоди", "Космічні відкриття",
        "Тенденції подіумів", "Великі вчені", "Фентезійні світи", "Технічні інновації"
    };

    private static readonly string[] Authors =
    {
        "Іван Петренко", "Олена Шевченко", "Михайло Коваленко", "Наталія Бойко",
        "Данило Мельник", "Анна Коваль", "Роман Лисенко", "Юлія Ткаченко"
    };

    private static readonly string[] Publishers =
    {
        "Наукова преса", "Фентезі світ", "Модні медіа", "Історичні книги",
        "Космічні видання", "Технічні видання", "Літературний світ", "Академвидав"
    };

    private static readonly string[] Genres =
    {
        "Фентезі", "Наукова фантастика", "Детектив", "Романтика", "Трилер"
    };

    private static readonly string[] Audiences =
    {
        "Діти", "Підлітки", "Дорослі", "Літні люди", "Всі вікові групи"
    };

    private static readonly string[] Topics =
    {
        "Наука", "Історія", "Технології", "Мистецтво", "Політика", "Філософія"
    };

    private static readonly string[] ScientificValues =
    {
        "Висока", "Середня", "Низька"
    };

    private static readonly string[] Periodicities =
    {
        "Щомісячний", "Квартальний", "Двомісячний", "Щотижневий", "Щорічний"
    };

    private static readonly string[] Trends =
    {
        "Весняна колекція", "Літні стилі", "Осіння мода", "Зимові тенденції",
        "Кольорові палітри", "Тенденції аксесуарів", "Мода взуття"
    };

    private static readonly string[] Discoveries =
    {
        "Нова екзопланета", "Дослідження чорних дір", "Відкриття темної матерії",
        "Квантові обчислення", "Дослідження космосу", "Формування галактик"
    };

    private static readonly string[] Subjects =
    {
        "Математика", "Фізика", "Хімія", "Біологія", "Історія",
        "Географія", "Англійська мова", "Українська література"
    };

    private static readonly string[] GradeLevels =
    {
        "1 клас", "2 клас", "3 клас", "4 клас", "5 клас",
        "6 клас", "7 клас", "8 клас", "9 клас", "10 клас",
        "11 клас", "1 курс", "2 курс", "3 курс", "4 курс"
    };

    private static readonly string[] CoverStyles =
    {
        "М'яка", "Тверда", "Гнучка", "Шкіряна"
    };

    private static readonly string[] Regions =
    {
        "Європа", "Азія", "Африка", "Північна Америка",
        "Південна Америка", "Австралія", "Антарктида"
    };

    private static readonly string[] PhotographyQualities =
    {
        "Висока", "Середня", "Низька"
    };

    private static readonly string[] MedicalSpecialties =
    {
        "Кардіологія", "Педіатрія", "Хірургія", "Терапія",
        "Неврологія", "Дерматологія", "Офтальмологія"
    };

    private static readonly string[] TargetProfessionals =
    {
        "Лікарі", "Медсестри", "Фармацевти", "Студенти медичних ВНЗ"
    };

    public static List<Product> GenerateRandomProducts()
    {
        Console.Write("Введіть кількість випадкових продуктів для генерації: ");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.Write("Некоректне введення. Будь ласка, введіть додатне число: ");
        }

        var products = new List<Product>();

        for (int i = 0; i < count; i++)
        {
            int productType = random.Next(1, 9); // тепер 8 типів продуктів

            switch (productType)
            {
                case 1:
                    products.Add(GenerateRandomNovel());
                    break;
                case 2:
                    products.Add(GenerateRandomBiography());
                    break;
                case 3:
                    products.Add(GenerateRandomAstrophysicsMagazine());
                    break;
                case 4:
                    products.Add(GenerateRandomFashionMagazine());
                    break;
                case 5:
                    products.Add(GenerateRandomTextbook());
                    break;
                case 6:
                    products.Add(GenerateRandomShortStoryCollection());
                    break;
                case 7:
                    products.Add(GenerateRandomTravelMagazine());
                    break;
                case 8:
                    products.Add(GenerateRandomMedicalMagazine());
                    break;
            }
        }

        return products;
    }

    private static Textbook GenerateRandomTextbook()
    {
        return new Textbook(
            name: $"Підручник {Subjects[random.Next(Subjects.Length)]}",
            price: random.Next(200, 600),
            author: Authors[random.Next(Authors.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: Subjects[random.Next(Subjects.Length)],
            scientificValue: ScientificValues[random.Next(ScientificValues.Length)],
            subject: Subjects[random.Next(Subjects.Length)],
            gradeLevel: GradeLevels[random.Next(GradeLevels.Length)]
        );
    }

    private static ShortStoryCollection GenerateRandomShortStoryCollection()
    {
        return new ShortStoryCollection(
            name: $"Збірка оповідань {Authors[random.Next(Authors.Length)]}",
            price: random.Next(150, 400),
            author: Authors[random.Next(Authors.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            genre: Genres[random.Next(Genres.Length)],
            targetAudience: Audiences[random.Next(Audiences.Length)],
            storyCount: random.Next(5, 30),
            isThematic: random.Next(0, 2) == 1
        );
    }

    private static TravelMagazine GenerateRandomTravelMagazine()
    {
        return new TravelMagazine(
            name: $"Подорожі {Regions[random.Next(Regions.Length)]}",
            price: random.Next(100, 300),
            periodicity: Periodicities[random.Next(Periodicities.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: "Подорожі",
            targetAudience: random.Next(0, 2) == 0 ? "Дорослі" : "Сім'ї",
            primaryRegion: Regions[random.Next(Regions.Length)],
            hasTravelGuides: random.Next(0, 2) == 1
        );
    }

    private static MedicalMagazine GenerateRandomMedicalMagazine()
    {
        return new MedicalMagazine(
            name: $"Медицина {MedicalSpecialties[random.Next(MedicalSpecialties.Length)]}",
            price: random.Next(300, 500),
            periodicity: Periodicities[random.Next(Periodicities.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: MedicalSpecialties[random.Next(MedicalSpecialties.Length)],
            scientificValue: "Висока",
            medicalSpecialty: MedicalSpecialties[random.Next(MedicalSpecialties.Length)],
            isPeerReviewed: true
        );
    }

    private static Novel GenerateRandomNovel()
    {
        return new Novel(
            name: Names[random.Next(Names.Length)],
            price: random.Next(100, 500),
            author: Authors[random.Next(Authors.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            genre: Genres[random.Next(Genres.Length)],
            targetAudience: Audiences[random.Next(Audiences.Length)],
            pageCount: random.Next(200, 800),
            narrativeStyle: random.Next(0, 2) == 0 ? "Від першої особи" : "Від третьої особи"
        );
    }

    private static Biography GenerateRandomBiography()
    {
        return new Biography(
            name: $"Біографія {Authors[random.Next(Authors.Length)]}",
            price: random.Next(150, 600),
            author: Authors[random.Next(Authors.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: Topics[random.Next(Topics.Length)],
            scientificValue: ScientificValues[random.Next(ScientificValues.Length)],
            aboutPerson: Authors[random.Next(Authors.Length)],
            structure: random.Next(0, 2) == 0 ? "Хронологічна" : "Тематична"
        );
    }

    private static AstrophysicsMagazine GenerateRandomAstrophysicsMagazine()
    {
        return new AstrophysicsMagazine(
            name: $"Астро {Names[random.Next(Names.Length)]}",
            price: random.Next(200, 400),
            periodicity: Periodicities[random.Next(Periodicities.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: "Астрофізика",
            scientificValue: "Висока",
            recentDiscoveries: Discoveries[random.Next(Discoveries.Length)],
            authoritativeResearch: random.Next(0, 2) == 0 ? "NASA" : "ЄКА"
        );
    }

    private static FashionMagazine GenerateRandomFashionMagazine()
    {
        return new FashionMagazine(
            name: $"Мода {Names[random.Next(Names.Length)]}",
            price: random.Next(100, 300),
            periodicity: Periodicities[random.Next(Periodicities.Length)],
            publisher: Publishers[random.Next(Publishers.Length)],
            topic: "Мода",
            targetAudience: random.Next(0, 2) == 0 ? "Жінки 18-35" : "Чоловіки 20-40",
            trends: Trends[random.Next(Trends.Length)],
            exclusiveInterviews: random.Next(0, 2) == 0 ? "Інтерв'ю з дизайнером" : "Інтерв'ю з моделлю"
        );
    }
}

class Program
{
    private static List<Product> products = new List<Product>();

    static void Main(string[] args)
    {
        System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        System.Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.SetWindowSize(240, 40);

        while (true)
        {
            AnsiConsole.MarkupLine("\n[green]Головне меню:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] Створити продукт вручну");
            AnsiConsole.MarkupLine("[magenta]2.[/] Згенерувати випадкові продукти");
            AnsiConsole.MarkupLine("[blue]3.[/] Переглянути всі продукти");
            AnsiConsole.MarkupLine("[red]4.[/] Вийти");
            AnsiConsole.Markup("[grey]Введіть ваш вибір: [/]");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-4[/]: ");
            }

            switch (choice)
            {
                case 1:
                    products.Add(ManualProductCreator.CreateProductFromUserChoice());
                    AnsiConsole.MarkupLine("[green]Продукт успішно доданий![/]");
                    break;

                case 2:
                    var randomProducts = RandomProductGenerator.GenerateRandomProducts();
                    products.AddRange(randomProducts);
                    AnsiConsole.MarkupLine($"[green]Додано [yellow]{randomProducts.Count}[/] випадкових продуктів.[/]");
                    break;

                case 3:
                    DisplayAllProducts();
                    break;

                case 4:
                    AnsiConsole.MarkupLine("[yellow]До побачення![/]");
                    return;
            }
        }
    }

    private static void DisplayAllProducts()
    {
        if (products.Count == 0)
        {
            AnsiConsole.Markup("[yellow]Немає доступних продуктів.[/]");
            return;
        }

        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.Expand();

        // Налаштування таблиці
        table.ShowHeaders = true;
        table.ShowFooters = true;
        table.UseSafeBorder = true;
        table.ShowRowSeparators = true;

        // Додавання стовпців з кольоровим форматуванням
        table.AddColumn(new TableColumn("[bold deepskyblue1]Тип[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold mediumpurple2]Назва[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold green]Ціна[/]").RightAligned());
        table.AddColumn(new TableColumn("[bold darkorange]Підкатегорія[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold dodgerblue2]Тип публікації[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold lightgoldenrod2]Автор/Видавництво[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold plum4]Жанр/Тема[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold steelblue1]Періодичність[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold violet]Цільова аудиторія[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold yellow]Наукова цінність[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold aqua]Кількість сторінок[/]").RightAligned());
        table.AddColumn(new TableColumn("[bold lightpink1]Стиль оповіді[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold wheat1]Про особу[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold lightcyan1]Структура[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold gold1]Останні відкриття[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold cornflowerblue]Авторитетні дослідження[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold hotpink]Тренди[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold lightgreen]Ексклюзивні інтерв'ю[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold darkseagreen]Предмет[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold lightsteelblue]Для класу[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold red]Кількість оповідань[/]").RightAligned());
        table.AddColumn(new TableColumn("[bold yellow]Тематична[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold orange1]Регіон[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold green]Путівник[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold blue]Спеціальність[/]").LeftAligned());
        table.AddColumn(new TableColumn("[bold grey]Рецензований[/]").LeftAligned());

        foreach (var product in products)
        {
            string type = product switch
            {
                Novel => "[grey84]Роман[/]",
                Biography => "[grey84]Біографія[/]",
                AstrophysicsMagazine => "[grey84]Астрофізичний журнал[/]",
                FashionMagazine => "[grey84]Модний журнал[/]",
                Textbook => "[grey84]Підручник[/]",
                ShortStoryCollection => "[grey84]Збірка оповідань[/]",
                TravelMagazine => "[grey84]Туристичний журнал[/]",
                MedicalMagazine => "[grey84]Медичний журнал[/]",
                _ => $"[grey84]{product.GetType().Name}[/]"
            };

            string name = $"[wheat1]{product.Name.EscapeMarkup()}[/]";
            string price = $"[lime]{product.Price.ToString("C")}[/]";
            string subcategory = "";
            string publicationType = "";
            string authorPublisher = "";
            string genreTopic = "";
            string periodicity = "";
            string targetAudience = "";
            string scientificValue = "";
            string pageCount = "";
            string narrativeStyle = "";
            string aboutPerson = "";
            string structure = "";
            string recentDiscoveries = "";
            string authoritativeResearch = "";
            string trends = "";
            string exclusiveInterviews = "";
            string subject = "";
            string gradeLevel = "";
            string hasExercises = "";
            string storyCount = "";
            string isThematic = "";
            string coverStyle = "";
            string primaryRegion = "";
            string hasTravelGuides = "";
            string photographyQuality = "";
            string medicalSpecialty = "";
            string isPeerReviewed = "";
            string targetProfessionals = "";

            switch (product)
            {
                case Novel novel:
                    subcategory = $"[lightskyblue1]Художня книга[/]";
                    publicationType = $"[aqua]Книга[/]";
                    authorPublisher = $"[wheat1]{novel.Author.EscapeMarkup()} / {novel.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[orchid]{novel.Genre.EscapeMarkup()}[/]";
                    targetAudience = $"[lightpink1]{novel.TargetAudience.EscapeMarkup()}[/]";
                    pageCount = $"[aqua]{novel.PageCount}[/]";
                    narrativeStyle = $"[plum1]{novel.NarrativeStyle.EscapeMarkup()}[/]";
                    break;

                case Biography biography:
                    subcategory = $"[lightskyblue1]Наукова книга[/]";
                    publicationType = $"[aqua]Книга[/]";
                    authorPublisher = $"[wheat1]{biography.Author.EscapeMarkup()} / {biography.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[orchid]{biography.Topic.EscapeMarkup()}[/]";
                    scientificValue = GetScientificValueColor(biography.ScientificValue);
                    aboutPerson = $"[wheat1]{biography.AboutPerson.EscapeMarkup()}[/]";
                    structure = $"[lightcyan1]{biography.Structure.EscapeMarkup()}[/]";
                    break;

                case AstrophysicsMagazine astroMag:
                    subcategory = $"[lightskyblue1]Науковий журнал[/]";
                    publicationType = $"[mediumpurple2]Журнал[/]";
                    authorPublisher = $"[wheat1]{astroMag.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[orchid]{astroMag.Topic.EscapeMarkup()}[/]";
                    periodicity = $"[steelblue1]{astroMag.Periodicity.EscapeMarkup()}[/]";
                    scientificValue = GetScientificValueColor(astroMag.ScientificValue);
                    recentDiscoveries = $"[gold1]{astroMag.RecentDiscoveries.EscapeMarkup()}[/]";
                    authoritativeResearch = $"[cornflowerblue]{astroMag.AuthoritativeResearch.EscapeMarkup()}[/]";
                    break;

                case FashionMagazine fashionMag:
                    subcategory = $"[lightskyblue1]Глянцевий журнал[/]";
                    publicationType = $"[mediumpurple2]Журнал[/]";
                    authorPublisher = $"[wheat1]{fashionMag.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[orchid]{fashionMag.Topic.EscapeMarkup()}[/]";
                    periodicity = $"[steelblue1]{fashionMag.Periodicity.EscapeMarkup()}[/]";
                    targetAudience = $"[lightpink1]{fashionMag.TargetAudience.EscapeMarkup()}[/]";
                    trends = $"[hotpink]{fashionMag.Trends.EscapeMarkup()}[/]";
                    exclusiveInterviews = $"[lightgreen]{fashionMag.ExclusiveInterviews.EscapeMarkup()}[/]";
                    break;

                case Textbook textbook:
                    subcategory = $"[blue]Наукова книга[/]";
                    publicationType = $"[cyan]Книга[/]";
                    authorPublisher = $"[yellow]{textbook.Author.EscapeMarkup()} / {textbook.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[magenta]{textbook.Topic.EscapeMarkup()}[/]";
                    scientificValue = GetScientificValueColor(textbook.ScientificValue);
                    subject = $"[green]{textbook.Subject.EscapeMarkup()}[/]";
                    gradeLevel = $"[cyan1]{textbook.GradeLevel.EscapeMarkup()}[/]";
                    break;

                case ShortStoryCollection shortStory:
                    subcategory = $"[blue]Художня книга[/]";
                    publicationType = $"[cyan]Книга[/]";
                    authorPublisher = $"[yellow]{shortStory.Author.EscapeMarkup()} / {shortStory.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[magenta]{shortStory.Genre.EscapeMarkup()}[/]";
                    targetAudience = $"[lightpink1]{shortStory.TargetAudience.EscapeMarkup()}[/]";
                    storyCount = $"[purple]{shortStory.StoryCount}[/]";
                    isThematic = $"[yellow]{(shortStory.IsThematic ? "Так" : "Ні")}[/]";
                    break;

                case TravelMagazine travelMag:
                    subcategory = $"[blue]Глянцевий журнал[/]";
                    publicationType = $"[magenta]Журнал[/]";
                    authorPublisher = $"[yellow]{travelMag.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[magenta]{travelMag.Topic.EscapeMarkup()}[/]";
                    periodicity = $"[blue1]{travelMag.Periodicity.EscapeMarkup()}[/]";
                    targetAudience = $"[lightpink1]{travelMag.TargetAudience.EscapeMarkup()}[/]";
                    primaryRegion = $"[orange1]{travelMag.PrimaryRegion.EscapeMarkup()}[/]";
                    hasTravelGuides = $"[green]{(travelMag.HasTravelGuides ? "Так" : "Ні")}[/]";
                    break;

                case MedicalMagazine medicalMag:
                    subcategory = $"[blue]Науковий журнал[/]";
                    publicationType = $"[magenta]Журнал[/]";
                    authorPublisher = $"[yellow]{medicalMag.Publisher.EscapeMarkup()}[/]";
                    genreTopic = $"[magenta]{medicalMag.Topic.EscapeMarkup()}[/]";
                    periodicity = $"[blue1]{medicalMag.Periodicity.EscapeMarkup()}[/]";
                    scientificValue = GetScientificValueColor(medicalMag.ScientificValue);
                    medicalSpecialty = $"[blue]{medicalMag.MedicalSpecialty.EscapeMarkup()}[/]";
                    isPeerReviewed = $"[grey]{(medicalMag.IsPeerReviewed ? "Так" : "Ні")}[/]";
                    break;
            }

            table.AddRow(
                type, name, price, subcategory, publicationType, authorPublisher,
                genreTopic, periodicity, targetAudience, scientificValue, pageCount,
                narrativeStyle, aboutPerson, structure, recentDiscoveries,
                authoritativeResearch, trends, exclusiveInterviews, subject, gradeLevel, storyCount, isThematic, primaryRegion,
                hasTravelGuides, medicalSpecialty, isPeerReviewed
            );
        }

        AnsiConsole.Write(table);
    }

    private static string GetScientificValueColor(string value)
    {
        return value.ToLower() switch
        {
            "висока" => "[bold lime]Висока[/]",
            "середня" => "[bold yellow]Середня[/]",
            "низька" => "[bold salmon1]Низька[/]",
            _ => $"[grey]{value.EscapeMarkup()}[/]"
        };
    }
}