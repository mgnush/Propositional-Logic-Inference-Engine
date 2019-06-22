using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class CountTable
    {
        private Sentence _sentence;
        private int _premises;

        public CountTable(Sentence s)
        {
            _sentence = s;
            _premises = s.Premises.Count;
        }

        public int Premises { get => _premises; set => _premises = value; }
        public Sentence Sentence { get => _sentence; }
    }

    public class FCEngine : Engine
    {
        public FCEngine(string file) : base(file)
        {

        }
        
        private bool FCEntails(List<Symbol> entailed)
        {
            List<Symbol> agenda = new List<Symbol>();
            List<Symbol> inferred = new List<Symbol>(); // List of all symbols that have been inferred
            List<CountTable> count = new List<CountTable>(); // Keep tabs on premises that have been fulfilled for each head
           
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

                if (!inferred.Exists(x => x.Name.Equals(p.Name))) // Don't infer any symbols more than once
                {
                    inferred.Add(p);
                    // Find all sentences with premise p
                    foreach (Sentence sentence in _kb.Sentences)
                    {
                        if (sentence.ContainsPremise(p))
                        {
                            // Indicate that a sentence premise has been fullfilled
                            CountTable ct = count.Find(x => x.Sentence == sentence);
                            ct.Premises--;
                            // Sentence head is entailed if all premises are fullfilled
                            if (ct.Premises == 0)
                            {
                                entailed.Add(sentence.Head);
                                if (sentence.Head.Name.Equals(_query.Name)) { return true; } // Exit search when query is entailed
                                agenda.Add(sentence.Head); // Add entailed head to agenda  
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
