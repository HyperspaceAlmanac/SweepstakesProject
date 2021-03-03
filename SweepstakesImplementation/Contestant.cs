using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    class Contestant
    {
        private string firstName;
        private string lastName;
        private string email;
        private string address;
        private int registrationNumber;

        public void FillOutInformation()
        {
            string[] temp = UserInterface.GetFirstAndLastName();
            firstName = temp[0];
            lastName = temp[1];
            email = UserInterface.GetEmail();
            address = UserInterface.GetAddress();
            registrationNumber = UserInterface.GetRegistrationNumber();
        }
        // For testing and instantiating Contestant class without asking for user input
        public void FillOutInformation(string firstName, string lastName, string email, string address, int registrationNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.address = address;
            this.registrationNumber = registrationNumber;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Address { get; }
        public int RegistrationNumber { get; }
    }
}
