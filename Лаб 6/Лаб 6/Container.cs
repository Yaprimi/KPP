using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Container : ProductContainerBase
    {
        private Product[] items;
        private int count;

        public Container()
        {
            items = new Product[4];
            count = 0;
        }

        public override void AddToBeginning(Product item)
        {
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }

            for (int i = count; i > 0; i--)
            {
                items[i] = items[i - 1];
            }

            items[0] = item;
            count++;
        }

        public override void InsertAt(int index, Product item)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }

            for (int i = count; i > index; i--)
            {
                items[i] = items[i - 1];
            }

            items[index] = item;
            count++;
        }

        public override void Add(Product item)
        {
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }
            items[count] = item;
            count++;
        }

        public override void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ProductNotFoundException(
                    $"Індекс {index + 1} виходить за межі діапазону (1.. {count})",
                    null,
                    "Remove at index");
            }

            try
            {
                Product removedProduct = items[index];

                for (int i = index; i < count - 1; i++)
                {
                    items[i] = items[i + 1];
                }

                items[count - 1] = null;
                count--;

                if (count > 0 && count == items.Length / 4)
                {
                    Resize(items.Length / 2);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidProductOperationException(
                    "Помилка при видаленні елементу з масиву",
                    index < items.Length ? items[index] : null,
                    "Remove at index");
            }
        }

        public override void OrderByPrice()
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

        public override void OrderBy(ProductSortField sortField)
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

        public override Product this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new ProductNotFoundException($"Індекс {index + 1} виходить за межі діапазону (1.. {count})", null, "Container index access");
                return items[index];
            }
            set
            {
                if (index < 0 || index >= count)
                    throw new ProductNotFoundException($"Індекс {index + 1} виходить за межі діапазону (1.. {count})", value, "Container index assignment");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Продукт не може бути null");
                items[index] = value;
            }
        }

        public override Product this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    throw new InvalidProductOperationException("Назва не може бути порожньою", null, "Product search by name");

                var product = items.FirstOrDefault(p => p?.Name.Equals(name, StringComparison.OrdinalIgnoreCase) == true);
                if (product == null)
                    throw new ProductNotFoundException($"Продукт з назвою '{name}' не знайдено", null, "Product search by name");
                return product;
            }
            set
            {
                if (string.IsNullOrEmpty(name))
                    throw new InvalidProductOperationException("Назва не може бути порожньою", value, "Product update by name");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Продукт не може бути null");

                bool found = false;
                for (int i = 0; i < count; i++)
                {
                    if (items[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        items[i] = value;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new ProductNotFoundException($"Продукт з назвою '{name}' не знайдено", value, "Product update by name");
            }
        }

        public override Product[] this[decimal price]
        {
            get
            {
                if (price < 0)
                    throw new ArgumentException("Ціна не може бути від'ємною");

                var matchingProducts = items.Take(count).Where(p => p.Price == price).ToArray();
                if (matchingProducts.Length == 0)
                    throw new KeyNotFoundException($"Продукти з ціною {price} не знайдено");
                return matchingProducts;
            }
            set
            {
                if (price < 0)
                    throw new ArgumentException("Ціна не може бути від'ємною");
                if (value == null || value.Any(p => p == null))
                    throw new ArgumentNullException(nameof(value), "Список продуктів не може містити null");

                bool anyReplaced = false;
                for (int i = 0; i < count; i++)
                {
                    if (items[i].Price == price)
                    {
                        if (value.Length > 0)
                        {
                            items[i] = value[0];
                            value = value.Skip(1).ToArray();
                            anyReplaced = true;
                        }
                    }
                }

                if (!anyReplaced)
                    throw new KeyNotFoundException($"Продукти з ціною {price} не знайдено");
            }
        }

        public  Product[] GetAllByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty");

            var matchingProducts = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                if (items[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    matchingProducts.Add(items[i]);
            }

            return matchingProducts.ToArray();
        }

        public override int Count => count;
    }
}
