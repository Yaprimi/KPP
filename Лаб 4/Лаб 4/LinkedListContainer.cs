using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Add an item to the container

        public void AddToBeginning(Product item)
        {
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

        public void InsertAt(int index, Product item)
        {
            if (index < 0 || index > count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

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

        public void Add(Product item)
        {
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

        // Remove an item at the specified index
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            Node current = GetNodeAt(index);

            if (current.Previous != null)
            {
                current.Previous.Next = current.Next;
            }
            else
            {
                head = current.Next;
            }

            if (current.Next != null)
            {
                current.Next.Previous = current.Previous;
            }
            else
            {
                tail = current.Previous;
            }

            count--;
        }

        // Order the items by price (ascending) using bubble sort
        public void OrderByPrice()
        {
            if (count <= 1) return;

            bool swapped;
            do
            {
                swapped = false;
                Node current = head;

                while (current != null && current.Next != null)
                {
                    if (current.Data.Price > current.Next.Data.Price)
                    {
                        // Swap the data
                        Product temp = current.Data;
                        current.Data = current.Next.Data;
                        current.Next.Data = temp;
                        swapped = true;
                    }
                    current = current.Next;
                }
            } while (swapped);
        }

        // Convert container contents to string
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Container with {count} items:");

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

        // Helper method to get node at specific index
        private Node GetNodeAt(int index)
        {
            if (index < 0 || index >= count)
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }

            Node current;
            if (index < count / 2)
            {
                // Start from head
                current = head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
            }
            else
            {
                // Start from tail
                current = tail;
                for (int i = count - 1; i > index; i--)
                {
                    current = current.Previous;
                }
            }

            return current;
        }

        // Indexer to access items by index
        public Product this[int index]
        {
            get => GetNodeAt(index).Data;
            set => GetNodeAt(index).Data = value;
        }

        // Property to get the count of items
        public int Count => count;
    }
}
