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
            
        }
        private void FillOutFirstName()
        {
            Console.WriteLine("Please enter your first name:");

        }
        private void FillOutLastName()
        {
            Console.WriteLine("Please enter your last name:");
        }
        private void FillOutEmail()
        {
            Console.WriteLine("Please fill out your email address:");
        }
        private void FillOutAddress()
        {
            Console.WriteLine("Please fill out your address:");
        }
        private void FillOutRegistrationNumber()
        {
            Console.WriteLine("Please enter your registration number:");
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Address { get; }
        public int RegistrationNumber { get; }
    }
}
