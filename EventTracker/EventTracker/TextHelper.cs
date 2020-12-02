using System;
using System.Collections.Generic;
using System.Text;

namespace EventTracker
{
    public static class TextHelper
    {
        public static void PrintFormattedText (string name, EventType eventType, DateTime start, DateTime end)
        {
            Console.WriteLine(name + "-" + eventType + "-" + start + "-" + end);
        }
    }
}
