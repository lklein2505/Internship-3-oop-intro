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

        public Person()
        {
            PersonInput();
        }

        public void PersonInput()
        {
            Console.WriteLine("\nUnesite ime osobe: ");
            FirstName = Console.ReadLine();

            Console.WriteLine("\nUnesite prezime osobe: ");
            LastName = Console.ReadLine();

            Console.WriteLine("\nUnesite OIB osobe");
            Oib = Console.ReadLine();

            Console.WriteLine("\nUnesite broj mobitela osobe");
            PhoneNumber = Console.ReadLine();
        }
    }
}
