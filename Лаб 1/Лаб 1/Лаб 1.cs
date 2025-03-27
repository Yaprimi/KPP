using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //Варіант 9.
        // Нехай надано рядок, що складається зі слів, відокремлених одне від
        // одного одним і більше пропусками.Визначити довжину найкоротшого
        // із слів, що містяться в ньому.

        System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        System.Console.InputEncoding = System.Text.Encoding.Unicode;

        Console.WriteLine("Введіть рядок:");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Рядок порожній або містить лише пробіли.");
            return;
        }

        int minLength = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                             .Min(word => word.Length);

        Console.WriteLine($"Довжина найкоротшого слова чи слів: {minLength}");
    }
}
