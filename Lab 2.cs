using System;

//6. Ввести цілий двовимірний рваний масив ( jagged array ), що складається з
//рядків довільної довжини. Видалити в ньому рядки, всі елементи яких є
//позитивними, а після кожного рядка, елементи якого впорядковані за
//зростанням, вставити його дзеркальне відображення. Для перевірки рядків
//реалізувати окремі функції.

class Program
{
    static bool IsAllPositive(int[] row)
    {
        for (int i = 0; i < row.Length; i++)
        {
            if (row[i] <= 0)
                return false;
        }
        return true;
    }

    static bool IsSortedAscending(int[] row)
    {
        for (int i = 1; i < row.Length; i++)
        {
            if (row[i] < row[i - 1])
                return false;
        }
        return true;
    }

    static int[] ReverseRow(int[] row)
    {
        int[] reversed = new int[row.Length];
        for (int i = 0; i < row.Length; i++)
        {
            reversed[i] = row[row.Length - 1 - i];
        }
        return reversed;
    }

    static int[][] ProcessJaggedArray(int[][] jaggedArray)
    {
        int newSize = 0;
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (!IsAllPositive(jaggedArray[i]))
                newSize++;
            if (IsSortedAscending(jaggedArray[i]) && jaggedArray[i].Length > 1) // Перевірка довжини
                newSize++;
        }

        int[][] result = new int[newSize][];
        int index = 0;

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (!IsAllPositive(jaggedArray[i]))
            {
                result[index++] = jaggedArray[i];
                if (IsSortedAscending(jaggedArray[i]) && jaggedArray[i].Length > 1) // Перевірка довжини
                {
                    result[index++] = ReverseRow(jaggedArray[i]);
                }
            }
        }

        return result;
    }


    static void PrintJaggedArray(int[][] jaggedArray)
    {
        if (jaggedArray == null)
        {
            Console.WriteLine("Масив не ініціалізований!");
            return;
        }

        int maxLength = 0;
        foreach (var row in jaggedArray)
        {
            if (row != null)
            {
                foreach (var num in row)
                {
                    maxLength = Math.Max(maxLength, num.ToString().Length);
                }
            }
        }

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (jaggedArray[i] == null) continue;

            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(jaggedArray[i][j].ToString().PadLeft(maxLength + 1) + " ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }



    static int[][] GenerateRandomJaggedArray()
    {
        Random rand = new Random();
        Console.Write("Введіть кількість рядків: ");
        int rows = int.Parse(Console.ReadLine());
        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            int length = rand.Next(1, 10);
            jaggedArray[i] = new int[length];
            for (int j = 0; j < length; j++)
            {
                jaggedArray[i][j] = rand.Next(-10, 10);
            }
        }

        return jaggedArray;
    }

    static int[][] ManualInputJaggedArray()
    {
        Console.Write("Введіть кількість рядків: ");
        int rows = int.Parse(Console.ReadLine());
        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Введіть довжину рядка {i + 1}: ");
            int length = int.Parse(Console.ReadLine());
            jaggedArray[i] = new int[length];

            Console.WriteLine($"Введіть {length} елементів для рядка {i + 1}:");
            for (int j = 0; j < length; j++)
            {
                jaggedArray[i][j] = int.Parse(Console.ReadLine());
            }
        }
        return jaggedArray;
    }

    static int[][] PredefinedJaggedArray()
    {
        return new int[][]
        {
            new int[] { 1, 2, 3, 4, 5 },
            new int[] { -1, -2, -3 },
            new int[] { -10, -9, -8, -7, -6 },
            new int[] { 5, -5, 10, -10 },
            new int[] { 3, 1, 4, 1, 5, 9 }
        };
    }

    static int[][] Choise()
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Оберіть спосіб введення масиву:");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1. Ручний ввід");
            Console.WriteLine("2. Генерація випадкових значень");
            Console.WriteLine("3. Використати заготовлений масив");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ваш вибір: ");
            Console.ResetColor();
            string choice = Console.ReadLine();

            if (choice == "1")
                return ManualInputJaggedArray();
            else if (choice == "2")
                return GenerateRandomJaggedArray();
            else if (choice == "3")
                return PredefinedJaggedArray();
            else
                Console.WriteLine("Невірний вибір, спробуйте ще раз.");
        }
    }

    static void PrintColoredJaggedArray(int[][] jaggedArray)
    {
        if (jaggedArray == null)
        {
            Console.WriteLine("Масив не ініціалізований!");
            return;
        }

        int maxLength = 0;
        foreach (var row in jaggedArray)
        {
            if (row != null)
            {
                foreach (var num in row)
                {
                    maxLength = Math.Max(maxLength, num.ToString().Length);
                }
            }
        }

        for (int i = 0; i < jaggedArray.Length; i++)
        {
            if (jaggedArray[i] == null) continue;

            // Вибір кольору на основі умов
            if (IsAllPositive(jaggedArray[i]))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (IsSortedAscending(jaggedArray[i]) && jaggedArray[i].Length > 1)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write(jaggedArray[i][j].ToString().PadLeft(maxLength + 1) + " ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    static void Menu() {
        do
        {
            int[][] jaggedArray = Choise();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nВихідний масив:");
            Console.ResetColor();
            PrintJaggedArray(jaggedArray);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nВихідний масив (з виділенням операцій)\n" +
                "(Червоний - рядок, що буде видалено, Жовтий - рядок, що буде вставлений у дзеркальному відображені):");
            Console.ResetColor();
            PrintColoredJaggedArray(jaggedArray);

            int[][] processedArray = ProcessJaggedArray(jaggedArray);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nОброблений масив:");
            Console.ResetColor();
            PrintJaggedArray(processedArray);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nБажаєте повторити роботу програми? (y/n): ");
            Console.ResetColor();
        } while (Console.ReadLine()?.ToLower() == "y");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nДякуємо за використання програми!");
        Console.ResetColor();
    }

    static void Main()
    {
        System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        System.Console.InputEncoding = System.Text.Encoding.Unicode;

        Menu();

    }

}
