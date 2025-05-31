using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDZ;

namespace IDZ
{
    public class IntWrapper : IName<IntWrapper>
    {
        public int Value { get; set; }
        public string Name { get; set; }

        public IntWrapper(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int CompareTo(object obj) => Value.CompareTo((obj as IntWrapper)?.Value);
    }

}
