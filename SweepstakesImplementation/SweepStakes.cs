﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Sweepstakes
    {
        private string _name;
        private Random _rand;
        private Dictionary<int, Contestant> dictionary;
        public Sweepstakes(string name)
        {
            _name = name;
            _rand = new Random(100);
            dictionary = new Dictionary<int, Contestant>();
        }
        public string Name
        {
            get => _name; set => _name = value;
        }

        public void RegisterContestant(Contestant contestant)
        {
            dictionary[contestant.RegistrationNumber] = contestant;
        }

        public void NotifyAllContestants(int winnerRegistrationNumber)
        {
            string firstName = dictionary[winnerRegistrationNumber].FirstName;
            string lastName = dictionary[winnerRegistrationNumber].LastName;
            string winnerMessage = "The sweepstakes is over. Congraduatlions! You are the winner!";
            string otherMessage = $"The sweepstakes is over. Contestant {winnerRegistrationNumber} {firstName} {lastName} is the winer!";
            foreach (int contestantNumber in dictionary.Keys)
            {
                if (contestantNumber == winnerRegistrationNumber)
                {
                    dictionary[contestantNumber].Notify(winnerMessage);
                }
                else
                {
                    dictionary[contestantNumber].Notify(otherMessage);
                }
            }
        }
        public Contestant PickWinner()
        {
            int dictionaryIndex = _rand.Next(dictionary.Count);
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

        public void PrintCOntestantInfo(Contestant contestant)
        {
            Console.WriteLine($"Contestant number {contestant.RegistrationNumber}:");
            Console.WriteLine($"First name: {contestant.FirstName}, Last name: {contestant.LastName}");
            Console.WriteLine($"Address: {contestant.Address}");
            Console.WriteLine($"Email: {contestant.Email}");
        }
    }
}
