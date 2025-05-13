using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

        public override void Clear()
        {
            Array.Clear(items, 0, count);
            count = 0;
            Resize(4); // Повертаємо до початкового розміру
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

        public override void SaveToBinaryFile(string filePath)
        {
            using (var stream = File.Create(filePath))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(count);
                foreach (var item in items.Take(count))
                {
                    SerializeItem(writer, item);
                }
            }
        }

        public override void LoadFromBinaryFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            using (var reader = new BinaryReader(stream))
            {
                int itemCount = reader.ReadInt32();
                items = new T[itemCount];
                count = 0;

                for (int i = 0; i < itemCount; i++)
                {
                    T item = DeserializeItem(reader);
                    Add(item);
                }
            }
        }

        private void SerializeItem(BinaryWriter writer, T item)
        {
            string typeName = item.GetType().AssemblyQualifiedName;
            writer.Write(typeName);

            var fields = item.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite);

            writer.Write(fields.Length + properties.Count());

            foreach (var field in fields)
            {
                writer.Write(field.Name);
                WriteValue(writer, field.GetValue(item));
            }

            foreach (var prop in properties)
            {
                writer.Write(prop.Name);
                WriteValue(writer, prop.GetValue(item));
            }
        }

        private T DeserializeItem(BinaryReader reader)
        {
            string typeName = reader.ReadString();
            Type type = Type.GetType(typeName);
            if (type == null)
                throw new InvalidOperationException($"Тип {typeName} не знайдено.");

            object obj = Activator.CreateInstance(type);

            int memberCount = reader.ReadInt32();
            for (int i = 0; i < memberCount; i++)
            {
                string memberName = reader.ReadString();
                var field = type.GetField(memberName);
                if (field != null)
                {
                    object value = ReadValue(reader, field.FieldType);
                    field.SetValue(obj, value);
                }
                else
                {
                    var prop = type.GetProperty(memberName);
                    if (prop != null && prop.CanWrite)
                    {
                        object value = ReadValue(reader, prop.PropertyType);
                        prop.SetValue(obj, value);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Член {memberName} не знайдено.");
                    }
                }
            }

            return (T)obj;
        }

        private void WriteValue(BinaryWriter writer, object value)
        {
            switch (value)
            {
                case int intVal:
                    writer.Write(intVal);
                    break;
                case decimal decimalVal:
                    writer.Write(decimalVal);
                    break;
                case string stringVal:
                    writer.Write(stringVal);
                    break;
                case bool boolVal:
                    writer.Write(boolVal);
                    break;
                // Додайте інші типи за необхідності
                default:
                    throw new NotSupportedException($"Тип {value.GetType()} не підтримується.");
            }
        }

        private object ReadValue(BinaryReader reader, Type type)
        {
            if (type == typeof(int)) return reader.ReadInt32();
            if (type == typeof(decimal)) return reader.ReadDecimal();
            if (type == typeof(string)) return reader.ReadString();
            if (type == typeof(bool)) return reader.ReadBoolean();
            // Додайте інші типи
            throw new NotSupportedException($"Тип {type} не підтримується.");
        }

        public override int Count => count;
    }
}
