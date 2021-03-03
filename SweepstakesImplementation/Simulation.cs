using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Simulation
    {
        public void CreateMarketingFirmWithManager()
        {
            SweepstakesManagerType managerType = UserInterface.AskForSweepstakesManagerType();

            ISweepstakesManager manager = SweepstakesManagerFactory(managerType);
            MarketingFirm firm = new MarketingFirm(manager);
        }

        public ISweepstakesManager SweepstakesManagerFactory(SweepstakesManagerType sweepstakesType)
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
