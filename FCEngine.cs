using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public struct SymbolTable
    {
        public Symbol symbol;
        public bool value;

        public SymbolTable(Symbol s, bool v)
        {
            symbol = s;
            value = v;
        }       
    }

    public struct CountTable
    {
        public Sentence sentence;
        public int premises;

        public CountTable(Sentence s)
        {
            sentence = s;
            premises = s.Premises.Count;
        }
    }

    public class FCEngine : Engine
    {
        public FCEngine(string file) : base(file)
        {

        }
        
        private bool FCEntails(List<Symbol> entailed)
        {
            List<Symbol> agenda = new List<Symbol>();
            List<SymbolTable> inferred = new List<SymbolTable>();
            List<CountTable> count = new List<CountTable>();
            

            // Add all symbols to the inferred list
            foreach (Symbol symbol in _kb.Symbols)
            {                
                inferred.Add(new SymbolTable(symbol, false));
            }

            foreach (Sentence sentence in _kb.Sentences)
            {                
                if (sentence.Premises.Count == 0)
                {
                    agenda.AddRange(sentence.AllSymbols);   // Add symbols known to be true to agenda
                    entailed.AddRange(sentence.AllSymbols);
                } else
                {
                    count.Add(new CountTable(sentence));   // Add clauses with LHS to table of clauses
                }
            }

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

            if (FCEntails(entailed))
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
            } else
            {
                output = "NO";
            }


            return output;
        }
    }
}
