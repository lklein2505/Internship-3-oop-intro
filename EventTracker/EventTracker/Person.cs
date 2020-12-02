using System;
using System.Collections.Generic;
using System.Text;

namespace EventTracker
{
    public class Person
    {
        public Person(string firstName, string lastName, string oib, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Oib = oib;
            PhoneNumber = phoneNumber;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Oib { get; set; }
        public string PhoneNumber { get; set; }
    }
}
