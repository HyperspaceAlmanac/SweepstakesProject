using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweepstakesImplementation
{
    // Copy and pasted code of Email Checker from White Board Challenges worksheet
    // It should cover most of the cases. might be missing a few
    static class EmailChecker
    {
        private static bool DomainCheck(string str)
        {
            Regex r = new Regex(@"@.*[\-0-9a-zA-Z]+\.(org|com|net|int|edu|edu|gov|mil)$");
            return r.IsMatch(str);
        }

        public static bool ValidEmail(string str)
        {
            bool foundAtSign = false;
            bool insideQuote = false;
            HashSet<char> legalCharactersLocal = new HashSet<char>("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLNOPQRSTUVWXYZ!@#$%^&+-/=?_'{|}~".ToCharArray());
            HashSet<char> legalCharactersDomain = new HashSet<char>("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLNOPQRSTUVWXYZ".ToCharArray());
            char prev = ' ';
            char current;
            int labelLength = 0;
            for (int i = 0; i < str.Length; i++)
            {
                current = str[i];
                if (foundAtSign)
                {
                    // Domain
                    if (current == '@')
                    {
                        return false;
                    }
                    else if (current == '-')
                    {
                        if (prev == '@' || i == str.Length - 1)
                        {
                            return false;
                        }
                    }
                    else if (current == '.')
                    {
                        if (i == str.Length - 1)
                        {
                            return false;
                        }
                        if (prev == '@' || prev == '.')
                        {
                            return false;
                        }
                        labelLength = 0;
                    }
                    else if (!legalCharactersDomain.Contains(current))
                    {
                        return false;
                    }
                    if (current != '.')
                    {
                        labelLength += 1;
                    }
                    if (labelLength > 63)
                    {
                        return false;
                    }

                }
                else
                {
                    if (i >= 64)
                    {
                        // Longer tha 64 characters
                        return false;
                    }
                    // Local
                    if (insideQuote)
                    {
                        if (current == '"')
                        {
                            insideQuote = false;
                        }
                    }
                    else if (current == '"')
                    {
                        if (i == 0 || prev == '.')
                        {
                            insideQuote = true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else if (current == '@')
                    {
                        if (prev == '.')
                        {
                            return false;
                        }
                        else
                        {
                            foundAtSign = true;
                        }
                    }
                    else if (current == '.')
                    {
                        // Can't be first
                        if (i == 0)
                        {
                            return false;
                        }
                        else if (prev == '.')
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (!legalCharactersLocal.Contains(current))
                        {
                            return false;
                        }
                    }
                    //First character check
                    // First character before @ check
                }
                prev = current;
            }

            if (!foundAtSign)
            {
                return false;
            }
            return DomainCheck(str);
        }
    }
}
