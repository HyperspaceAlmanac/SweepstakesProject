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
            FillOutFirstAndLastName();
            FillOutEmail();
            FillOutAddress();
            FillOutRegistrationNumber();
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
        private void FillOutFirstAndLastName()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            while (!done)
            {
                Console.Clear();
                if (prevError)
                {
                    Console.WriteLine("Previous input was invalid, please try entering your first name again");
                }
                else
                {
                    Console.WriteLine("Please enter your first name:");
                }
                userInput = Console.ReadLine();
                if (userInput.Length > 0)
                {
                    firstName = userInput;
                    done = true;
                }
                else
                {
                    prevError = true;
                }
            }
            done = false;
            prevError = false;
            while (!done)
            {
                Console.Clear();
                if (prevError)
                {
                    Console.WriteLine("Previous input was invalid, please try entering your last name again");
                }
                else
                {
                    Console.WriteLine("Please enter your last name:");
                }
                userInput = Console.ReadLine();
                if (userInput.Length > 0)
                {
                    lastName = userInput;
                    done = true;
                }
                else
                {
                    prevError = true;
                }
            }

        }
        private void FillOutEmail()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            while (!done)
            {
                Console.Clear();
                if (prevError)
                {
                    Console.WriteLine("The email you entered is not valid, please try again!");
                }
                else
                {
                    Console.WriteLine("Please enter your email address:");
                }
                userInput = Console.ReadLine();
                if (EmailChecker.ValidEmail(userInput))
                {
                    email = userInput;
                    done = true;
                }
                else
                {
                    prevError = true;
                }
            }
        }
        private void FillOutAddress()
        {
            Console.WriteLine("Please fill out your address:");
            address = Console.ReadLine();
        }
        private void FillOutRegistrationNumber()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            int value = -1;
            while (!done)
            {
                if (prevError)
                {
                    Console.WriteLine("Previous input was invalid. Please try entering your Registration number again");
                }
                else
                {
                    Console.WriteLine("Please enter your registration number:");
                }
                userInput = Console.ReadLine();
                if (Int32.TryParse(userInput, out value))
                {
                    if (value > -1)
                    {
                        registrationNumber = value;
                    }
                    else
                    {
                        prevError = true;
                    }
                }
                else
                {
                    prevError = true;
                }
            }
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Address { get; }
        public int RegistrationNumber { get; }
    }
}
