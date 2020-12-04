using System;
using System.Collections.Generic;
using System.Text;

namespace EventTracker
{
    public static class EventPrinter
    {
        public static void PrintFormattedText (string name, EventType eventType, DateTime start, DateTime end, TimeSpan span, int counter)
        {
            Console.WriteLine("\nDetalji eventa:");
            Console.WriteLine(name + " - " + eventType + " - " + start + " - " + end + " - " + span.Hours + " hour(s) - " + counter);
        }
    }
}
