using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лаб_4
{
    public class LinkedListContainer<T> : ProductContainerBase<T> where T : class, IName<T>

    {
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

                try
                {
                    GetNodeAt(index).Data = value;
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw new ProductNotFoundException($"Індекс {index + 1} виходить за межі діапазону (1..{count})", value as Product, "LinkedList index assignment");
                }
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
    }
}