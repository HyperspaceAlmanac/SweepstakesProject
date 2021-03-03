using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class SweepstakesStackManager
    {
        private Stack<SweepStakes> stack;

        public void InsertSweepstakes(SweepStakes sweepstakes)
        {
            stack.Push(sweepstakes);
        }

        public SweepStakes GetSweepstakes()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }
            else
            {
                return null;
            }
        }
    }
}
