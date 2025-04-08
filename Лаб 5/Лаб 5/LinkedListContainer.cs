using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Лаб_4
{
    public class LinkedListContainer : IProductContainer
    {
        private class Node
        {
            public Product Data { get; set; }
            public Node Previous { get; set; }
            public Node Next { get; set; }

            public Node(Product data)
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

        // Індексатор за порядковим номером
        public Product this[int index]
        {
            get => GetNodeAt(index).Data;
            set
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException($"Index {index} is out of range (0..{count - 1})");
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Product cannot be null");

                GetNodeAt(index).Data = value;
            }
        }

        // Індексатор за назвою товару
        public Product this[string name]
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

        // Індексатор за ціною
        public Product[] this[decimal price]
        {
            get
            {
                if (price < 0)
                    throw new ArgumentException("Price cannot be negative");

                List<Product> matchingProducts = new List<Product>();
                Node current = head;
                while (current != null)
                {
                    if (current.Data.Price == price)
                        matchingProducts.Add(current.Data);
                    current = current.Next;
                }

                if (matchingProducts.Count == 0)
                    throw new KeyNotFoundException($"No products found with price {price}");

                return matchingProducts.ToArray();
            }
            set
            {
                if (price < 0)
                    throw new ArgumentException("Price cannot be negative");
                if (value == null || value.Length == 0)
                    throw new ArgumentNullException(nameof(value), "Products array cannot be null or empty");

                List<Product> productsToAssign = new List<Product>(value);
                Node current = head;
                while (current != null && productsToAssign.Count > 0)
                {
                    if (current.Data.Price == price)
                    {
                        current.Data = productsToAssign[0];
                        productsToAssign.RemoveAt(0);
                    }
                    current = current.Next;
                }

                if (productsToAssign.Count > 0)
                    throw new KeyNotFoundException($"Not enough products with price {price} to replace");
            }
        }

        // Додатковий метод для отримання всіх продуктів за назвою
        public Product[] GetAllByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null or empty");

            List<Product> matchingProducts = new List<Product>();
            Node current = head;
            while (current != null)
            {
                if (current.Data.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    matchingProducts.Add(current.Data);
                current = current.Next;
            }

            return matchingProducts.ToArray();
        }

        // Інші методи інтерфейсу IProductContainer
        public void Add(Product item)
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

        public void AddToBeginning(Product item)
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

        public void InsertAt(int index, Product item)
        {
            if (index < 0 || index > count)
                throw new IndexOutOfRangeException("Index is out of range");
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

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Index is out of range");

            Node current = GetNodeAt(index);

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

        public void OrderByPrice()
        {
            OrderBy(ProductSortField.Price);
        }

        public void OrderBy(ProductSortField sortField)
        {
            if (count <= 1) return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = head;

                while (current != null && current.Next != null)
                {
                    bool shouldSwap = false;

                    switch (sortField)
                    {
                        case ProductSortField.Price:
                            shouldSwap = current.Data.Price > current.Next.Data.Price;
                            break;
                        case ProductSortField.Name:
                            shouldSwap = string.Compare(current.Data.Name, current.Next.Data.Name, StringComparison.Ordinal) > 0;
                            break;
                        case ProductSortField.PublicationType:
                            shouldSwap = GetPublicationTypeOrder(current.Data) > GetPublicationTypeOrder(current.Next.Data);
                            break;
                    }

                    if (shouldSwap)
                    {
                        Product temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
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

        public int Count => count;

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