using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
#pragma warning disable SYSLIB0011

namespace Лаб_4
{
    public class Container<T> : ProductContainerBase<T>, IEnumerable<T> where T : IName<T>
    {
        private T[] items;
        private int count;

        public Container()
        {
            items = new T[4];
            count = 0;
        }

        // Реалізація IEnumerable<T>
        public override IEnumerator<T> GetEnumerator()
        {
            return new ContainerIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerable<T> GetReverseEnumerator()
        {
            for (int i = count - 1; i >= 0; i--)
            {
                yield return items[i];
            }
        }

        public override IEnumerable<T> FilterByNameSubstring(string substring)
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i].Name.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    yield return items[i];
                }
            }
        }

        public override IEnumerable<T> GetOrderedEnumerator(ProductSortField sortField)
        {
            var sorted = items.Take(count).ToList();
            sorted.Sort((x, y) =>
            {
                return sortField switch
                {
                    ProductSortField.Price => (x as Product)?.Price.CompareTo((y as Product)?.Price) ?? 0,
                    ProductSortField.Name => string.Compare(x.Name, y.Name, StringComparison.Ordinal),
                    ProductSortField.PublicationType => GetPublicationTypeOrder(x).CompareTo(GetPublicationTypeOrder(y)),
                    _ => 0
                };
            });

            foreach (var item in sorted)
            {
                yield return item;
            }
        }

        private class ContainerIterator : IEnumerator<T>
        {
            private readonly Container<T> _container;
            private int _currentIndex = -1;

            public ContainerIterator(Container<T> container)
            {
                _container = container;
            }

            public T Current
            {
                get
                {
                    if (_currentIndex == -1 || _currentIndex >= _container.count)
                        throw new InvalidOperationException();
                    return _container.items[_currentIndex];
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                _currentIndex++;
                return _currentIndex < _container.count;
            }

            public void Reset()
            {
                _currentIndex = -1;
            }

            public void Dispose()
            {
                // Нічого не потрібно звільняти
            }
        }

        public override void Clear()
        {
            Array.Clear(items, 0, count);
            count = 0;
            Resize(4); // Повертаємо до початкового розміру
        }

        public override void AddToBeginning(T item)
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

        public override void InsertAt(int index, T item)
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

        public override void Add(T item)
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
                T removedProduct = items[index];

                for (int i = index; i < count - 1; i++)
                {
                    items[i] = items[i + 1];
                }

                items[count - 1] = default(T)!;
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
                            T temp = items[j];
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

            T[] itemsToSort = new T[count];
            Array.Copy(items, itemsToSort, count);

            Comparison<T> comparison = sortField switch
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
            T[] newArray = new T[newSize];
            int elementsToCopy = Math.Min(newSize, count);
            for (int i = 0; i < elementsToCopy; i++)
            {
                newArray[i] = items[i];
            }
            items = newArray;
        }

        public override T this[int index]
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

        public override T this[string name]
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

        public override void SaveToFile(string filePath)
        {
            try
            {
                T[] dataToSave = items.Take(count).ToArray();
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, dataToSave);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override void LoadFromFile(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    T[] loadedItems = (T[])formatter.Deserialize(fs);
                    items = new T[loadedItems.Length];
                    Array.Copy(loadedItems, items, loadedItems.Length);
                    count = loadedItems.Length;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override int Count => count;
    }
}
