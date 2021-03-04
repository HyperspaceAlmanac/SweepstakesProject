using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    interface ICanBeNotified
    {
        void Notify(string message);
        string ContactEmail();
        string FullName();
    }
}
