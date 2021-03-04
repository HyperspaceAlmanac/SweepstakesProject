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
    // System for handling event notification for multiple sweepstakes going on
    class SweepstakesObserverSystem : ISweepstakesObserver
    {
        private Dictionary<int, List<ICanBeNotified>> _sweepstakes;
        private int _sweepstakesToken;
        private string _firmName;

        public SweepstakesObserverSystem()
        {
            _sweepstakes = new Dictionary<int, List<ICanBeNotified>>();
            _sweepstakesToken = 0;
            _firmName = "InsertFirmNameHere";
        }
        public void RegisterFirmName(string name)
        {
            _firmName = name;
        }

        // A sweepstakes calls this to get assigned a token to index into dictionary 
        public int RegisterSweepstakes() {
            int tokenNum = _sweepstakesToken;
            _sweepstakesToken += 1;
            _sweepstakes[tokenNum] = new List<ICanBeNotified>();
            return tokenNum;
        }

        // Sweepstake provides token that it used when registered, and use it to
        // Let the system know to register a new observer into that List
        public int RegisterObserver(int tokenNum, ICanBeNotified observer) {
            if (!_sweepstakes.ContainsKey(tokenNum))
            {
                return -1;
            }
            else
            {
                int index = _sweepstakes[tokenNum].Count;
                _sweepstakes[tokenNum].Add(observer);
                return index;
            }
        }

        // Notify all observers that a winner has been declarted
        public void NotifyAllAboutWinner(int tokenNum, int observerToken, string sweepstakesName) {
            if (_sweepstakes.ContainsKey(tokenNum)) {
                List<ICanBeNotified> observers = _sweepstakes[tokenNum];
                ICanBeNotified winner = observers[observerToken];
                for (int i = 0; i < observers.Count; i++)
                {
                    if (i == observerToken)
                    {
                        observers[i].Notify($"Congradulations! You are the winner of the {sweepstakesName} sweepstakes!");
                        SendWinningContestantEmail(sweepstakesName, winner.FullName(), winner.ContactEmail());
                    }
                    else
                    {
                        observers[i].Notify($"The {sweepstakesName} sweepstakes is now over. {winner.FullName()} is the winner.");
                    }
                }
            }
        }

        private void SendWinningContestantEmail(string sweepstakesName, string name, string emailTo)
        {
            // Email address and password for it are both saved in a static class
            // that is added to gitignore.
            string emailFrom = SensitiveInfo.EmailSender;
            string password = SensitiveInfo.EmailPassword;

            // Always ask for password!
            MimeMessage message = new MimeMessage();
            message.Subject = $"Dear {name}, you are the winner of {_firmName}'s {sweepstakesName} sweepstakes!";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = @"This is not sketchy at all. Please follow these instructions to claim your prize."
            };

            message.From.Add(new MailboxAddress("NOREPLY Sweepstakes Notification System", emailFrom));
            message.To.Add(new MailboxAddress(name, emailTo));

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailFrom, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
