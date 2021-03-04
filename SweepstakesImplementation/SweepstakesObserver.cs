using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    interface SweepstakesObserver
    {
        int RegisterSweepstakes();
        int RegisterObserver(int tokenNum, ICanBeNotified observer);
        void NotifyDrawingDone(int tokenNum, int observerToken, string winningMessage, string other, string sweepstakesName, ICanBeNotified winner);
    }
}
