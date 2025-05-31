using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDZ;

namespace IDZ
{
    public class StringWrapper : IName<StringWrapper>
    {
        public string Value { get; set; }
        public string Name { get => Value; set => Value = value; }

        public StringWrapper(string value) => Value = value;
        public int CompareTo(object obj) => Value.CompareTo((obj as StringWrapper)?.Value);
    }

}
