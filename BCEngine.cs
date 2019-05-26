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
            List<Symbol> goalStack = new List<Symbol>();
            List<Symbol> goalFailed = new List<Symbol>();

            goalStack.Add(_query);

            while (goalStack.Count > 0)
            {
                Symbol p = goalStack.Last();

                List<Sentence> sentences = new List<Sentence>();
                sentences.AddRange(_kb.Sentences.FindAll(x => x.Head.Name.Equals(p.Name)));   // Retrieve all sentences with p as head
                int ruleFalse = sentences.Count; // Goal must be false in all rules to conclude false      
                int newGoals = 0;

                foreach (Sentence rule in sentences)
                {
                    bool ruleEntailed = true;                    

                    foreach (Symbol premise in rule.Premises)
                    {
                        // Cannot entail head unless all premises are entailed // removing too aggressivle, entailed...
                        if (!entailed.Exists(x => x.Name.Equals(premise.Name)))
                        {
                            ruleEntailed = false;
                        }
                        if (goalFailed.Exists(x => x.Name.Equals(premise.Name)))
                        {
                            ruleFalse--;
                            break;
                        }                            
                    }

                    if (ruleEntailed)
                    {
                        entailed.Add(p);
                        goalStack.Remove(p);
                        break;                               
                    } else
                    {                        
                        foreach (Symbol premise in rule.Premises)
                        {
                            if (!entailed.Exists(x => x.Name.Equals(premise.Name)) && !goalFailed.Exists(x => x.Name.Equals(premise.Name)))
                            {
                                if (!goalStack.Exists(x => x.Name.Equals(premise.Name)))
                                {
                                    goalStack.Add(premise);
                                    newGoals++;
                                }                                       
                            }
                        }                      
                    }
                }

                //check if p is false
                if (ruleFalse == 0)
                {
                    goalFailed.Add(p);
                    goalStack.Remove(p);
                }

                if (newGoals == 0)
                {
                    goalStack.Remove(p);
                }

            }
            
            return entailed.Contains(_query);
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
