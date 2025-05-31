using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace IDZ
{
    public class Container<T> : ProductContainerBase<T>, IEnumerable<T> where T : IName<T>
    {
        private T[] items;
        private int count;

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

        public Container()
        {
            items = new T[4];
            count = 0;
        }


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

        public override IEnumerable<T> FilterByNamePrefix(string prefix)
        {
            if (string.IsNullOrEmpty(prefix)) yield break;

            for (int i = 0; i < count; i++)
            {
                if (items[i].Name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    yield return items[i];
                }
            }
        }

        public override void Clear()
        {
            for (int i = 0; i < count; i++)
            {
                if (items[i] is Product product)
                {
                    product.PriceChanged -= UpdateTotalPrice;
                    _totalPrice -= product.Price;
                }

                items[i] = default(T);
            }
            count = 0;
            Resize(4);

        }

        public override IEnumerable<T> GetOrderedEnumerator(ProductSortField sortField)
        {
            T[] sortedArray = new T[count];
            for (int i = 0; i < count; i++)
            {
                sortedArray[i] = items[i];
            }

            Comparison<T> comparison = (x, y) =>
            {
                return sortField switch
                {
                    ProductSortField.Price => (x as Product)?.Price.CompareTo((y as Product)?.Price) ?? 0,
                    ProductSortField.Name => string.Compare(x.Name, y.Name, StringComparison.Ordinal),
                    ProductSortField.PublicationType => GetPublicationTypeOrder(x).CompareTo(GetPublicationTypeOrder(y)),
                    _ => 0
                };
            };

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (comparison(sortedArray[j], sortedArray[j + 1]) > 0)
                    {
                        T temp = sortedArray[j];
                        sortedArray[j] = sortedArray[j + 1];
                        sortedArray[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                yield return sortedArray[i];
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

            }
        }

        public override void AddToBeginning(T item)
        {
            if (item is Product product)
            {
                product.PriceChanged += UpdateTotalPrice;
                _totalPrice += product.Price;
            }

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

            if (item is Product product)
            {
                product.PriceChanged += UpdateTotalPrice;
                _totalPrice += product.Price;
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
            if (item is Product product)
            {
                product.PriceChanged += UpdateTotalPrice;
                _totalPrice += product.Price;
            }
            if (count == items.Length)
            {
                Resize(items.Length * 2);
            }
            items[count] = item;
            count++;
        }

        public override void RemoveAt(int index)
        {
            if (items[index] is Product product)
            {
                product.PriceChanged -= UpdateTotalPrice;
                _totalPrice -= product.Price;
            }
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
            for (int i = 0; i < count; i++)
            {
                itemsToSort[i] = items[i];
            }

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

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (comparison(itemsToSort[j], itemsToSort[j + 1]) > 0)
                    {
                        T temp = itemsToSort[j];
                        itemsToSort[j] = itemsToSort[j + 1];
                        itemsToSort[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                items[i] = itemsToSort[i];
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Контейнер (масив): {count} товарів");
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"[{i}] {items[i].ToString()}");
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

                T oldItem = items[index];
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

                items[index] = value;
            }
        }

        public override T this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    throw new InvalidProductOperationException("Назва не може бути порожньою", null, "Product search by name");

                T foundItem = default(T);
                for (int i = 0; i < count; i++)
                {
                    if (items[i] != null && items[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    {
                        foundItem = items[i];
                        break;
                    }
                }

                if (foundItem == null)
                    throw new ProductNotFoundException($"Продукт з назвою '{name}' не знайдено", null, "Product search by name");
                return foundItem;
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
                        T oldItem = items[i];
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

                        items[i] = value;
                        found = true;
                        break;
                    }
                }

                if (!found)
                    throw new ProductNotFoundException($"Продукт з назвою '{name}' не знайдено", value as Product, "Product update by name");
            }
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

            T[] itemsToSort = new T[count];
            for (int i = 0; i < count; i++)
            {
                itemsToSort[i] = items[i];
            }

            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (comparison(itemsToSort[j], itemsToSort[j + 1]) > 0)
                    {
                        T temp = itemsToSort[j];
                        itemsToSort[j] = itemsToSort[j + 1];
                        itemsToSort[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                items[i] = itemsToSort[i];
            }
        }

        public override T Find(PredicateDelegate predicate)
        {
            for (int i = 0; i < count; i++)
            {
                if (predicate(items[i]))
                    return items[i];
            }
            return default(T);
        }

        public override IEnumerable<T> FindAll(PredicateDelegate predicate)
        {
            for (int i = 0; i < count; i++)
            {
                if (predicate(items[i]))
                    yield return items[i];
            }
        }


        public override int Count => count;
    }
}
