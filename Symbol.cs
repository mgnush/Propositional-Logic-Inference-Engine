using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class Symbol
    {
        string _name;
        bool _value;

        public Symbol(string name)
        {
            _name = name;
            _value = false;
        }

        public string Name { get => _name; set => _name = value; }
        public bool Value { get => _value; set => _value = value; }
    }
}
