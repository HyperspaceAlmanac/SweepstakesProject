using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    static class UserInterface
    {
        public static string GetUserInputFor(string prompt)
        {
            Console.Clear();
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
        public static SweepstakesManagerType AskForSweepstakesManagerType()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            int value = -1;
            SweepstakesManagerType result = SweepstakesManagerType.QueueManager;
            while (!done)
            {
                if (prevError)
                {
                    userInput = GetUserInputFor("Previous input was invalid. Please enter 1 for SweepstakesStackManager or 2 for SweepstakesQueueManager:");
                }
                else
                {
                    userInput = GetUserInputFor("Please enter 1 for SweepstakesStackManager or 2 for SweepstakesQueueManager:");
                }
                if (Int32.TryParse(userInput, out value))
                {
                    if (value == 1 || value == 2)
                    {
                        if (value == 2)
                        {
                            result = SweepstakesManagerType.StackManager;
                        }
                        done = true;
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
            return result;
        }

        public static string[] GetFirstAndLastName()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            string firstName = "";
            string lastName = "";
            while (!done)
            {
                if (prevError)
                {
                    userInput = GetUserInputFor("Previous input was invalid, please try entering your first name again");
                }
                else
                {
                    userInput = GetUserInputFor("Please enter your first name:");
                }
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
                if (prevError)
                {
                    userInput = GetUserInputFor("Previous input was invalid, please try entering your last name again");
                }
                else
                {
                    userInput = GetUserInputFor("Please enter your last name:");
                }

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
            return new string[] { firstName, lastName};

        }
        public static string GetEmail()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            string email = "";
            while (!done)
            {
                Console.Clear();
                if (prevError)
                {
                    userInput = GetUserInputFor("The email you entered is not valid, please try again!");
                }
                else
                {
                    userInput = GetUserInputFor("Please enter your email address:");
                }
 
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
            return email;
        }
        public static string GetAddress()
        {
            return GetUserInputFor("Please fill out your address:");
        }
        public static int GetRegistrationNumber()
        {
            bool done = false;
            bool prevError = false;
            string userInput;
            int value = -1;
            int registrationNumber = -1;
            while (!done)
            {
                if (prevError)
                {
                    userInput = GetUserInputFor("Previous input was invalid. Please try entering your Registration number again");
                }
                else
                {
                    userInput = GetUserInputFor("Please enter your registration number:");
                }
                if (Int32.TryParse(userInput, out value))
                {
                    if (value > -1)
                    {
                        registrationNumber = value;
                        done = true;
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
            return registrationNumber;
        }
    }
}
