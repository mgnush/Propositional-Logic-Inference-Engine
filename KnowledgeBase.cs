using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class KnowledgeBase
    {
        private List<Sentence> _sentences;
        private List<Symbol> _symbols;

        public KnowledgeBase(string[] sentences)
        {
            foreach(string s in sentences)
            {
                Sentence sentence = new Sentence(s);
                _sentences.Add(sentence);
                _symbols.AddRange(sentence.AllSymbols);
            }
        }
    }
}
