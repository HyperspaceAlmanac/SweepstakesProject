using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Simulation
    {
        public void RunSim()
        {
            // Default is for the one to let user choose SweepstakesManater
            CreateMarketingFirmWithManager();

            // Other tests goes here
            //CurrentTests();

            // Test to randomly generate 100 contestants, and pick winner, and send email to specified emails
            //HundredEntryQueueManagerTest();

        }
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


        // Just change this to test anything
        private void CurrentTests()
        {
            ISweepstakesManager manager = SweepstakesManagerFactory(SweepstakesManagerType.QueueManager);
            //ISweepstakesManager manager = SweepstakesManagerFactory(SweepstakesManagerType.StackManager);
            MarketingFirm firm = new MarketingFirm(manager);
            firm.CurrentTests();
        }

        private void HundredEntryQueueManagerTest()
        {
            ISweepstakesManager manager = SweepstakesManagerFactory(SweepstakesManagerType.QueueManager);
            MarketingFirm firm = new MarketingFirm(manager);
            firm.RunQueueManagerHundredEntriesSimulation();
        }
    }
}
