using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Contestant : ICanBeNotified
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _address;
        private int _registrationNumber;

        public void FillOutInformation()
        {
            string[] temp = UserInterface.GetFirstAndLastName();
            _firstName = temp[0];
            _lastName = temp[1];
            _email = UserInterface.GetEmail();
            _address = UserInterface.GetAddress();
            _registrationNumber = UserInterface.GetRegistrationNumber();
        }
        // For testing and instantiating Contestant class without asking for user input
        public void FillOutInformation(string firstName, string lastName, string email, string address, int registrationNumber)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _address = address;
            _registrationNumber = registrationNumber;
        }
        public void Notify(string message)
        {
            Console.WriteLine($"Contestant with entry {_registrationNumber} has received the following sweepstakes results:");
            Console.WriteLine(message);
        }
        public string FirstName { get => _firstName ; set => _firstName = value; }
        public string LastName { get => _lastName ; set => _lastName = value; }
        public string Email { get => _email; set => _email = value; }
        public string Address { get => _address; set => _address = value; }
        public int RegistrationNumber { get => _registrationNumber; set => _registrationNumber = value; }
    }
}
