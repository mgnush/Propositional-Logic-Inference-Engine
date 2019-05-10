using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class Sentence
    {
        private List<Symbol> _lhs;
        private Symbol _rhs;
        private List<Symbol> _allSymbols;

        public Sentence(string s)
        {
            string[] stringSeparators = new string[] { "=>" };
            string[] vs = s.Split(stringSeparators, StringSplitOptions.None);

            // LHS - need to remove white spaces
            string[] lhs = vs[0].Split('&');
            foreach(string symb in lhs)
            {
                string symbol = symb.Trim();
                _lhs.Add(new Symbol(symbol));
            }

            string rhs = vs[1].Trim();
            _rhs = new Symbol(rhs);
        }

        public List<Symbol> AllSymbols { get => _allSymbols; set => _allSymbols = value; }

        public bool IsSatisfied(List<Symbol> symbs)
        {
            foreach(Symbol s in symbs)
            {
                foreach(Symbol ls in _lhs)
                {
                    if (s.Name.Equals(ls.Name))
                    {
                        ls.Value = s.Value;
                    }
                }
                if (s.Name.Equals(_rhs.Name))
                {
                    _rhs.Value = s.Value;
                }
            }

            bool lhs = _lhs.First().Value;
            foreach (Symbol ls in _lhs)
            {
                lhs &= ls.Value;
            }

            return (lhs == _rhs.Value);
        }
    }
}
