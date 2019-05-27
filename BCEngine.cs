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
            List<Symbol> goalStack = new List<Symbol>(); // Equivalent to FC agenda
            List<Symbol> goalFailed = new List<Symbol>(); // Known failed goals

            goalStack.Add(_query);  // Start with the query

            while (goalStack.Count > 0)
            {
                Symbol p = goalStack.Last();

                List<Sentence> sentences = new List<Sentence>();
                sentences.AddRange(_kb.Sentences.FindAll(x => x.Head.Name.Equals(p.Name))); // Retrieve all sentences with p as head
                int ruleFalse = sentences.Count; // Goal must be false in all rules to conclude false      
                int newGoals = 0; // The number of new goals p introduces

                foreach (Sentence rule in sentences)
                {
                    bool ruleEntailed = true;                    

                    foreach (Symbol premise in rule.Premises)
                    {
                        // Cannot entail head unless all premises are entailed
                        if (!entailed.Exists(x => x.Name.Equals(premise.Name)))
                        {
                            ruleEntailed = false;
                        }
                        // All rules with head p must be false to fail p
                        if (goalFailed.Exists(x => x.Name.Equals(premise.Name)))
                        {
                            ruleFalse--;
                            break; // Only one premise need be false for rule to fail
                        }                            
                    }

                    if (ruleEntailed)
                    {
                        entailed.Add(p);
                        goalStack.Remove(p);
                        break; // Only one rule need be true to entail p                   
                    } else
                    {                        
                        foreach (Symbol premise in rule.Premises)
                        {
                            // Add subgoal to goalstack if it is not already on it, is not entailed, and has not failed yet
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

                // If all rules with head p failed, remove p from goalstack and add to failed goals
                if ((ruleFalse == 0))
                {
                    goalFailed.Add(p);
                    goalStack.Remove(p);
                }
                // Remove p from goalstack if introduces no new subgoals
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
