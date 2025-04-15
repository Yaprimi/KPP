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
    void Add(Product item);
    void AddToBeginning(Product item);
    void InsertAt(int index, Product item);
    void RemoveAt(int index);
    void OrderByPrice();
    Product this[int index] { get; set; }
    Product[] this[decimal price] { get;set; }
    Product this[string name] { get; set; }
    int Count { get; }
    void OrderBy(ProductSortField sortField);

}

public enum ProductSortField
{
    Price,
    Name,
    PublicationType
}
