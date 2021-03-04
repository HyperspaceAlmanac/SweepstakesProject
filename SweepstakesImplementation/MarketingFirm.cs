using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit;
using MimeKit;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace SweepstakesImplementation
{
    class MarketingFirm
    {
        private ISweepstakesManager _manager;
        private string _firmName;
        private ISweepstakesObserver _observerSystem;

        // Dependency injection, takes in manager object rather than instantiating one
        public MarketingFirm(ISweepstakesManager manager, ISweepstakesObserver observerSystem)
        {
            _firmName = "Fictional Marketing Firm";
            _manager = manager;
            _observerSystem = observerSystem;
        }

        public void CreateSweepstake()
        {
            string sweepstakesName = UserInterface.GetUserInputFor("Please enter a name for the sweepstakes");
            Sweepstakes sweepstakes = new Sweepstakes(sweepstakesName, _observerSystem);
            _manager.InsertSweepstakes(sweepstakes);
        }

        // Just for testing

        public void CreateSweepstake(string sweepstakesName)
        {
            Sweepstakes sweepstakes = new Sweepstakes(sweepstakesName, _observerSystem);
            _manager.InsertSweepstakes(sweepstakes);
        }

        public void PickWinner(Sweepstakes sweepstakes)
        {
            Contestant winner = sweepstakes.PickWinner();
        }

        public void RunQueueManagerHundredEntriesSimulation()
        {
            // 5 most popular baby boy and 5 most popular baby girl names of 2021
            // Probably going to stick with this for now, but thinking about it more this might make it more likely to get detected as spam
            string[] _firstNames = new string[] { "Liam", "Noah", "Oliver", "William", "Elijah", "Olivia", "Emma", "Ava", "Sophia", "Isabella" };
            // 10 most common surnames
            string[] _lastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
            // Address, a lot of people just decline to fill it in, randomly do a few
            string[] _addresses = new string[] { "", "1234 Street", "First Street", "London", "Randomly filling something out", "aaaaaaaaaaa" };
            
            // Do not want to accidentally sending things out to real emails
            // Going to have a static string[] with list of my own email addresses
            // Going to hide how many there are. It is going to randomly return an address
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
                entry.FillOutInformation(tempFirst, tempLast, SensitiveInfo.GetRandomEmail(), tempAddr, i);
                sweepstakes.RegisterContestant(entry);
            }
            PickWinner(sweepstakes);
        }

        public void StackAndQueueTests()
        {
            CreateSweepstake("first");
            CreateSweepstake("second");
            Sweepstakes temp = _manager.GetSweepstakes();
            temp = _manager.GetSweepstakes();
        }

        public void CurrentTests()
        {
            CreateSweepstake("first");
            Sweepstakes temp = _manager.GetSweepstakes();

            Contestant contestant;
            for (int i = 0; i < 3; i++)
            {
                contestant = new Contestant();
                contestant.FillOutInformation();
                temp.RegisterContestant(contestant);
            }
            temp.PickWinner();
        }
    }
}
