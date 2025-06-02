using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;
using static Program;

namespace IDZ
{
    class main_menu
    {
        private static ProductContainerBase<Product> products = new Container<Product>();
        public static void Main_menu()
        {
            while (true)
            {
                AnsiConsole.MarkupLine("\n[green]Головне меню:[/]");

                AnsiConsole.MarkupLine("[bold underline yellow] Додавання продуктів:[/]");
                AnsiConsole.MarkupLine("[cyan]1.[/] Додати продукт");
                AnsiConsole.MarkupLine("[magenta]2.[/] Згенерувати випадкові продукти");

                AnsiConsole.MarkupLine("\n[bold underline blue] Перегляд та редагування:[/]");
                AnsiConsole.MarkupLine("[blue]3.[/] Переглянути всі продукти");
                AnsiConsole.MarkupLine("[yellow]4.[/] Видалити продукт за індексом");
                AnsiConsole.MarkupLine("[orange1]5.[/] Відсортувати продукти");
                AnsiConsole.MarkupLine("[cyan]6.[/] Пошук продукту");
                AnsiConsole.MarkupLine("[magenta]7.[/] Редагування продукту");

                AnsiConsole.MarkupLine("\n[bold underline purple] Управління контейнером:[/]");
                AnsiConsole.MarkupLine("[purple]8.[/] Змінити тип контейнера (поточний: " + (products.GetType().IsGenericType && products.GetType().GetGenericTypeDefinition() == typeof(Container<>) ? "Масив" : "Двозв'язний список") + ")");
                AnsiConsole.MarkupLine("[darkred]9.[/] Очистити контейнер");
                AnsiConsole.MarkupLine("[darkcyan]10.[/] Показати загальну вартість товарів");

                AnsiConsole.MarkupLine("\n[bold underline darkcyan] Робота з файлами:[/]");
                AnsiConsole.MarkupLine("[darkcyan]11.[/] Зберегти контейнер у файл");
                AnsiConsole.MarkupLine("[darkgreen]12.[/] Завантажити контейнер з файл");
                AnsiConsole.MarkupLine("[darkcyan]13.[/] Зберегти контейнер у текстовий файл");

                AnsiConsole.MarkupLine("\n[bold underline darkorange] Демонстраційні функції:[/]");
                AnsiConsole.MarkupLine("[darkorange]14.[/] Демо універсальних контейнерів");
                AnsiConsole.MarkupLine("[darkmagenta]15.[/] Демонстрація делегатів (Sort/Find/FindAll)");

                AnsiConsole.MarkupLine("\n[bold underline darkgreen] Аналітика:[/]");
                AnsiConsole.MarkupLine("[darkcyan]16.[/] LINQ: Найдешевший та найдорожчий товар");
                AnsiConsole.MarkupLine("[darkgreen]17.[/] LINQ: Середня вартість за категоріями");

                AnsiConsole.MarkupLine("\n[bold underline red] Вихід:[/]");
                AnsiConsole.MarkupLine("[red]18.[/] Вийти");

                AnsiConsole.Markup("[bold]Ваш вибір: [/]");
                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 18)
                {
                    AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-18[/]: ");
                }

                switch (choice)
                {
                    case 1:
                        AddProductMenu();
                        break;
                    case 2:
                        GenerateRandomProducts();
                        break;
                    case 3:
                        DisplayAllProducts();
                        break;
                    case 4:
                        RemoveProductByIndex();
                        break;
                    case 5:
                        SortProducts();
                        break;
                    case 6:
                        SearchProductMenu();
                        break;
                    case 7:
                        EditProductMenu();
                        break;
                    case 8:
                        ChangeContainerType();
                        break;
                    case 9:
                        products.Clear();
                        AnsiConsole.MarkupLine("[green]Контейнер успішно очищено![/]");
                        break;
                    case 10:
                        ShowTotalPrice();
                        break;
                    case 11:
                        SaveContainerToFile();
                        break;
                    case 12:
                        LoadContainerFromFile();
                        break;
                    case 13:
                        SaveContainerToTextFile();
                        break;
                    case 14:
                        DemonstrateGenericContainers();
                        break;
                    case 15:
                        DemonstrateDelegatesAndMethods();
                        break;
                    case 16:
                        ShowMinMaxPrices();
                        break;
                    case 17:
                        ShowAveragePriceByCategory();
                        break;
                    case 18:
                        AnsiConsole.MarkupLine("[yellow]До побачення![/]");
                        return;
                }
            }
        }

        private static void ShowMinMaxPrices()
        {
            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Контейнер порожній.[/]");
                return;
            }

            var productsEnumerable = products.Cast<Product>();
            var cheapest = productsEnumerable.OrderBy(p => p.Price).FirstOrDefault();
            var mostExpensive = productsEnumerable.OrderByDescending(p => p.Price).FirstOrDefault();

            if (cheapest == null || mostExpensive == null)
            {
                AnsiConsole.MarkupLine("[yellow]Не вдалося знайти продукти.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[yellow]Найдешевший[/]\n");
            DisplayProductDetails(cheapest);

            AnsiConsole.MarkupLine("\n[red]Найдорожчий[/]\n");
            DisplayProductDetails(mostExpensive);

        }

        private static void ShowAveragePriceByCategory()
        {
            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Контейнер порожній.[/]");
                return;
            }

            var averagePrices = products.Cast<Product>()
                .GroupBy(p => p.GetType().Name)
                .Select(g => new
                {
                    Category = g.Key,
                    AveragePrice = g.Average(p => p.Price)
                })
                .OrderByDescending(x => x.AveragePrice);

            var table = new Table().RoundedBorder();
            table.AddColumn("[bold]Категорія[/]");
            table.AddColumn("[bold]Середня ціна[/]");

            foreach (var item in averagePrices)
            {
                table.AddRow(
                    $"[blue]{item.Category.EscapeMarkup()}[/]",
                    $"[bold]{item.AveragePrice:C}[/]"
                );
            }

            AnsiConsole.Write(table);
        }

        private static void SaveContainerToTextFile()
        {
            AnsiConsole.Markup("[grey]Введіть шлях до текстового файлу: [/]");
            string path = Console.ReadLine();

            try
            {
                products.SaveToTextFile(path);
                AnsiConsole.MarkupLine("[green]Контейнер збережено у текстовий файл![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при збереженні:[/] {ex.Message}");
            }
        }

        private static void SearchProductMenu()
        {
            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Немає продуктів для пошуку.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[green]Оберіть критерій пошуку:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] За індексом");
            AnsiConsole.MarkupLine("[magenta]2.[/] За назвою");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int searchChoice;
            while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            try
            {
                switch (searchChoice)
                {
                    case 1:
                        AnsiConsole.Markup("[grey]Введіть індекс: [/]");
                        int index = int.Parse(Console.ReadLine());
                        try
                        {
                            Product itemByIndex = products[index - 1];
                            AnsiConsole.MarkupLine("[green]Знайдено продукт:[/]");

                            if (itemByIndex is Product product)
                            {
                                DisplayProductDetails(product);
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"Назва: [bold]{itemByIndex.Name.EscapeMarkup()}[/]");
                            }
                        }
                        catch (ProductNotFoundException ex)
                        {
                            AnsiConsole.MarkupLine($"[red]Помилка: {ex.Message}[/]");
                        }
                        break;

                    case 2:
                        AnsiConsole.Markup("[grey]Введіть назву: [/]");
                        string name = Console.ReadLine();
                        try
                        {
                            Product itemByName = products[name];
                            AnsiConsole.MarkupLine("[green]Знайдено продукт:[/]");

                            if (itemByName is Product product)
                            {
                                DisplayProductDetails(product);
                            }
                            else
                            {
                                AnsiConsole.MarkupLine($"Назва: [bold]{itemByName.Name.EscapeMarkup()}[/]");
                            }
                        }
                        catch (ProductNotFoundException ex)
                        {
                            AnsiConsole.MarkupLine($"[red]Помилка: {ex.Message}[/]");
                        }
                        break;
                }
            }
            catch (FormatException)
            {
                AnsiConsole.MarkupLine("[red]Невірний формат введених даних.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Несподівана помилка: {ex.Message}[/]");
            }
        }

        private static void DemonstrateGenericContainers()
        {
            AnsiConsole.MarkupLine("[bold underline green]Демонстрація універсальних контейнерів:[/]");

            // Контейнер для рядків
            var stringContainer = new Container<StringWrapper>();
            stringContainer.Add(new StringWrapper("Перший рядок"));
            stringContainer.AddToBeginning(new StringWrapper("Другий рядок"));
            stringContainer.InsertAt(1, new StringWrapper("Третій рядок"));

            var stringTable = new Table().RoundedBorder();
            stringTable.Title = new TableTitle("[yellow]Container<StringWrapper>[/]");
            stringTable.AddColumn("[bold]#[/]");
            stringTable.AddColumn("[bold]Тип[/]");
            stringTable.AddColumn("[bold]Значення[/]");

            int counter = 1;
            foreach (var item in stringContainer)
            {
                stringTable.AddRow(
                    (counter++).ToString(),
                    "[grey84]StringWrapper[/]",
                    $"[wheat1]{item.Value.EscapeMarkup()}[/]"
                );
            }
            AnsiConsole.Write(stringTable);

            // Контейнер для чисел
            var intList = new LinkedListContainer<IntWrapper>();
            intList.Add(new IntWrapper(100, "Елемент 1"));
            intList.AddToBeginning(new IntWrapper(200, "Елемент 2"));
            intList.InsertAt(1, new IntWrapper(300, "Елемент 3"));

            var intTable = new Table().RoundedBorder();
            intTable.Title = new TableTitle("[cyan]LinkedListContainer<IntWrapper>[/]");
            intTable.AddColumn("[bold]#[/]");
            intTable.AddColumn("[bold]Тип[/]");
            intTable.AddColumn("[bold]Значення[/]");
            intTable.AddColumn("[bold]Ім'я[/]");

            counter = 1;
            foreach (var item in intList)
            {
                intTable.AddRow(
                    (counter++).ToString(),
                    "[grey84]IntWrapper[/]",
                    $"[lime]{item.Value}[/]",
                    $"[wheat1]{item.Name.EscapeMarkup()}[/]"
                );
            }
            AnsiConsole.Write(intTable);

            AnsiConsole.MarkupLine("\n[green]Натисніть будь-яку клавішу...[/]");
            Console.ReadKey();
        }

        private static void EditProductMenu()
        {
            Product itemToEdit = null;

            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Немає продуктів для редагування.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[green]Оберіть критерій для пошуку продукту:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] За індексом");
            AnsiConsole.MarkupLine("[magenta]2.[/] За назвою");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int searchChoice;
            while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            try
            {
                switch (searchChoice)
                {
                    case 1:
                        AnsiConsole.Markup("[grey]Введіть індекс: [/]");
                        int index = int.Parse(Console.ReadLine());
                        itemToEdit = products[index - 1];
                        break;
                    case 2:
                        AnsiConsole.Markup("[grey]Введіть назву: [/]");
                        string name = Console.ReadLine();
                        itemToEdit = products[name];
                        break;
                }

                if (itemToEdit is Product productToEdit)
                {
                    AnsiConsole.MarkupLine("[green]Поточні дані продукту:[/]");
                    DisplayProductDetails(productToEdit);

                    AnsiConsole.MarkupLine("\n[green]Оберіть поле для редагування:[/]");
                    AnsiConsole.MarkupLine("[cyan]1.[/] Назва");
                    AnsiConsole.MarkupLine("[magenta]2.[/] Ціна");
                    AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

                    int editChoice;
                    while (!int.TryParse(Console.ReadLine(), out editChoice) || editChoice < 1 || editChoice > 2)
                    {
                        AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
                    }

                    switch (editChoice)
                    {
                        case 1:
                            AnsiConsole.Markup("[grey]Введіть нову назву: [/]");
                            string newName = Console.ReadLine();
                            productToEdit.Name = newName;
                            break;
                        case 2:
                            AnsiConsole.Markup("[grey]Введіть нову ціну: [/]");
                            decimal newPrice = decimal.Parse(Console.ReadLine());
                            productToEdit.Price = newPrice;
                            break;
                    }

                    AnsiConsole.MarkupLine("\n[green]Оновлений продукт:[/]");
                    DisplayProductDetails(productToEdit);
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка редагування:[/] {ex.Message}");
            }
        }


        private static void DisplayProductDetails(Product item)
        {
            if (item is not Product product)
            {
                AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{item.Name.EscapeMarkup()}[/]");
                return;
            }

            switch (product)
            {
                case MedicalMagazine medical:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{medical.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{medical.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Періодичність:[/] [magenta]{medical.Periodicity.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{medical.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Тема:[/] [lime]{medical.Topic.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Наукова цінність:[/] [blue]{medical.ScientificValue.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Спеціальність:[/] [salmon1]{medical.MedicalSpecialty.EscapeMarkup()}[/]");
                    break;

                case Novel novel:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{novel.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{novel.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Автор:[/] [blue]{novel.Author.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{novel.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Жанр:[/] [magenta]{novel.Genre.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Аудиторія:[/] [darkorange]{novel.TargetAudience.EscapeMarkup()}[/]"); 
                    AnsiConsole.MarkupLine($"[bold]Сторінок:[/] [cyan]{novel.PageCount}[/]");
                    break;

                case Biography biography:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{biography.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{biography.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Автор:[/] [blue]{biography.Author.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{biography.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Про особу:[/] [turquoise4]{biography.AboutPerson.EscapeMarkup()}[/]"); 
                    AnsiConsole.MarkupLine($"[bold]Наукова цінність:[/] [blue]{biography.ScientificValue.EscapeMarkup()}[/]");
                    break;

                case AstrophysicsMagazine astroMag:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{astroMag.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{astroMag.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Періодичність:[/] [magenta]{astroMag.Periodicity.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{astroMag.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Тема:[/] [lime]{astroMag.Topic.EscapeMarkup()}[/]"); 
                    AnsiConsole.MarkupLine($"[bold]Наукова цінність:[/] [blue]{astroMag.ScientificValue.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Відкриття:[/] [springgreen2]{astroMag.RecentDiscoveries.EscapeMarkup()}[/]"); 
                    break;

                case FashionMagazine fashionMag:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{fashionMag.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{fashionMag.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Періодичність:[/] [magenta]{fashionMag.Periodicity.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{fashionMag.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Тема:[/] [lime]{fashionMag.Topic.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Аудиторія:[/] [darkorange]{fashionMag.TargetAudience.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Тренди:[/] [hotpink]{fashionMag.Trends.EscapeMarkup()}[/]");
                    break;

                case Textbook textbook:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{textbook.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{textbook.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Автор:[/] [blue]{textbook.Author.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{textbook.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Предмет:[/] [sandybrown]{textbook.Subject.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Наукова цінність:[/] [blue]{textbook.ScientificValue.EscapeMarkup()}[/]");
                    break;

                case ShortStoryCollection storyCollection:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{storyCollection.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{storyCollection.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Автор:[/] [blue]{storyCollection.Author.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{storyCollection.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Жанр:[/] [magenta]{storyCollection.Genre.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Кількість оповідань:[/] [mediumorchid]{storyCollection.StoryCount}[/]");
                    AnsiConsole.MarkupLine($"[bold]Аудиторія:[/] [darkorange]{storyCollection.TargetAudience.EscapeMarkup()}[/]");
                    break;

                case TravelMagazine travelMag:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{travelMag.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{travelMag.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Періодичність:[/] [magenta]{travelMag.Periodicity.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Видавництво:[/] [yellow]{travelMag.Publisher.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Регіон:[/] [seagreen3]{travelMag.PrimaryRegion.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Аудиторія:[/] [darkorange]{travelMag.TargetAudience.EscapeMarkup()}[/]");
                    break;

                default:
                    AnsiConsole.MarkupLine($"[bold]Товар:[/] [cyan]{product.Name.EscapeMarkup()}[/]");
                    AnsiConsole.MarkupLine($"[bold]Ціна:[/] [green]{product.Price} грн[/]");
                    AnsiConsole.MarkupLine($"[bold]Тип:[/] [white]{product.GetType().Name.EscapeMarkup()}[/]");
                    break;
            }
        }

        private static void AddProductMenu()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть спосіб додавання продукту:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] Вручну");
            AnsiConsole.MarkupLine("[magenta]2.[/] Випадково згенерувати");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int inputChoice;
            while (!int.TryParse(Console.ReadLine(), out inputChoice) || inputChoice < 1 || inputChoice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            Product product = inputChoice == 1
                ? ManualProductCreator.CreateProductFromUserChoice()
                : RandomProductGenerator.GenerateRandomProduct();

            AnsiConsole.MarkupLine("\n[green]Оберіть позицію для додавання:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] На початок");
            AnsiConsole.MarkupLine("[magenta]2.[/] В кінець");
            AnsiConsole.MarkupLine("[blue]3.[/] За конкретним індексом");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-3): [/]");

            int positionChoice;
            while (!int.TryParse(Console.ReadLine(), out positionChoice) || positionChoice < 1 || positionChoice > 3)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-3[/]: ");
            }

            switch (positionChoice)
            {
                case 1:
                    products.InsertAt(0, product);
                    AnsiConsole.MarkupLine("[green]Продукт успішно доданий на початок ![/]");
                    break;
                case 2:
                    products.InsertAt(products.Count, product);
                    AnsiConsole.MarkupLine($"[green]Продукт успішно доданий в кінець (індекс {products.Count - 1})![/]");
                    break;
                case 3:
                    InsertProductAtPosition(product);
                    break;
            }
        }

        private static void SortProducts()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть поле для сортування:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] За ціною (від дешевих до дорогих)");
            AnsiConsole.MarkupLine("[magenta]2.[/] За назвою (алфавітний порядок)");
            AnsiConsole.MarkupLine("[blue]3.[/] За типом публікації");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-3): [/]");

            int sortChoice;
            while (!int.TryParse(Console.ReadLine(), out sortChoice) || sortChoice < 1 || sortChoice > 3)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-3[/]: ");
            }

            switch (sortChoice)
            {
                case 1:
                    products.OrderBy(ProductSortField.Price);
                    AnsiConsole.MarkupLine("[green]Продукти відсортовано за ціною![/]");
                    break;
                case 2:
                    products.OrderBy(ProductSortField.Name);
                    AnsiConsole.MarkupLine("[green]Продукти відсортовано за назвою![/]");
                    break;
                case 3:
                    products.OrderBy(ProductSortField.PublicationType);
                    AnsiConsole.MarkupLine("[green]Продукти відсортовано за типом публікації![/]");
                    break;
            }
        }

        private static void InsertProductAtPosition(Product product)
        {
            if (products.Count > 0)
            {
                AnsiConsole.MarkupLine($"\n[green]Доступні індекси: від 1 до {products.Count}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine("\n[green]Список порожній, можна вставити тільки на позицію 1 [/]");
            }

            AnsiConsole.Markup("[grey]Введіть номер позиції (1-" + (products.Count + 1) + "): [/]");

            int position;
            while (!int.TryParse(Console.ReadLine(), out position) || position < 1 || position > products.Count + 1)
            {
                AnsiConsole.Markup($"[red]Некоректне введення.[/] Будь ласка, введіть число від [yellow]1 до {products.Count + 1}[/]: ");
            }

            int index = position - 1;
            products.InsertAt(index, product);
            AnsiConsole.MarkupLine($"[green]Продукт успішно вставлено на позицію {position}![/]");
        }

        private static void GenerateRandomProducts()
        {
            AnsiConsole.Markup("\n[grey]Скільки продуктів згенерувати?: [/]");
            int count;
            while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть додатне число: ");
            }

            AnsiConsole.MarkupLine("\n[green]Оберіть позицію для додавання:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] На початок");
            AnsiConsole.MarkupLine("[magenta]2.[/] В кінець");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int positionChoice;
            while (!int.TryParse(Console.ReadLine(), out positionChoice) || positionChoice < 1 || positionChoice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            for (int i = 0; i < count; i++)
            {
                Product product = (Product)RandomProductGenerator.GenerateRandomProduct();
                if (positionChoice == 1)
                {
                    products.AddToBeginning(product);
                }
                else
                {
                    products.Add(product);
                }
            }

            AnsiConsole.MarkupLine($"[green]Додано [yellow]{count}[/] випадкових продуктів![/]");
        }

        private static void ChangeContainerType()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть тип контейнера:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] Масив");
            AnsiConsole.MarkupLine("[magenta]2.[/] Двозв'язний список");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            ProductContainerBase<Product> newContainer = choice == 1
    ? new Container<Product>()
    : new LinkedListContainer<Product>();

            for (int i = 0; i < products.Count; i++)
            {
                newContainer.Add(products[i]);
            }

            products = newContainer;
            AnsiConsole.MarkupLine($"[green]Тип контейнера змінено на: {(choice == 1 ? "Масив" : "Двозв'язний список")}[/]");
        }

        private static void RemoveProductByIndex()
        {
            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Немає продуктів для видалення.[/]");
                return;
            }

            AnsiConsole.MarkupLine($"\n[green]Доступні індекси: 1-{products.Count}[/]");
            AnsiConsole.Markup("[grey]Введіть індекс продукту для видалення: [/]");

            try
            {
                if (!int.TryParse(Console.ReadLine(), out int index))
                {
                    throw new InvalidProductOperationException("Введено некоректний формат індексу", null, "Remove product");
                }

                if (index < 1 || index > products.Count)
                {
                    throw new ProductNotFoundException(
                        $"Індекс {index} виходить за межі діапазону (1-{products.Count})",
                        null,
                        "Remove product");
                }

                Product itemToRemove = products[index - 1];

                products.RemoveAt(index - 1);

                AnsiConsole.MarkupLine($"[green]Продукт успішно видалений:[/] {itemToRemove.Name}");
            }
            catch (ProductNotFoundException ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при видаленні:[/] {ex.Message}");
                AnsiConsole.MarkupLine($"[grey]Деталі: спроба видалити продукт за неіснуючим індексом[/]");
            }
            catch (InvalidProductOperationException ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при введенні даних:[/] {ex.Message}");
                AnsiConsole.MarkupLine("[grey]Будь ласка, введіть коректний номер індексу[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Несподівана помилка при видаленні:[/] {ex.Message}");
                AnsiConsole.MarkupLine("[grey]Зверніться до розробника системи[/]");
            }
        }

        private static void DisplayAllProducts()
        {
            if (products.Count == 0)
            {
                AnsiConsole.Markup("[yellow]Немає доступних продуктів.[/]");
                return;
            }

            AnsiConsole.MarkupLine("\n[green]Оберіть спосіб виведення:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] Звичайний порядок");
            AnsiConsole.MarkupLine("[magenta]2.[/] Зворотній порядок");
            AnsiConsole.MarkupLine("[blue]3.[/] Фільтр за підрядком у назві");
            AnsiConsole.MarkupLine("[yellow]4.[/] Фільтр за префіксом у назві");
            AnsiConsole.MarkupLine("[orange1]5.[/] Відсортований порядок");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-5): [/]");

            int displayChoice;
            while (!int.TryParse(Console.ReadLine(), out displayChoice) || displayChoice < 1 || displayChoice > 5)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-5[/]: ");
            }

            IEnumerable<Product> itemsToDisplay = displayChoice switch
            {
                1 => products,
                2 => products.GetReverseEnumerator(),
                3 => GetFilteredProducts(),
                4 => GetFilteredProductsByPrefix(),
                5 => GetSortedProducts(),
                _ => products
            };

            RenderProductsTable(itemsToDisplay);
        }

        private static IEnumerable<Product> GetFilteredProductsByPrefix()
        {
            AnsiConsole.Markup("[grey]Введіть префікс для пошуку в назві: [/]");
            string prefix = Console.ReadLine();
            return products.FilterByNamePrefix(prefix);
        }

        private static IEnumerable<Product> GetFilteredProducts()
        {
            AnsiConsole.Markup("[grey]Введіть підрядок для пошуку в назві: [/]");
            string substring = Console.ReadLine();
            return products.FilterByNameSubstring(substring);
        }

        private static IEnumerable<Product> GetSortedProducts()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть поле для сортування:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] За ціною");
            AnsiConsole.MarkupLine("[magenta]2.[/] За назвою");
            AnsiConsole.MarkupLine("[blue]3.[/] За типом публікації");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-3): [/]");

            int sortChoice;
            while (!int.TryParse(Console.ReadLine(), out sortChoice) || sortChoice < 1 || sortChoice > 3)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-3[/]: ");
            }

            ProductSortField sortField = sortChoice switch
            {
                1 => ProductSortField.Price,
                2 => ProductSortField.Name,
                3 => ProductSortField.PublicationType,
                _ => ProductSortField.Name
            };

            return products.GetOrderedEnumerator(sortField);
        }


        private static void RenderProductsTable(IEnumerable<Product> itemsToDisplay) {

            if (!itemsToDisplay.Any())
            {
                AnsiConsole.MarkupLine("[yellow]Немає продуктів для відображення.[/]");
                return;
            }

            int rowNumber = 1;
            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.ShowHeaders = true;
            table.ShowFooters = true;
            table.UseSafeBorder = true;
            table.ShowRowSeparators = true;

            table.AddColumn(new TableColumn("[bold white]#[/]").RightAligned().Width(20));
            table.AddColumn(new TableColumn("[bold deepskyblue1]Тип[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold mediumpurple2]Назва[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold green]Ціна[/]").RightAligned().Width(20));
            table.AddColumn(new TableColumn("[bold darkorange]Підкатегорія[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold dodgerblue2]Тип публікації[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold lightgoldenrod2]Автор/Видавництво[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold plum4]Жанр/Тема[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold steelblue1]Періодичність[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold violet]Цільова аудиторія[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold yellow]Наукова цінність[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold aqua]Кількість сторінок[/]").RightAligned().Width(20));
            table.AddColumn(new TableColumn("[bold wheat1]Про особу[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold gold1]Останні відкриття[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold hotpink]Тренди[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold darkseagreen]Предмет[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold yellow]Тематична[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold orange1]Регіон[/]").LeftAligned().Width(20));
            table.AddColumn(new TableColumn("[bold blue]Спеціальність[/]").LeftAligned().Width(20));
            table.Columns[0].LeftAligned();

            foreach (var item in itemsToDisplay)
            {
                string type = item switch
                {
                    Novel => "[grey84]Роман[/]",
                    Biography => "[grey84]Біографія[/]",
                    AstrophysicsMagazine => "[grey84]Астрофізичний журнал[/]",
                    FashionMagazine => "[grey84]Модний журнал[/]",
                    Textbook => "[grey84]Підручник[/]",
                    ShortStoryCollection => "[grey84]Збірка оповідань[/]",
                    TravelMagazine => "[grey84]Туристичний журнал[/]",
                    MedicalMagazine => "[grey84]Медичний журнал[/]",
                    _ => $"[grey84]{item.GetType().Name}[/]"
                };

                string price = "";
                string name = $"[wheat1]{item.Name.EscapeMarkup()}[/]";
                if (item is Product product)
                {
                    price = $"[lime]{product.Price.ToString("C")}[/]";
                }
                string subcategory = "";
                string publicationType = "";
                string authorPublisher = "";
                string genreTopic = "";
                string periodicity = "";
                string targetAudience = "";
                string scientificValue = "";
                string pageCount = "";
                string aboutPerson = "";
                string recentDiscoveries = "";
                string trends = "";
                string subject = "";
                string storyCount = "";
                string primaryRegion = "";
                string medicalSpecialty = "";

                switch (item)
                {
                    case Novel novel:
                        subcategory = $"[lightskyblue1]Художня книга[/]";
                        publicationType = $"[aqua]Книга[/]";
                        authorPublisher = $"[wheat1]{novel.Author.EscapeMarkup()} / {novel.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[orchid]{novel.Genre.EscapeMarkup()}[/]";
                        targetAudience = $"[lightpink1]{novel.TargetAudience.EscapeMarkup()}[/]";
                        pageCount = $"[aqua]{novel.PageCount}[/]";
                        break;

                    case Biography biography:
                        subcategory = $"[lightskyblue1]Наукова книга[/]";
                        publicationType = $"[aqua]Книга[/]";
                        authorPublisher = $"[wheat1]{biography.Author.EscapeMarkup()} / {biography.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[orchid]{biography.Topic.EscapeMarkup()}[/]";
                        scientificValue = GetScientificValueColor(biography.ScientificValue);
                        aboutPerson = $"[wheat1]{biography.AboutPerson.EscapeMarkup()}[/]";
                        break;

                    case AstrophysicsMagazine astroMag:
                        subcategory = $"[lightskyblue1]Науковий журнал[/]";
                        publicationType = $"[mediumpurple2]Журнал[/]";
                        authorPublisher = $"[wheat1]{astroMag.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[orchid]{astroMag.Topic.EscapeMarkup()}[/]";
                        periodicity = $"[steelblue1]{astroMag.Periodicity.EscapeMarkup()}[/]";
                        scientificValue = GetScientificValueColor(astroMag.ScientificValue);
                        recentDiscoveries = $"[gold1]{astroMag.RecentDiscoveries.EscapeMarkup()}[/]";
                        break;

                    case FashionMagazine fashionMag:
                        subcategory = $"[lightskyblue1]Глянцевий журнал[/]";
                        publicationType = $"[mediumpurple2]Журнал[/]";
                        authorPublisher = $"[wheat1]{fashionMag.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[orchid]{fashionMag.Topic.EscapeMarkup()}[/]";
                        periodicity = $"[steelblue1]{fashionMag.Periodicity.EscapeMarkup()}[/]";
                        targetAudience = $"[lightpink1]{fashionMag.TargetAudience.EscapeMarkup()}[/]";
                        trends = $"[hotpink]{fashionMag.Trends.EscapeMarkup()}[/]";
                        break;

                    case Textbook textbook:
                        subcategory = $"[blue]Наукова книга[/]";
                        publicationType = $"[cyan]Книга[/]";
                        authorPublisher = $"[yellow]{textbook.Author.EscapeMarkup()} / {textbook.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[magenta]{textbook.Topic.EscapeMarkup()}[/]";
                        scientificValue = GetScientificValueColor(textbook.ScientificValue);
                        subject = $"[green]{textbook.Subject.EscapeMarkup()}[/]";
                        break;

                    case ShortStoryCollection shortStory:
                        subcategory = $"[blue]Художня книга[/]";
                        publicationType = $"[cyan]Книга[/]";
                        authorPublisher = $"[yellow]{shortStory.Author.EscapeMarkup()} / {shortStory.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[magenta]{shortStory.Genre.EscapeMarkup()}[/]";
                        targetAudience = $"[lightpink1]{shortStory.TargetAudience.EscapeMarkup()}[/]";
                        storyCount = $"[purple]{shortStory.StoryCount}[/]";
                        break;

                    case TravelMagazine travelMag:
                        subcategory = $"[blue]Глянцевий журнал[/]";
                        publicationType = $"[magenta]Журнал[/]";
                        authorPublisher = $"[yellow]{travelMag.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[magenta]{travelMag.Topic.EscapeMarkup()}[/]";
                        periodicity = $"[blue1]{travelMag.Periodicity.EscapeMarkup()}[/]";
                        targetAudience = $"[lightpink1]{travelMag.TargetAudience.EscapeMarkup()}[/]";
                        primaryRegion = $"[orange1]{travelMag.PrimaryRegion.EscapeMarkup()}[/]";
                        break;

                    case MedicalMagazine medicalMag:
                        subcategory = $"[blue]Науковий журнал[/]";
                        publicationType = $"[magenta]Журнал[/]";
                        authorPublisher = $"[yellow]{medicalMag.Publisher.EscapeMarkup()}[/]";
                        genreTopic = $"[magenta]{medicalMag.Topic.EscapeMarkup()}[/]";
                        periodicity = $"[blue1]{medicalMag.Periodicity.EscapeMarkup()}[/]";
                        scientificValue = GetScientificValueColor(medicalMag.ScientificValue);
                        medicalSpecialty = $"[blue]{medicalMag.MedicalSpecialty.EscapeMarkup()}[/]";
                        break;
                }

                table.AddRow(
                    $"[bold white]{rowNumber++}[/]",
                    type, name, price, subcategory, publicationType, authorPublisher,
                    genreTopic, periodicity, targetAudience, scientificValue, pageCount,
                    aboutPerson, recentDiscoveries,trends, subject, storyCount, primaryRegion, medicalSpecialty
                );
            }
            table.Width = 200;
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

        private static void SaveContainerToFile()
        {
            AnsiConsole.Markup("[grey]Введіть шлях до файлу: [/]");
            string path = Console.ReadLine();
            try
            {
                ContainerSerializer.SerializeToFile(products, path);
                AnsiConsole.MarkupLine("[green]Контейнер збережено![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при збереженні: {ex.Message}[/]");
            }
        }

        private static void LoadContainerFromFile()
        {
            AnsiConsole.Markup("[grey]Введіть шлях до файлу: [/]");
            string path = Console.ReadLine();
            try
            {
                products = ContainerSerializer.DeserializeFromFile<Product>(path);
                AnsiConsole.MarkupLine("[green]Контейнер завантажено![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при завантаженні: {ex.Message}[/]");
            }
        }

        private static void DemonstrateDelegatesAndMethods()
        {
            if (products.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Контейнер порожній. Додайте продукти спочатку.[/]");
                return;
            }

            while (true)
            {
                AnsiConsole.MarkupLine("\n[underline green]Демонстрація роботи делегатів:[/]");
                AnsiConsole.MarkupLine("[cyan]1.[/] Сортування за ціною");
                AnsiConsole.MarkupLine("[magenta]2.[/] Пошук першого продукту за ціною");
                AnsiConsole.MarkupLine("[blue]3.[/] Пошук всіх продуктів за підрядком у назві");
                AnsiConsole.MarkupLine("[red]4.[/] Повернутися до головного меню");
                AnsiConsole.Markup("[grey]Введіть ваш вибір (1-4): [/]");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
                {
                    AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-4[/]: ");
                }

                switch (choice)
                {
                    case 1:
                        HandleSorting();
                        break;
                    case 2:
                        HandlePriceSearch();
                        break;
                    case 3:
                        HandleNameSearch();
                        break;
                    case 4:
                        return;
                }
            }
        }

        private static void HandleSorting()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть напрямок сортування:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] За зростанням ціни");
            AnsiConsole.MarkupLine("[magenta]2.[/] За спаданням ціни");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int sortChoice;
            while (!int.TryParse(Console.ReadLine(), out sortChoice) || sortChoice < 1 || sortChoice > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            products.Sort((x, y) => sortChoice == 1
                ? x.Price.CompareTo(y.Price)
                : y.Price.CompareTo(x.Price));

            AnsiConsole.MarkupLine("[green]Відсортовані продукти:[/]");
            RenderProductsTable(products);
        }

        private static void HandlePriceSearch()
        {
            AnsiConsole.MarkupLine("\n[green]Оберіть тип пошуку:[/]");
            AnsiConsole.MarkupLine("[cyan]1.[/] Ціна більше ніж");
            AnsiConsole.MarkupLine("[magenta]2.[/] Ціна менше ніж");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-2): [/]");

            int searchType;
            while (!int.TryParse(Console.ReadLine(), out searchType) || searchType < 1 || searchType > 2)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-2[/]: ");
            }

            AnsiConsole.Markup("[grey]Введіть ціну для порівняння: [/]");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть число: ");
            }

            ProductContainerBase<Product>.PredicateDelegate predicate = searchType == 1
                        ? p => p.Price > price
                        : p => p.Price < price;

            Product found = products.Find(predicate);
            if (found != null)
            {
                AnsiConsole.MarkupLine($"[green]Знайдено продукт:[/]");
                DisplayProductDetails(found);
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Продукти за вказаними критеріями не знайдено.[/]");
            }
        }

        private static void ShowTotalPrice()
        {
            AnsiConsole.MarkupLine($"[green]Загальна вартість товарів у контейнері: {products.TotalPrice:C}[/]");
        }

        private static void HandleNameSearch()
        {
            AnsiConsole.Markup("[grey]Введіть підрядок для пошуку в назві: [/]");
            string substring = Console.ReadLine();

            var results = products.FindAll(p => p.Name.Contains(substring, StringComparison.OrdinalIgnoreCase));

            if (results.Any())
            {
                AnsiConsole.MarkupLine($"[green]Знайдено {results.Count()} продуктів:[/]");
                RenderProductsTable(results);
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Продукти за вказаними критеріями не знайдено.[/]");
            }
        }


    }
}
