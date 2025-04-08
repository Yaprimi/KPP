using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Container : IProductContainer
    {
        private Product[] items;
        private int count;

        public Container()
        {
            items = new Product[4]; // Initial capacity
            count = 0;
        }

        public void AddToBeginning(Product item)
        {
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }

            // Зсуваємо всі елементи вправо
            for (int i = count; i > 0; i--)
            {
                items[i] = items[i - 1];
            }

            items[0] = item;
            count++;
        }

        public void InsertAt(int index, Product item)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }

            // Зсуваємо елементи праворуч від індексу
            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            items[index] = item;
            count++;
        }

        // Add an item to the container
        public void Add(Product item)
        {
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }
            items[count] = item;
            count++;
        }

        // Remove an item at the specified index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            // Shift all elements after the index to the left
            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            items[count - 1] = null; // Clear last reference
            count--;

            // Shrink array if it's too empty
            if (count > 0 && count == items.Length / 4)
            {
                Resize(items.Length / 2);
            }
        }

        // Order the items by price (ascending)
        public void OrderByPrice()
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (items[j].Price > items[j + 1].Price)
                    {
                        Product temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }

        public void OrderBy(ProductSortField sortField)
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    bool shouldSwap = false;

                    switch (sortField)
                    {
                        case ProductSortField.Price:
                            shouldSwap = items[j].Price > items[j + 1].Price;
                            break;
                        case ProductSortField.Name:
                            shouldSwap = string.Compare(items[j].Name, items[j + 1].Name, StringComparison.Ordinal) > 0;
                            break;
                        case ProductSortField.PublicationType:
                            shouldSwap = GetPublicationTypeOrder(items[j]) > GetPublicationTypeOrder(items[j + 1]);
                            break;
                    }

                    if (shouldSwap)
                    {
                        Product temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
        }
        private int GetPublicationTypeOrder(Product product)
        {
            return product switch
            {
                Book => 1,
                Magazine => 2,
                _ => 3
            };
        }


        // Convert container contents to string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Container with {count} items:");
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"[{i}] {items[i]}");
            }
            return sb.ToString();
        }

        // Helper method to resize the internal array
        private void Resize(int newSize)
        {
            Product[] newArray = new Product[newSize];
            int elementsToCopy = Math.Min(newSize, count);
            for (int i = 0; i < elementsToCopy; i++)
            {
                newArray[i] = items[i];
            }
            items = newArray;
        }

        // Indexer to access items by index
        public Product this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі колекції");
                }
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                {
                    throw new IndexOutOfRangeException("Індекс виходить за межі колекції");
                }
                items[index] = value;
            }
        }

        public Product this[string назва]
        {
            get
            {
                for (int i = 0; i < count; i++)
                {
                    if (items[i].Name.Equals(назва, StringComparison.OrdinalIgnoreCase))
                    {
                        return items[i];
                    }
                }
                throw new KeyNotFoundException($"Товар з назвою '{назва}' не знайдено");
            }
        }

        // Новий індексатор за ціною
        public IEnumerable<Product> this[decimal ціна]
        {
            get
            {
                List<Product> результат = new List<Product>();
                for (int i = 0; i < count; i++)
                {
                    if (items[i].Price == ціна)
                    {
                        результат.Add(items[i]);
                    }
                }
                if (результат.Count == 0)
                {
                    throw new KeyNotFoundException($"Товарів з ціною {ціна} не знайдено");
                }
                return результат;
            }
        }

        // Property to get the count of items
        public int Count => count;
    }
}
