using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;
using Spectre.Console.Rendering;
using Лаб_4;
class Program
{
    static void Main(string[] args)
    {
        System.Console.OutputEncoding = System.Text.Encoding.Unicode;
        System.Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.SetWindowSize(240, 40);
        main_menu.Main_menu();

    }

}

public interface IProductContainer
{
    void Add(Product item); // Додає в кінець (вже було)
    void AddToBeginning(Product item); // Додає на початок
    void InsertAt(int index, Product item); // Додає за індексом
    void RemoveAt(int index); // Вже було
    void OrderByPrice(); // Вже було
    Product this[int index] { get; set; } // Вже було
    Product[] this[decimal price] { get;set; }
    Product this[string name] { get; set; }
    int Count { get; } // Вже було
    void OrderBy(ProductSortField sortField);

}

public enum ProductSortField
{
    Price,
    Name,
    PublicationType
}
