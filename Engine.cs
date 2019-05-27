using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public abstract class Engine
    {
        protected KnowledgeBase _kb;
        protected Symbol _query;

        public Engine(string file)
        {
            // Load tell into line 0, ask into line 1 of tellask array
            string[] stringSeparators = new string[] { "TELL", "ASK" , "\n"};
            string[] lines = file.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            string[] tellask = new string[2];
            int i = 0;
            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    tellask[i] = line;
                    i++;
                }
            }

            // TESTING ONLY
            /*
            foreach (string line in tellask)
            {
                Console.WriteLine("{0}", line);
            }
            */

            // Load tell into kb
            string[] sentences = tellask[0].Split(';');
            _kb = new KnowledgeBase(sentences);

            // Obtain ask
            _query = new Symbol(tellask[1].Trim());
        }

        public abstract string KbEntails();
    }
}
