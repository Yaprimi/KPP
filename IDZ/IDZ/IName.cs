using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDZ
{
    public interface IName : IComparable
    {
        string Name { get; set; }
    }

    public interface IName<T> : IComparable
    {
        string Name { get; set; }
    }
}
