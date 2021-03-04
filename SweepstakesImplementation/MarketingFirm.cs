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

        // Dependency injection, takes in manager object rather than instantiating one
        public MarketingFirm(ISweepstakesManager manager)
        {
            _firmName = "Fictional Marketing Firm";
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
            SendWinningContestantEmail(winner, sweepstakes.Name);
        }
        private void SendWinningContestantEmail(Contestant winner, string sweepstakesName)
        {
            // Email address and password for it are both saved in a static class
            // that is added to gitignore.
            string emailFrom = SensitiveInfo.EmailSender;
            string password = SensitiveInfo.EmailPassword;
            string emailTo = winner.Email;

            // Always ask for password!
            MimeMessage message = new MimeMessage();
            message.Subject = $"Dear {winner.FirstName} {winner.LastName}, you are the winner of the {sweepstakesName} sweepstakes!";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = @"This is not sketchy at all. Please follow these instructions to claim your prize."
            };

            message.From.Add(new MailboxAddress(_firmName, emailFrom));
            message.To.Add(new MailboxAddress(winner.FirstName + " " + winner.LastName, emailTo));

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailFrom, password);
            client.Send(message);
            client.Disconnect(true);
        }

        public void RunQueueManagerHundredEntriesSimulation()
        {
            // 5 most popular baby boy and 5 most popular baby girl names of 2021
            // Probably going to stick with this for now, but thinking about it more this might make it more likely to get detected as spam
            string[] _firstNames = new string[] { "Liam", "Noah", "Oliver", "William", "Elijah", "Olivia", "Emma", "Ava", "Sophia", "Isabella" };
            // 10 most common surnames
            string[] _lastNames = new string[] { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez" };
            // Address, a lot of people just decline to fill it in, randomly do a few
            string[] _addresses = new string[] { "", "1234 Street", "First Street", "Lodon", "Randomly filling something out", "aaaaaaaaaaa" };
            
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
            PickWinnerAndNotifyAllContestants(sweepstakes);
        }

        public void CurrentTests()
        {
            CreateSweepstake("first");
            CreateSweepstake("second");
            Sweepstakes temp = _manager.GetSweepstakes();
            temp = _manager.GetSweepstakes();
        }
    }
}
