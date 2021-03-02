using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class SweepStakes
    {
        private string name;
        private Random rand;
        private Dictionary<int, Contestant> dictionary;
        public SweepStakes(string name)
        {
            this.name = name;
            rand = new Random(100);
        }

        void RegisterContestant(Contestant contestant)
        {
            dictionary[contestant.RegistrationNumber] = contestant;
        }


        Contestant PickWinner()
        {
            int dictionaryIndex = rand.Next(dictionary.Count);
            int count = 0;
            foreach (int contestantNum in dictionary.Keys)
            {
                if (count == dictionaryIndex)
                {
                    return dictionary[contestantNum];
                }
                else
                {
                    count += 1;
                }
            }
            return null;
        }

        void PrintCOntestantInfo(Contestant contestant)
        {
            Console.WriteLine($"Contestant number {contestant.RegistrationNumber}:");
            Console.WriteLine($"First name: {contestant.FirstName}, Last name: {contestant.LastName}");
            Console.WriteLine($"Address: {contestant.Address}");
            Console.WriteLine($"Email: {contestant.Email}");
        }
    }
}
