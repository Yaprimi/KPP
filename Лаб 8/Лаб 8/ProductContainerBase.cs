using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лаб_4;

namespace Лаб_4
{
    public abstract class ProductContainerBase<T> where T : IName<T>
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
