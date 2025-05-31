using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDZ;

namespace IDZ
{
    public class ProductException : Exception
    {
        public Product Product { get; }
        public string Operation { get; }

        public ProductException(string message, Product product = null, string operation = null, Exception innerException = null)
            : base(message, innerException)
        {
            Product = product;
            Operation = operation;
        }

    }
}






public class ProductNotFoundException : ProductException
{
    public ProductNotFoundException(string message, Product product = null, string operation = null)
        : base(message, product, operation) { }
}

public class InvalidProductOperationException : ProductException
{
    public InvalidProductOperationException(string message, Product product = null, string operation = null)
        : base(message, product, operation) { }
}

public class ContainerFullException : ProductException
{
    public int MaxCapacity { get; }

    public ContainerFullException(int maxCapacity, string message, Product product = null, string operation = null)
        : base(message, product, operation)
    {
        MaxCapacity = maxCapacity;
    }
}
