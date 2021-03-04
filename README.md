# SweepstakesProject
Dependency Injection Documentation

The MarketingFirm class takes in an object that implements the ISweeptaskesManager interface rather than instantiating it inside of MarketingFirm itself.
Thus, the dependency is injected into MarketingFirm class rather than MarketingFirm instantiating it and creating a new dependency.
MarketingFirm and ISweepstakesManager are now a lot more isolated from each other where changes to either will not affect the other. 

Currently there are SweepstakesStackManager and SweepstakesQueueManager classes that implement ISweepstakesManager interface, but in the future there can be new classes
that uses other data structures underneath such as SweepstakesHeapManager.
If a class such as SweepstakesHeapManager is implemented, it can just be passed into MarketingFirm without MarketingFirm needing to be updated.
If MarketingFirm implementation is changed to not use something that implements ISweepstakesManager, it can just change the input parameter into something else.

Overall this makes the dependency between ISweepstakesManager interface and MarketingFirm a lot easier to manage.

# But wait, there is more! If your marketing firm buys the Sweepstakes system, you get the notification system for FREE!
This notification system will keep track of all the sweepstakes that are going on, and allows your firm to automatically send messages to all
of the contestants when the sweepstakes is over, as well as being able to send an email to the winner.
# Observer and Depedency Injection

The Observer Notificaion System inherits from an interface with these Methods defind:

int RegisterSweepstakes();
int RegisterObserver(int tokenNum, ICanBeNotified observer);
void NotifyDrawingDone(int tokenNum, int observerToken, string sweepstakesName);

The Sweepstakes class has the Observer Notification System injected into it, and will use the sweepstakes token generated by EventNotification System
when it registers. The sweepstakes token will be used to let the observer notification system know which sweepstakes it is, and generates an observer token for each
observer that the Sweepstakes registers.

The implementation of how the Observer System generates the tokens can completely change without the need to change Sweepstakes.
The Sweepstakes class is now only responsible for notifying the system that an event has happened, and the event notification system will handle the rest.
The implementation for both sides are now isolated from each other, and they interact with each other through predefind protocols.

##############################
# Notes about email capabilities implementation:
## As a side effect of trying to get MailKit to work, the Project is using .Net 4.8

I have been testing the email sending functionalities with gmail accounts, and the project is hard coded for gmail server "smtp.gmail.com" and port 587.
I had to enable less secure apps (through specific link for those accounts) to not have to use OAuth 2.0.
Other email servers may have different requirements.
The emails and password(for the one that sends) are now placed into a static class and added to .gitignore.

Implementatio notes for SensitiveInfo class:
The SensitiveInfo class should be static.
It should have a public static string EmailSender.
It should have a public static string EmailPassword.
It should have have a Method GetRandomEmail() for retrieving a random email for EmailReceiver.



###################################
# This Project uses MailKit
###################################

I have MailKit as a Submodule, but not actually using it.
Ran into issues with dependencies and found out that I could have just used NuGet to install it.
Now the project is is using the MailKit version installed with NuGet.

# MailKit License
MIT License

Copyright (C) 2013-2020 .NET Foundation and Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.


