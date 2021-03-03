using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class MarketingFirm
    {
        private ISweepstakesManager _manager;

        public MarketingFirm(ISweepstakesManager manager)
        {
            _manager = manager;
        }

        public void CreateSweepstake()
        {
            string sweepstakesName = UserInterface.GetUserInputFor("Please enter a name for the sweepstakes");
            SweepStakes sweepstakes = new SweepStakes(sweepstakesName);
            _manager.InsertSweepstakes(sweepstakes);
        }
    }
}
