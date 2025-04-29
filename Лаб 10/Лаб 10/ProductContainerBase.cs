using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лаб_4;

namespace Лаб_4
{
    public abstract class ProductContainerBase<T> : IEnumerable<T> where T : IName<T>
    {
        public abstract void Add(T item);
        public abstract void AddToBeginning(T item);
        public abstract void InsertAt(int index, T item);
        public abstract void RemoveAt(int index);
        public abstract void OrderByPrice();
        public abstract T this[int index] { get; set; }
        public abstract T this[string name] { get; set; }
        public abstract int Count { get; }
        public abstract void OrderBy(ProductSortField sortField);

        public abstract IEnumerator<T> GetEnumerator();

        public abstract IEnumerable<T> GetReverseEnumerator();
        public abstract IEnumerable<T> FilterByNameSubstring(string substring);
        public abstract IEnumerable<T> GetOrderedEnumerator(ProductSortField sortField);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected int GetPublicationTypeOrder(T item)
        {
            return item switch
            {
                Book => 1,
                Magazine => 2,
                _ => 3
            };
        }
    }

    public enum ProductSortField
    {
        Price,
        Name,
        PublicationType
    }
}
