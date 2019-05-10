using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ai_ass2
{
    public class TTEngine : Engine
    {
        public TTEngine(string file) : base(file)
        {

        }

        public bool TTEntails()
        {
            return false;
        }

        public bool TTCheckAll()
        {

            return false;

        }

        public override string KbEntails()
        {
            throw new NotImplementedException();
        }
    }
}
