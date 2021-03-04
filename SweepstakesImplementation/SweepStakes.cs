using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Sweepstakes
    {
        private string _name;
        private static Random _rand = new Random();
        private Dictionary<int, Contestant> dictionary;

        // Observer related
        private Dictionary<int, int> _observerMapping;
        private int eventSystemToken;
        private ISweepstakesObserver observerSystem;

        // Dependency injection for observerSystem
        public Sweepstakes(string name, ISweepstakesObserver observerSystem)
        {
            _name = name;
            dictionary = new Dictionary<int, Contestant>();
            // Create mapping of RegistrationNumber to observer number retrieved from observerSystem
            _observerMapping = new Dictionary<int, int>();
            eventSystemToken = observerSystem.RegisterSweepstakes();
            this.observerSystem = observerSystem;
        }
        public string Name
        {
            get => _name; set => _name = value;
        }

        public void RegisterContestant(Contestant contestant)
        {
            dictionary[contestant.RegistrationNumber] = contestant;
            int newContestantObserverNumber = observerSystem.RegisterObserver(eventSystemToken, contestant);
            _observerMapping[contestant.RegistrationNumber] = newContestantObserverNumber;
            
        }

        public Contestant PickWinner()
        {
            int dictionaryIndex = _rand.Next(dictionary.Count);
            int count = 0;
            foreach (int contestantNum in dictionary.Keys)
            {
                if (count == dictionaryIndex)
                {
                    // Not completely sure about right way to do this.
                    // I want there to be distinction between Contestant and ICanBeNotified
                    int winnerObserverNumber = _observerMapping[contestantNum];
                    observerSystem.NotifyDrawingDone(eventSystemToken, winnerObserverNumber, _name);
                    return dictionary[contestantNum];
                }
                else
                {
                    count += 1;
                }
            }
            return null;
        }

        public void PrintCOntestantInfo(Contestant contestant)
        {
            Console.WriteLine($"Contestant number {contestant.RegistrationNumber}:");
            Console.WriteLine($"First name: {contestant.FirstName}, Last name: {contestant.LastName}");
            Console.WriteLine($"Address: {contestant.Address}");
            Console.WriteLine($"Email: {contestant.Email}");
        }
    }
}
