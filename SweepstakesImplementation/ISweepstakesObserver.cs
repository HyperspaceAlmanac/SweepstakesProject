using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    interface ISweepstakesObserver
    {
        int RegisterSweepstakes();
        int RegisterObserver(int tokenNum, ICanBeNotified observer);
        void NotifyAllAboutWinner(int tokenNum, int observerToken, string sweepstakesName);
    }
}
