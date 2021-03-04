using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    static class SweepstakesManagerFactory
    {
        public static ISweepstakesManager GenerateSweepstakesManager(SweepstakesManagerType sweepstakesType)
        {
            if (sweepstakesType == SweepstakesManagerType.QueueManager)
            {
                return new SweepstakesQueueManager();
            }
            else
            {
                return new SweepstakesStackManager();
            }
        }
    }
}
