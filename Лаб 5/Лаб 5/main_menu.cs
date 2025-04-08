using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Rendering;
using static Program;

namespace Лаб_4
{
    class main_menu
    {
        private static IProductContainer products = new Container();
        public static void Main_menu()
        {

            while (true)
            {
                AnsiConsole.MarkupLine("\n[green]Головне меню:[/]");
                AnsiConsole.MarkupLine("[cyan]1.[/] Додати продукт");
                AnsiConsole.MarkupLine("[magenta]2.[/] Згенерувати випадкові продукти");
                AnsiConsole.MarkupLine("[blue]3.[/] Переглянути всі продукти");
                AnsiConsole.MarkupLine("[yellow]4.[/] Видалити продукт за індексом");
                AnsiConsole.MarkupLine("[orange1]5.[/] Відсортувати продукти");
                AnsiConsole.MarkupLine("[purple]6.[/] Змінити тип контейнера (поточний: " + (products is Container ? "Масив" : "Двозв'язний список") + ")");
                AnsiConsole.MarkupLine("[cyan]7.[/] Пошук продукту");
                AnsiConsole.MarkupLine("[magenta]8.[/] Редагування продукту");
                AnsiConsole.MarkupLine("[red]9.[/] Вийти");
                AnsiConsole.Markup("[grey]Введіть ваш вибір (1-9): [/]");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 9)
                {
                    AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-9[/]: ");
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
                        ChangeContainerType();
                        break;
                    case 7:
                        SearchProductMenu();
                        break;
                    case 8:
                        EditProductMenu();
                        break;
                    case 9:
                        AnsiConsole.MarkupLine("[yellow]До побачення![/]");
                        return;
                }
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
            AnsiConsole.MarkupLine("[blue]3.[/] За ціною");
            AnsiConsole.Markup("[grey]Введіть ваш вибір (1-3): [/]");

            int searchChoice;
            while (!int.TryParse(Console.ReadLine(), out searchChoice) || searchChoice < 1 || searchChoice > 3)
            {
                AnsiConsole.Markup("[red]Некоректне введення.[/] Будь ласка, введіть [yellow]1-3[/]: ");
            }

            try
            {
                switch (searchChoice)
                {
                    case 1:
                        AnsiConsole.Markup("[grey]Введіть індекс: [/]");
                        int index = int.Parse(Console.ReadLine());
                        var productByIndex = products[index - 1];
                        AnsiConsole.MarkupLine("[green]Знайдено продукт:[/]");
                        DisplayProductDetails(productByIndex);
                        break;
                    case 2:
                        AnsiConsole.Markup("[grey]Введіть назву: [/]");
                        string name = Console.ReadLine();
                        var productByName = products[name];
                        AnsiConsole.MarkupLine("[green]Знайдено продукт:[/]");
                        DisplayProductDetails(productByName);
                        break;
                    case 3:
                        AnsiConsole.Markup("[grey]Введіть ціну: [/]");
                        decimal price = decimal.Parse(Console.ReadLine());
                        var productsByPrice = products[price];
                        AnsiConsole.MarkupLine($"[green]Знайдено продуктів: {productsByPrice.Length}[/]");
                        foreach (var product in productsByPrice)
                        {
                            DisplayProductDetails(product);
                            AnsiConsole.MarkupLine("──────────────────────────────");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка пошуку:[/] {ex.Message}");
            }
        }

        private static void EditProductMenu()
        {
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
                Product productToEdit = null;
                switch (searchChoice)
                {
                    case 1:
                        AnsiConsole.Markup("[grey]Введіть індекс: [/]");
                        int index = int.Parse(Console.ReadLine());
                        productToEdit = products[index - 1];
                        break;
                    case 2:
                        AnsiConsole.Markup("[grey]Введіть назву: [/]");
                        string name = Console.ReadLine();
                        productToEdit = products[name];
                        break;
                }

                if (productToEdit != null)
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

        private static void DisplayProductDetails(Product product)
        {

            switch (product)
            {
                case MedicalMagazine medical:
                    AnsiConsole.MarkupLine($"Товар: [bold]{medical.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{medical.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Періодичність: [bold]{medical.Periodicity.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{medical.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Тема: [bold]{medical.Topic.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Наукова цінність: [bold]{medical.ScientificValue.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Спеціальність: [bold]{medical.MedicalSpecialty.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Рецензований: [bold]{(medical.IsPeerReviewed ? "Так" : "Ні")}[/]");
                    break;

                case Novel novel:
                    AnsiConsole.MarkupLine($"Товар: [bold]{novel.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{novel.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Автор: [bold]{novel.Author.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{novel.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Жанр: [bold]{novel.Genre.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Аудиторія: [bold]{novel.TargetAudience.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Сторінок: [bold]{novel.PageCount}[/],");
                    AnsiConsole.MarkupLine($"Стиль: [bold]{novel.NarrativeStyle.EscapeMarkup()}[/]");
                    break;

                case Biography biography:
                    AnsiConsole.MarkupLine($"Товар: [bold]{biography.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{biography.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Автор: [bold]{biography.Author.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{biography.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Про особу: [bold]{biography.AboutPerson.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Структура: [bold]{biography.Structure.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Наукова цінність: [bold]{biography.ScientificValue.EscapeMarkup()}[/]");
                    break;

                case AstrophysicsMagazine astroMag:
                    AnsiConsole.MarkupLine($"Товар: [bold]{astroMag.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{astroMag.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Періодичність: [bold]{astroMag.Periodicity.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{astroMag.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Тема: [bold]{astroMag.Topic.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Наукова цінність: [bold]{astroMag.ScientificValue.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Відкриття: [bold]{astroMag.RecentDiscoveries.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Дослідження: [bold]{astroMag.AuthoritativeResearch.EscapeMarkup()}[/]");
                    break;

                case FashionMagazine fashionMag:
                    AnsiConsole.MarkupLine($"Товар: [bold]{fashionMag.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{fashionMag.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Періодичність: [bold]{fashionMag.Periodicity.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{fashionMag.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Тема: [bold]{fashionMag.Topic.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Аудиторія: [bold]{fashionMag.TargetAudience.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Тренди: [bold]{fashionMag.Trends.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Інтерв'ю: [bold]{fashionMag.ExclusiveInterviews.EscapeMarkup()}[/],");
                    break;

                case Textbook textbook:
                    AnsiConsole.MarkupLine($"Товар: [bold]{textbook.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{textbook.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Автор: [bold]{textbook.Author.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{textbook.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Предмет: [bold]{textbook.Subject.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Клас: [bold]{textbook.GradeLevel.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Наукова цінність: [bold]{textbook.ScientificValue.EscapeMarkup()}[/]");
                    break;

                case ShortStoryCollection storyCollection:
                    AnsiConsole.MarkupLine($"Товар: [bold]{storyCollection.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{storyCollection.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Автор: [bold]{storyCollection.Author.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{storyCollection.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Жанр: [bold]{storyCollection.Genre.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Кількість оповідань: [bold]{storyCollection.StoryCount}[/],");
                    AnsiConsole.MarkupLine($"Тематична: [bold]{(storyCollection.IsThematic ? "Так" : "Ні")}[/],");
                    AnsiConsole.MarkupLine($"Аудиторія: [bold]{storyCollection.TargetAudience.EscapeMarkup()}[/]");
                    break;

                case TravelMagazine travelMag:
                    AnsiConsole.MarkupLine($"Товар: [bold]{travelMag.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{travelMag.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Періодичність: [bold]{travelMag.Periodicity.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Видавництво: [bold]{travelMag.Publisher.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Регіон: [bold]{travelMag.PrimaryRegion.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Путівник: [bold]{(travelMag.HasTravelGuides ? "Так" : "Ні")}[/],");
                    AnsiConsole.MarkupLine($"Аудиторія: [bold]{travelMag.TargetAudience.EscapeMarkup()}[/]");
                    break;

                default:
                    AnsiConsole.MarkupLine($"Товар: [bold]{product.Name.EscapeMarkup()}[/],");
                    AnsiConsole.MarkupLine($"Ціна: [bold]{product.Price} грн[/],");
                    AnsiConsole.MarkupLine($"Тип: [bold]{product.GetType().Name.EscapeMarkup()}[/]");
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
                    products.InsertAt(0, product); // Вставляємо на індекс 0
                    AnsiConsole.MarkupLine("[green]Продукт успішно доданий на початок ![/]");
                    break;
                case 2:
                    products.InsertAt(products.Count, product); // Вставляємо в кінець
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

            int index = position - 1; // Конвертуємо позицію в індекс
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
                Product product = RandomProductGenerator.GenerateRandomProduct();
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

            // Створюємо новий контейнер обраного типу
            IProductContainer newContainer = choice == 1 ? new Container() : new LinkedListContainer();

            // Копіюємо всі продукти зі старого контейнера в новий
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

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > products.Count)
            {
                AnsiConsole.Markup($"[red]Некоректне введення.[/] Будь ласка, введіть число від [yellow]1 до {products.Count}[/]: ");
            }

            try
            {
                // Віднімаємо 1 від введеного індексу, щоб отримати правильний індекс у списку
                Product removedProduct = products[index - 1];
                products.RemoveAt(index - 1);
                AnsiConsole.MarkupLine($"[green]Продукт видалено:[/] {removedProduct.Name}");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Помилка при видаленні:[/] {ex.Message}");
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
            table.AddColumn(new TableColumn("[bold white]#[/]").RightAligned()); // Новий стовпець з номером
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

            for (int i = 0; i < products.Count; i++)
            {
                Product product = products[i];
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
                string storyCount = "";
                string isThematic = "";
                string primaryRegion = "";
                string hasTravelGuides = "";
                string medicalSpecialty = "";
                string isPeerReviewed = "";

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
                    $"[bold white]{i + 1}[/]", // Додано номер продукту (починаючи з 1)
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
}
