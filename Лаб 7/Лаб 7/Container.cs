using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаб_4
{
    public class Container : ProductContainerBase
    {
        private IName[] items;
        private int count;

        public Container()
        {
            items = new IName[4];
            count = 0;
        }

        public override void AddToBeginning(IName item)
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

        public override void InsertAt(int index, IName item)
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

        public override void Add(IName item)
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
                IName removedProduct = items[index];

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
                    index < items.Length ? items[index] as Product : null,
                    "Remove at index");
            }
        }

        public override void OrderByPrice()
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (items[j] is Product left && items[j + 1] is Product right)
                    {
                        if (left.Price > right.Price)
                        {
                            IName temp = items[j];
                            items[j] = items[j + 1];
                            items[j + 1] = temp;
                        }
                    }
                }
            }
        }

        public override void OrderBy(ProductSortField sortField)
        {
            if (count <= 1) return;

            IName[] itemsToSort = new IName[count];
            Array.Copy(items, itemsToSort, count);

            Comparison<IName> comparison = sortField switch
            {
                ProductSortField.Price => (x, y) =>
                {
                    if (x is Product xProduct && y is Product yProduct)
                        return xProduct.Price.CompareTo(yProduct.Price);
                    return 0;
                }
                ,
                ProductSortField.Name => (x, y) =>
                    string.Compare(x.Name, y.Name, StringComparison.Ordinal),
                ProductSortField.PublicationType => (x, y) =>
                    GetPublicationTypeOrder(x).CompareTo(GetPublicationTypeOrder(y)),
                _ => (x, y) => 0
            };

            Array.Sort(itemsToSort, comparison);

            Array.Copy(itemsToSort, items, count);
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
            IName[] newArray = new IName[newSize];
            int elementsToCopy = Math.Min(newSize, count);
            for (int i = 0; i < elementsToCopy; i++)
            {
                newArray[i] = items[i];
            }
            items = newArray;
        }

        public override IName this[int index]
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
                    throw new ProductNotFoundException($"Індекс {index + 1} виходить за межі діапазону (1.. {count})", value as Product, "Container index assignment");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Продукт не може бути null");
                items[index] = value;
            }
        }

        public override IName this[string name]
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
                    throw new InvalidProductOperationException("Назва не може бути порожньою", value as Product, "Product update by name");
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
                    throw new ProductNotFoundException($"Продукт з назвою '{name}' не знайдено", value as Product, "Product update by name");
            }
        }

        public override int Count => count;
    }
}
