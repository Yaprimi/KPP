using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лаб_4;

namespace Лаб_4
{
    public abstract class ProductContainerBase
    {
        public abstract void Add(Product item);
        public abstract void AddToBeginning(Product item);
        public abstract void InsertAt(int index, Product item);
        public abstract void RemoveAt(int index);
        public abstract void OrderByPrice();
        public abstract Product this[int index] { get; set; }
        public abstract Product[] this[decimal price] { get; set; }
        public abstract Product this[string name] { get; set; }
        public abstract int Count { get; }
        public abstract void OrderBy(ProductSortField sortField);

        protected int GetPublicationTypeOrder(Product product)
        {
            return product switch
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
