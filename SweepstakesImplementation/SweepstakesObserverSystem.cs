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
        private Dictionary<int, List<ICanBeNotified>> sweepstakes;
        private int sweepstakesToken;

        public SweepstakesObserverSystem()
        {
            sweepstakes = new Dictionary<int, List<ICanBeNotified>>();
            sweepstakesToken = 0;
        }

        // A sweepstakes calls this to get assigned a token to index into dictionary 
        public int RegisterSweepstakes() {
            int tokenNum = sweepstakesToken;
            sweepstakesToken += 1;
            sweepstakes[tokenNum] = new List<ICanBeNotified>();
            return tokenNum;
        }

        // Sweepstake provides token that it used when registered, and use it to
        // Let the system know to register a new observer into that List
        public int RegisterObserver(int tokenNum, ICanBeNotified observer) {
            if (!sweepstakes.ContainsKey(tokenNum))
            {
                return -1;
            }
            else
            {
                int index = sweepstakes[tokenNum].Count;
                sweepstakes[tokenNum].Add(observer);
                return index;
            }
        }

        // Notify all observers that 
        public void NotifyDrawingDone(int tokenNum, int observerToken, string sweepstakesName) {
            if (sweepstakes.ContainsKey(tokenNum)) {
                List<ICanBeNotified> observers = sweepstakes[tokenNum];
                ICanBeNotified winner = observers[tokenNum];
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
            message.Subject = $"Dear {name}, you are the winner of the {sweepstakesName} sweepstakes!";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = @"This is not sketchy at all. Please follow these instructions to claim your prize."
            };

            message.From.Add(new MailboxAddress("Sweepstakes Notification System", emailFrom));
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
