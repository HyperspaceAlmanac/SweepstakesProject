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

            // Three contests, using Queue Manager. Should be 0, 1, 2 for tokens
            //MultipleContestsQueueManagerTest();

            // Three contests, using stack Manager. Should be 2, 1, 0 for tokens
            //MultipleContestsStackManagerTest();
        }
        public void CreateMarketingFirmWithManager()
        {
            SweepstakesManagerType managerType = UserInterface.AskForSweepstakesManagerType();
            ISweepstakesObserver observerSystem = new SweepstakesObserverSystem();

            ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(managerType);
            MarketingFirm firm = new MarketingFirm(manager, observerSystem);
        }

        // Just change this to test anything
        private void CurrentTests()
        {
            //ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(SweepstakesManagerType.QueueManager);
            //ISweepstakesManager manager = SweepstakesManagerFactory(SweepstakesManagerType.StackManager);
            SweepstakesManagerType managerType = UserInterface.AskForSweepstakesManagerType();
            ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(managerType);
            ISweepstakesObserver observerSystem = new SweepstakesObserverSystem();
            MarketingFirm firm = new MarketingFirm(manager, observerSystem);
            firm.CurrentTests();
        }

        private void HundredEntryQueueManagerTest()
        {
            ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(SweepstakesManagerType.QueueManager);
            ISweepstakesObserver observerSystem = new SweepstakesObserverSystem();
            MarketingFirm firm = new MarketingFirm(manager, observerSystem);
            firm.RunQueueManagerHundredEntriesSimulation();
        }

        private void MultipleContestsQueueManagerTest()
        {
            ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(SweepstakesManagerType.QueueManager);
            ISweepstakesObserver observerSystem = new SweepstakesObserverSystem();
            MarketingFirm firm = new MarketingFirm(manager, observerSystem);
            firm.RunMultipleContests();
        }
        private void MultipleContestsStackManagerTest()
        {
            ISweepstakesManager manager = SweepstakesManagerFactory.GenerateSweepstakesManager(SweepstakesManagerType.StackManager);
            ISweepstakesObserver observerSystem = new SweepstakesObserverSystem();
            MarketingFirm firm = new MarketingFirm(manager, observerSystem);
            firm.RunMultipleContests();
        }
    }
}
