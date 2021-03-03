using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class SweepstakesQueueManager : ISweepstakesManager
    {
        private Queue<SweepStakes> queue;

        public void InsertSweepstakes(SweepStakes sweepstakes)
        {
            queue.Enqueue(sweepstakes);
        }

        public SweepStakes GetSweepstakes()
        {
            if (queue.Count > 0)
            {
                return queue.Dequeue();
            }
            else
            {
                return null;
            }
        }
    }
}
