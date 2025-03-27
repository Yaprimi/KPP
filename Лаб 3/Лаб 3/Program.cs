using System;
using Spectre.Console;
// Базовий клас для всіх товарів
class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"Product: {Name}, Price: {Price}";
    }
}

// Перший рівень успадкування
class Book : Product
{
    public string Author { get; set; }
    public string Publisher { get; set; }

    public Book(string name, decimal price, string author, string publisher)
        : base(name, price)
    {
        Author = author;
        Publisher = publisher;
    }

    public override string ToString()
    {
        return base.ToString() + $", Author: {Author}, Publisher: {Publisher}";
    }
}

class Magazine : Product
{
    public string Periodicity { get; set; }
    public string Publisher { get; set; }

    public Magazine(string name, decimal price, string periodicity, string publisher)
        : base(name, price)
    {
        Periodicity = periodicity;
        Publisher = publisher;
    }

    public override string ToString()
    {
        return base.ToString() + $", Periodicity: {Periodicity}, Publisher: {Publisher}";
    }
}

// Другий рівень успадкування
class FictionBook : Book
{
    public string Genre { get; set; }
    public string TargetAudience { get; set; }

    public FictionBook(string name, decimal price, string author, string publisher, string genre, string targetAudience)
        : base(name, price, author, publisher)
    {
        Genre = genre;
        TargetAudience = targetAudience;
    }

    public override string ToString()
    {
        return base.ToString() + $", Genre: {Genre}, Target Audience: {TargetAudience}";
    }
}

class NonFictionBook : Book
{
    public string Topic { get; set; }
    public string ScientificValue { get; set; }

    public NonFictionBook(string name, decimal price, string author, string publisher, string topic, string scientificValue)
        : base(name, price, author, publisher)
    {
        Topic = topic;
        ScientificValue = scientificValue;
    }

    public override string ToString()
    {
        return base.ToString() + $", Topic: {Topic}, Scientific Value: {ScientificValue}";
    }
}

class GlossyMagazine : Magazine
{
    public string Topic { get; set; }
    public string TargetAudience { get; set; }

    public GlossyMagazine(string name, decimal price, string periodicity, string publisher, string topic, string targetAudience)
        : base(name, price, periodicity, publisher)
    {
        Topic = topic;
        TargetAudience = targetAudience;
    }

    public override string ToString()
    {
        return base.ToString() + $", Topic: {Topic}, Target Audience: {TargetAudience}";
    }
}

class ScienceMagazine : Magazine
{
    public string Topic { get; set; }
    public string ScientificValue { get; set; }

    public ScienceMagazine(string name, decimal price, string periodicity, string publisher, string topic, string scientificValue)
        : base(name, price, periodicity, publisher)
    {
        Topic = topic;
        ScientificValue = scientificValue;
    }

    public override string ToString()
    {
        return base.ToString() + $", Topic: {Topic}, Scientific Value: {ScientificValue}";
    }
}

// Третій рівень успадкування
class Novel : FictionBook
{
    public int PageCount { get; set; }
    public string NarrativeStyle { get; set; }

    public Novel(string name, decimal price, string author, string publisher, string genre, string targetAudience, int pageCount, string narrativeStyle)
        : base(name, price, author, publisher, genre, targetAudience)
    {
        PageCount = pageCount;
        NarrativeStyle = narrativeStyle;
    }

    public override string ToString()
    {
        return base.ToString() + $", Pages: {PageCount}, Narrative Style: {NarrativeStyle}";
    }
}

class Biography : NonFictionBook
{
    public string Subject { get; set; }
    public string Structure { get; set; }

    public Biography(string name, decimal price, string author, string publisher, string topic, string scientificValue, string subject, string structure)
        : base(name, price, author, publisher, topic, scientificValue)
    {
        Subject = subject;
        Structure = structure;
    }

    public override string ToString()
    {
        return base.ToString() + $", Subject: {Subject}, Structure: {Structure}";
    }
}

class AstrophysicsMagazine : ScienceMagazine
{
    public string LatestDiscoveries { get; set; }
    public string AuthoritativeResearch { get; set; }

    public AstrophysicsMagazine(string name, decimal price, string periodicity, string publisher, string topic, string scientificValue, string latestDiscoveries, string authoritativeResearch)
        : base(name, price, periodicity, publisher, topic, scientificValue)
    {
        LatestDiscoveries = latestDiscoveries;
        AuthoritativeResearch = authoritativeResearch;
    }

    public override string ToString()
    {
        return base.ToString() + $", Latest Discoveries: {LatestDiscoveries}, Authoritative Research: {AuthoritativeResearch}";
    }
}

class FashionMagazine : GlossyMagazine
{
    public string Trends { get; set; }
    public string ExclusiveInterviews { get; set; }

    public FashionMagazine(string name, decimal price, string periodicity, string publisher, string topic, string targetAudience, string trends, string exclusiveInterviews)
        : base(name, price, periodicity, publisher, topic, targetAudience)
    {
        Trends = trends;
        ExclusiveInterviews = exclusiveInterviews;
    }

    public override string ToString()
    {
        return base.ToString() + $", Trends: {Trends}, Exclusive Interviews: {ExclusiveInterviews}";
    }
}

class Menu
{
    public static void Show()
    {
        List<Product> products = new List<Product>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Додати товар вручну");
            Console.WriteLine("2. Згенерувати випадковий товар");
            Console.WriteLine("3. Показати всі товари");
            Console.WriteLine("4. Вийти");
            Console.Write("Оберіть опцію: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    products.Add(CreateProductManually());
                    break;
                case "2":
                    products.Add(GenerateRandomProduct());
                    break;
                case "3":
                    ShowAllProducts(products);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }

    private static Product CreateProductManually()
    {
        Console.Write("Введіть назву: ");
        string name = Console.ReadLine();
        Console.Write("Введіть ціну: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.Write("Введіть автора або видавництво: ");
        string authorPublisher = Console.ReadLine();
        Console.Write("Введіть жанр або тему: ");
        string genreTopic = Console.ReadLine();
        return new FictionBook(name, price, authorPublisher, "Unknown", genreTopic, "Дорослі");
    }

    private static Product GenerateRandomProduct()
    {
        string[] bookNames = { "Гаррі Поттер", "Дюна", "1984" };
        string[] magazineNames = { "National Geographic", "Forbes" };
        string[] authors = { "Дж. Роулінг", "Френк Герберт", "Джордж Орвелл" };
        string[] publishers = { "Видавництво А", "Видавництво Б" };
        string[] genres = { "Фентезі", "Наукова фантастика", "Дистопія" };
        string[] topics = { "Космос", "Мода", "Бізнес" };
        decimal[] prices = { 199m, 299m, 399m, 99m, 149m };
        string[] targetAudiences = { "Дорослі", "Підлітки" };
        string[] structures = { "Хронологічна", "Тематична" };
        string[] narrativeStyles = { "Розповідь від першої особи", "Описова" };
        string[] discoveries = { "Нові відкриття у чорних дірах", "Екзопланети" };
        string[] exclusiveInterviews = { "Інтерв’ю з дизайнером", "Розмова з модельєром" };
        int[] pageCounts = { 350, 500, 700 };
        int[] scientificValues = { 5, 7, 9 };
        string[] biographies = { "Ілон Маск", "Стів Джобс", "Марія Кюрі" };

        Random rnd = new Random();
        int productType = rnd.Next(4);

        switch (productType)
        {
            case 0:
                return new Novel(
                    bookNames[rnd.Next(bookNames.Length)],
                    prices[rnd.Next(prices.Length)],
                    authors[rnd.Next(authors.Length)],
                    publishers[rnd.Next(publishers.Length)],
                    genres[rnd.Next(genres.Length)],
                    targetAudiences[rnd.Next(targetAudiences.Length)],
                    pageCounts[rnd.Next(pageCounts.Length)],
                    narrativeStyles[rnd.Next(narrativeStyles.Length)]
                );
            case 1:
                return new Biography(
                    bookNames[rnd.Next(bookNames.Length)],
                    prices[rnd.Next(prices.Length)],
                    authors[rnd.Next(authors.Length)],
                    publishers[rnd.Next(publishers.Length)],
                    "Біографія",
                    targetAudiences[rnd.Next(targetAudiences.Length)],
                    biographies[rnd.Next(biographies.Length)],
                    structures[rnd.Next(structures.Length)]
                );
            case 2:
                return new AstrophysicsMagazine(
                    magazineNames[0],
                    prices[rnd.Next(prices.Length)],
                    "Редакція",
                    publishers[rnd.Next(publishers.Length)],
                    "Наука",
                    scientificValues[rnd.Next(scientificValues.Length)].ToString(),
                    discoveries[rnd.Next(discoveries.Length)],
                    "Останні наукові дослідження"
                );
            case 3:
                return new FashionMagazine(
                    magazineNames[1],
                    prices[rnd.Next(prices.Length)],
                    "Редакція",
                    publishers[rnd.Next(publishers.Length)],
                    "Мода",
                    targetAudiences[rnd.Next(targetAudiences.Length)],
                    topics[rnd.Next(topics.Length)],
                    exclusiveInterviews[rnd.Next(exclusiveInterviews.Length)]
                );
            default:
                return new Novel(
                    bookNames[rnd.Next(bookNames.Length)],
                    prices[rnd.Next(prices.Length)],
                    authors[rnd.Next(authors.Length)],
                    publishers[rnd.Next(publishers.Length)],
                    genres[rnd.Next(genres.Length)],
                    targetAudiences[rnd.Next(targetAudiences.Length)],
                    pageCounts[rnd.Next(pageCounts.Length)],
                    narrativeStyles[rnd.Next(narrativeStyles.Length)]
                );
        }
    }

private static void ShowAllProducts(List<Product> products)
    {
        var table = new Table();
        table.AddColumn(new TableColumn("Тип видання"));
        table.AddColumn(new TableColumn("Назва"));
        table.AddColumn(new TableColumn("Ціна"));
        table.AddColumn(new TableColumn("Підкатегорія"));
        table.AddColumn(new TableColumn("Тип публікації"));
        table.AddColumn(new TableColumn("Автор / Видавництво"));
        table.AddColumn(new TableColumn("Жанр / Тема"));
        table.AddColumn(new TableColumn("Періодичність"));
        table.AddColumn(new TableColumn("Цільова аудиторія"));
        table.AddColumn(new TableColumn("Наукова цінність"));
        table.AddColumn(new TableColumn("Кількість сторінок"));
        table.AddColumn(new TableColumn("Стиль оповіді"));
        table.AddColumn(new TableColumn("Про кого книга"));
        table.AddColumn(new TableColumn("Структура"));
        table.AddColumn(new TableColumn("Останні відкриття"));
        table.AddColumn(new TableColumn("Авторитетні дослідження"));
        table.AddColumn(new TableColumn("Тренди"));
        table.AddColumn(new TableColumn("Ексклюзивні інтерв’ю"));

        table.Border = TableBorder.Square;
        table.ShowRowSeparators = true;
        table.Alignment(Justify.Center);

        foreach (var product in products)
        {
            string authorPublisher = product is Book book ? $"{book.Author} / {book.Publisher}" : "-";
            string genreTopic = product is Book bookProduct ? bookProduct is FictionBook fictionBook ? fictionBook.Genre : ((NonFictionBook)bookProduct).Topic : "-";
            string periodicity = product is Magazine magazine ? magazine.Periodicity : "-";
            string targetAudience = product is FictionBook fiction ? fiction.TargetAudience : "-";
            string scientificValue = product is NonFictionBook nonFiction ? nonFiction.ScientificValue : "-";
            string pageCount = product is Novel novel ? novel.PageCount.ToString() : "-";
            string narrativeStyle = product is Novel novelProduct ? novelProduct.NarrativeStyle : "-";
            string aboutSubject = product is Biography biography ? biography.Subject : "-";
            string structure = product is Biography ? "Документальна" : "-";
            string latestDiscoveries = product is AstrophysicsMagazine astro ? astro.LatestDiscoveries : "-";
            string authoritativeResearch = product is AstrophysicsMagazine astroResearch ? astroResearch.AuthoritativeResearch : "-";
            string trends = product is FashionMagazine fashion ? fashion.Trends : "-";
            string exclusiveInterviews = product is FashionMagazine fashionInterviews ? fashionInterviews.ExclusiveInterviews : "-";

            table.AddRow(
                product.GetType().Name,
                product.Name,
                $"{product.Price} грн",
                product.GetType().BaseType?.Name ?? "-",
                product.GetType().Name.Contains("Magazine") ? "Журнал" : "Книга",
                authorPublisher,
                genreTopic,
                periodicity,
                targetAudience,
                scientificValue,
                pageCount,
                narrativeStyle,
                aboutSubject,
                structure,
                latestDiscoveries,
                authoritativeResearch,
                trends,
                exclusiveInterviews
            );
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Натисніть Enter для продовження...");
        Console.ReadLine();
    }


}

class Program
{
    static void Main()
    {
        System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        System.Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.SetWindowSize(220, 40); // Ширина = 120, Висота = 40
        Menu.Show();

    }
}