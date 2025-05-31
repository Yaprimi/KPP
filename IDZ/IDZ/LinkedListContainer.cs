using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace IDZ
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

        public override IEnumerable<T> FilterByNamePrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
                yield break;

            Node current = head;
            while (current != null)
            {
                if (current.Data.Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    yield return current.Data;
                }
                current = current.Next;
            }
        }

        public override IEnumerable<T> GetOrderedEnumerator(ProductSortField sortField)
        {
            T[] items = new T[count];
            Node current = head;
            int index = 0;
            while (current != null)
            {
                items[index++] = current.Data;
                current = current.Next;
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    int comparisonResult = 0;
                    switch (sortField)
                    {
                        case ProductSortField.Price:
                            if (items[j] is Product leftProd && items[j + 1] is Product rightProd)
                            {
                                comparisonResult = leftProd.Price.CompareTo(rightProd.Price);
                            }
                            break;
                        case ProductSortField.Name:
                            comparisonResult = string.Compare(items[j].Name, items[j + 1].Name, StringComparison.Ordinal);
                            break;
                        case ProductSortField.PublicationType:
                            comparisonResult = GetPublicationTypeOrder(items[j]).CompareTo(GetPublicationTypeOrder(items[j + 1]));
                            break;
                    }

                    if (comparisonResult > 0)
                    {
                        T temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                yield return items[i];
            }
        }

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

            T[] itemsArray = new T[count];
            Node current = head;
            int index = 0;
            while (current != null)
            {
                itemsArray[index++] = current.Data;
                current = current.Next;
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    int comparisonResult = 0;
                    switch (sortField)
                    {
                        case ProductSortField.Price:
                            if (itemsArray[j] is Product leftProd && itemsArray[j + 1] is Product rightProd)
                            {
                                comparisonResult = leftProd.Price.CompareTo(rightProd.Price);
                            }
                            break;
                        case ProductSortField.Name:
                            comparisonResult = string.Compare(itemsArray[j].Name, itemsArray[j + 1].Name, StringComparison.Ordinal);
                            break;
                        case ProductSortField.PublicationType:
                            comparisonResult = GetPublicationTypeOrder(itemsArray[j]).CompareTo(GetPublicationTypeOrder(itemsArray[j + 1]));
                            break;
                    }

                    if (comparisonResult > 0)
                    {
                        T temp = itemsArray[j];
                        itemsArray[j] = itemsArray[j + 1];
                        itemsArray[j + 1] = temp;
                    }
                }
            }

            current = head;
            for (int i = 0; i < count; i++)
            {
                current.Data = itemsArray[i];
                current = current.Next;
            }
        }

        public override int Count => count;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Контейнер (список): {count} товарів");

            Node current = head;
            int index = 0;
            while (current != null)
            {
                sb.AppendLine($"[{index}] {current.Data.ToString()}");
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

        public override void SaveToTextFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(this.ToString());
            }
        }

        public override void Sort(ComparisonDelegate comparison)
        {
            if (count <= 1) return;

            T[] itemsArray = new T[count];
            Node current = head;
            int index = 0;
            while (current != null)
            {
                itemsArray[index++] = current.Data;
                current = current.Next;
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (comparison(itemsArray[j], itemsArray[j + 1]) > 0)
                    {
                        T temp = itemsArray[j];
                        itemsArray[j] = itemsArray[j + 1];
                        itemsArray[j + 1] = temp;
                    }
                }
            }

            current = head;
            for (int i = 0; i < count; i++)
            {
                current.Data = itemsArray[i];
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