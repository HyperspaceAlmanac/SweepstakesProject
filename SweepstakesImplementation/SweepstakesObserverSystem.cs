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
    class SweepstakesObserverSystem
    {
        private Dictionary<int, List<INotification>> sweepstakes;
        private int sweepstakesToken;

        public SweepstakesObserverSystem()
        {
            sweepstakes = new Dictionary<int, List<INotification>>();
            sweepstakesToken = 0;
        }

        // A sweepstakes calls this to get assigned a token to index into dictionary 
        public int RegisterSweepstakes() {
            int tokenNum = sweepstakesToken;
            sweepstakesToken += 1;
            sweepstakes[tokenNum] = new List<INotification>();
            return tokenNum;
        }

        // Sweepstake provides token that it used when registered, and use it to
        // Let the system know to register a new observer into that List
        public int RegisterObserver(int tokenNum, INotification observer) {
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
        public void NotifyDrawingDone(int tokenNum, int observerToken, string winningMessage, string other, ICanBeEmailed info) {
            if (sweepstakes.ContainsKey(tokenNum)) {
                List<INotification> observers = sweepstakes[tokenNum];
                for (int i = 0; i < observers.Count; i++)
                {
                    if (i == observerToken)
                    {
                        observers[i].Notify(winningMessage);
                    }
                    else
                    {
                        observers[i].Notify(other);
                    }
                }
            }
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

            message.From.Add(new MailboxAddress("Sweepstakes Notification System", emailFrom));
            message.To.Add(new MailboxAddress(winner.FirstName + " " + winner.LastName, emailTo));

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(emailFrom, password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
