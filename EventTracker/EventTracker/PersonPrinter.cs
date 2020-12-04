using System;
using System.Collections.Generic;
using System.Text;

namespace EventTracker
{
    public static class PersonPrinter
    {
        public static void PrintFormattedText(int i, string firstName, string lastName, string phoneNumber)
        {
            Console.WriteLine(i + ". " + firstName + " - " + lastName + " - " + phoneNumber);
        }
    }
}
