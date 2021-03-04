using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit;
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
            Sweepstakes sweepstakes = new Sweepstakes(sweepstakesName);
            _manager.InsertSweepstakes(sweepstakes);
        }

        // Just for testing

        public void CreateSweepstake(string sweepstakesName)
        {
            Sweepstakes sweepstakes = new Sweepstakes(sweepstakesName);
            _manager.InsertSweepstakes(sweepstakes);
        }

        public void PickWinnerAndNotifyAllContestants(Sweepstakes sweepstakes)
        {
            Contestant winner = sweepstakes.PickWinner();
            sweepstakes.NotifyAllContestants(winner.RegistrationNumber);
            SendWinningContestantEmail(winner);
        }
        private void SendWinningContestantEmail(Contestant winner)
        {
            // Going to read email address from file added to gitignore.
            string emailFrom = System.IO.File.ReadAllText("..\\..\\..\\EmailReceiver.txt");
            string emailTo = winner.Email;
            string confirmation = UserInterface.GetUserInputFor($"Please enter \"yes\" to confirm that you would like to send email from {emailFrom} to {emailTo}");
            if (confirmation == "yes")
            {
                // Always ask for password!
                string password = UserInterface.GetUserInputFor($"Please enter the password for the email address");
            }
        }



        public void RunQueueManagerHundredEntriesSimulation()
        {
            // 10 most popular baby boy names of 2021
            string[] _firstNames = new string[] { "Liam", "Noah", "Oliver", "William", "Elijah", "James", "Benjamin", "Lucas", "Mason", "Ethan" };
            // 10 most common surnames
            string[] _lastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
            // Address, a lot of people just decline to fill it in, randomly do a few
            string[] _addresses = new string[] { "", "1234 Street", "First Street", "Lodon", "Randomly filling something out", "aaaaaaaaaaa" };
            // Since this is public repo, leavine these blank. Only doing specific ones for testing
            // Also do not want to accidentally send email to real addresses
            string commonEmail = System.IO.File.ReadAllText("..\\..\\..\\EmailReceiver.txt");
            Random _rand = new Random();

            string tempFirst, tempLast, tempAddr;
            CreateSweepstake("First contest");
            Sweepstakes sweepstakes = _manager.GetSweepstakes();
            for (int i = 0; i < 100; i++)
            {
                tempFirst = _firstNames[_rand.Next(_firstNames.Length)];
                tempLast = _lastNames[_rand.Next(_lastNames.Length)];
                tempAddr = _addresses[_rand.Next(_addresses.Length)];
                Contestant entry = new Contestant();
                entry.FillOutInformation(tempFirst, tempLast, commonEmail, tempAddr, i);
                sweepstakes.RegisterContestant(entry);
            }
            PickWinnerAndNotifyAllContestants(sweepstakes);
        }
    }
}
