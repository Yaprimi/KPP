using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Лаб_4;

namespace Лаб_4
{
    public class StringWrapper : IName<StringWrapper>
    {
        public string Value { get; set; }
        public string Name { get => Value; set => Value = value; }

        public StringWrapper(string value) => Value = value;
        public int CompareTo(object obj) => Value.CompareTo((obj as StringWrapper)?.Value);
    }

}
