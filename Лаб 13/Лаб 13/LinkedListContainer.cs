using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Лаб_4
{
    public class LinkedListContainer<T> : ProductContainerBase<T>, IEnumerable<T> where T : class, IName<T>

    {
        private decimal _totalPrice;
        public override decimal TotalPrice => _totalPrice;

        private void UpdateTotalPrice(object sender, decimal oldPrice)
        {
            if (sender is Product product)
            {
                _totalPrice -= oldPrice;
                _totalPrice += product.Price;
            }
        }

        private class Node
        {
            public T Data { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
                Previous = null;
                Next = null;
            }
        }

        private Node head;
        private Node tail;
        private int count;

        public LinkedListContainer()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new LinkedListIterator(this);
        }

        public override void Clear()
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data is Product product)
                {
                    product.PriceChanged -= UpdateTotalPrice;
                    _totalPrice -= product.Price;
                }
                current = current.Next;
            }

            head = null;
            tail = null;
            count = 0;
            _totalPrice = 0m;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override IEnumerable<T> GetReverseEnumerator()
        {
            Node current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }

        public override IEnumerable<T> FilterByNameSubstring(string substring)
        {
            Node current = head;
            while (current != null)
            {
                if (current.Data.Name.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    yield return current.Data;
                }
                current = current.Next;
            }
        }

        public override IEnumerable<T> GetOrderedEnumerator(ProductSortField sortField)
        {
            List<T> items = new List<T>();
            Node current = head;
            while (current != null)
            {
                items.Add(current.Data);
                current = current.Next;
            }

            items.Sort((x, y) =>
            {
                return sortField switch
                {
                    ProductSortField.Price => (x as Product)?.Price.CompareTo((y as Product)?.Price) ?? 0,
                    ProductSortField.Name => string.Compare(x.Name, y.Name, StringComparison.Ordinal),
                    ProductSortField.PublicationType => GetPublicationTypeOrder(x).CompareTo(GetPublicationTypeOrder(y)),
                    _ => 0
                };
            });

            foreach (var item in items)
            {
                yield return item;
            }
        }

        // Клас-ітератор
        private class LinkedListIterator : IEnumerator<T>
        {
            private readonly LinkedListContainer<T> _list;
            private Node _currentNode;

            public LinkedListIterator(LinkedListContainer<T> list)
            {
                _list = list;
                _currentNode = null;
            }

            public T Current
            {
                get
                {
                    if (_currentNode == null)
                        throw new InvalidOperationException();
                    return _currentNode.Data;
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_currentNode == null)
                {
                    _currentNode = _list.head;
                }
                else
                {
                    _currentNode = _currentNode.Next;
                }

                return _currentNode != null;
            }

            public void Reset()
            {
                _currentNode = null;
            }

            public void Dispose()
            {
                // Нічого не потрібно звільняти
            }
        }

        public override T this[int index]
        {
            get
            {
                try
                {
                    return GetNodeAt(index).Data;
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new ProductNotFoundException($"Індекс {index} виходить за межі діапазону (1..{count})", null, "LinkedList index access");
                }
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Продукт не може бути null");

                Node node = GetNodeAt(index);
                T oldItem = node.Data;

                if (oldItem is Product oldProduct)
                {
                    oldProduct.PriceChanged -= UpdateTotalPrice;
                    _totalPrice -= oldProduct.Price;
                }

                if (value is Product newProduct)
                {
                    newProduct.PriceChanged += UpdateTotalPrice;
                    _totalPrice += newProduct.Price;
                }

                node.Data = value;
            }
        }

        public override T this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Name cannot be null or empty");

                Node current = head;
                while (current != null)
                {
                    if (current.Data.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return current.Data;
                    current = current.Next;
                }

                throw new KeyNotFoundException($"Product with name '{name}' not found");
            }
            set
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Name cannot be null or empty");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Product cannot be null");

                Node current = head;
                while (current != null)
                {
                    if (current.Data.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        T oldItem = current.Data;
                        if (oldItem is Product oldProduct)
                        {
                            oldProduct.PriceChanged -= UpdateTotalPrice;
                            _totalPrice -= oldProduct.Price;
                        }

                        if (value is Product newProduct)
                        {
                            newProduct.PriceChanged += UpdateTotalPrice;
                            _totalPrice += newProduct.Price;
                        }

                        current.Data = value;
                        return;
                    }
                    current = current.Next;
                }

                throw new KeyNotFoundException($"Product with name '{name}' not found");
            }
        }

        public override void Add(T item)
        {
            if (item is Product product)
            {
                product.PriceChanged += UpdateTotalPrice;
                _totalPrice += product.Price;
            }
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Node newNode = new Node(item);
            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Previous = tail;
                tail = newNode;
            }
            count++;
        }

        public override void AddToBeginning(T item)
        {

            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Node newNode = new Node(item);
            if (newNode.Data is Product product)
            {
                product.PriceChanged -= UpdateTotalPrice;
                _totalPrice -= product.Price;
            }

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            count++;
        }

        public override void InsertAt(int index, T item)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException("Індекс знаходиться поза діапазоном");
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (index == 0)
            {
                AddToBeginning(item);
                return;
            }

            if (index == count)
            {
                Add(item);
                return;
            }

            Node current = GetNodeAt(index);
            Node newNode = new Node(item);

            newNode.Previous = current.Previous;
            newNode.Next = current;
            current.Previous.Next = newNode;
            current.Previous = newNode;

            count++;
        }

        public override void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new ProductNotFoundException(
                    $"Індекс {index + 1} виходить за межі діапазону (1..{count})",
                    null,
                    "Remove at index");
            }

            Node current = GetNodeAt(index);

            if (current.Data is Product product)
            {
                product.PriceChanged -= UpdateTotalPrice;
                _totalPrice -= product.Price;
            }

            try
            {
                if (current.Previous != null)
                    current.Previous.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Previous = current.Previous;
                else
                    tail = current.Previous;

                count--;
            }
            catch (Exception ex)
            {
                throw new InvalidProductOperationException(
                    "Помилка при видаленні вузла зі списку",
                    current?.Data as Product,
                    "Remove at index");
            }
        }

        public override void OrderByPrice()
        {
            OrderBy(ProductSortField.Price);
        }

        public override void OrderBy(ProductSortField sortField)
        {
            if (count <= 1) return;

            List<T> itemsList = new List<T>();
            Node current = head;
            while (current != null)
            {
                itemsList.Add(current.Data);
                current = current.Next;
            }

            Comparison<T> comparison = sortField switch
            {
                ProductSortField.Price => (x, y) =>
                {
                    if (x is Product xProd && y is Product yProd)
                        return xProd.Price.CompareTo(yProd.Price);
                    return 0;
                }
                ,
                ProductSortField.Name => (x, y) =>
                    string.Compare(x.Name, y.Name, StringComparison.Ordinal),
                ProductSortField.PublicationType => (x, y) =>
                    GetPublicationTypeOrder(x).CompareTo(GetPublicationTypeOrder(y)),
                _ => (x, y) => 0
            };

            itemsList.Sort(comparison);

            current = head;
            foreach (var item in itemsList)
            {
                current.Data = item;
                current = current.Next;
            }
        }

        public override int Count => count;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Linked List Container with {count} items:");

            Node current = head;
            int index = 0;
            while (current != null)
            {
                sb.AppendLine($"[{index}] {current.Data}");
                current = current.Next;
                index++;
            }

            return sb.ToString();
        }

        private Node GetNodeAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range");

            Node current;
            if (index < count / 2)
            {
                current = head;
                for (int i = 0; i < index; i++)
                    current = current.Next;
            }
            else
            {
                current = tail;
                for (int i = count - 1; i > index; i--)
                    current = current.Previous;
            }

            return current;
        }

        public override void SaveToBinaryFile(string filePath)
        {
            using (var stream = File.Create(filePath))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(count);
                Node current = head;
                while (current != null)
                {
                    SerializeItem(writer, current.Data);
                    current = current.Next;
                }
            }
        }

        public override void LoadFromBinaryFile(string filePath)
        {
            using (var stream = File.OpenRead(filePath))
            using (var reader = new BinaryReader(stream))
            {
                int itemCount = reader.ReadInt32();
                head = null;
                tail = null;
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

        public override void Sort(ComparisonDelegate comparison)
        {
            if (count <= 1) return;
            List<T> itemsList = new List<T>();
            Node current = head;
            while (current != null)
            {
                itemsList.Add(current.Data);
                current = current.Next;
            }
            itemsList.Sort((x, y) => comparison(x, y));
            current = head;
            foreach (var item in itemsList)
            {
                current.Data = item;
                current = current.Next;
            }
        }

        public override T Find(PredicateDelegate predicate)
        {
            Node current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                    return current.Data;
                current = current.Next;
            }
            return default(T);
        }

        public override IEnumerable<T> FindAll(PredicateDelegate predicate)
        {
            Node current = head;
            while (current != null)
            {
                if (predicate(current.Data))
                    yield return current.Data;
                current = current.Next;
            }
        }

    }
}