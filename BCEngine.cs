using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class BCEngine : Engine
    {
        public BCEngine(string file) : base(file)
        {

        }

        private bool BCEntails(List<Symbol> entailed)
        {
            List<Symbol> agenda = new List<Symbol>();
            List<SymbolTable> inferred = new List<SymbolTable>();
            List<CountTable> count = new List<CountTable>();


            // Add all symbols to the inferred list
            foreach (Symbol symbol in _kb.Symbols)
            {
                inferred.Add(new SymbolTable(symbol, false));
            }

            agenda.Add(_query);

            while (agenda.Count > 0)
            {
                Symbol p = agenda.Last();
                agenda.Remove(p);
                SymbolTable st = inferred.Find(x => x.symbol.Name.Equals(p.Name));
                if (!st.value)
                {
                    st.value = true;
                    foreach (Sentence sentence in _kb.Sentences)
                    {
                        if (sentence.ContainsPremise(p))
                        {
                            CountTable ct = count.Find(x => x.sentence == sentence);
                            ct.premises--;
                            if (ct.premises == 0)
                            {
                                entailed.Add(sentence.Head);
                                if (sentence.Head.Name.Equals(_query.Name)) { return true; }
                                agenda.Add(sentence.Head);
                            }
                        }
                    }
                }
            }

            return false;
        }


        public override string KbEntails()
        {
            List<Symbol> entailed = new List<Symbol>();
            string output = "";

            if (BCEntails(entailed))
            {
                output = "YES: ";
                foreach (Symbol s in entailed)
                {
                    output += s.Name;
                    if (s != entailed.Last())
                    {
                        output += ", ";
                    }
                }
            }
            else
            {
                output = "NO";
            }


            return output;
        }
    }
}
