using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata.Ecma335;

namespace EventTracker
{
    class Program
    {
        public static bool exit = false;

        static void Main(string[] args)
        {
            var rNadal = new Person("Rafael", "Nadal", "25184252", "0915847824");
            var rFederer = new Person("Roger", "Federer", "42324245", "0956292298");
            var mCilic = new Person("Marin", "Cilic", "28466144", "0954878854");
            var aZverev = new Person("Alex", "Zverev", "28433244", "0912632254");
            var bPaire = new Person("Benoit", "Paire", "84747714", "0982256647");
            var nKyrgios = new Person("Nick", "Kyrgios", "48557414", "0911142257");
            var ivosCoffee = new Event("Coffee with Dr. Ivo", 0, new DateTime(2013, 1, 23, 17, 30, 00), new DateTime(2013, 1, 23, 19, 30, 00));
            var backhandLecture = new Event("Backhand intro", 0, new DateTime(2013, 1, 23, 19, 45, 00), new DateTime(2013, 1, 23, 21, 45, 00));
            var agassiConcert = new Event("Karaoke with Agassi", 0, new DateTime(2013, 8, 15, 20, 45, 00), new DateTime(2013, 8, 15, 21, 45, 00));

            var ivosList = new List<Person>() { rNadal, rFederer };
            var backhandList = new List<Person>() { mCilic, aZverev };
            var concertList = new List<Person>() { bPaire, nKyrgios };

            var persons = new List<Person>() { rNadal, rFederer, mCilic, aZverev, bPaire, nKyrgios };

            var events = new List<Event>() { ivosCoffee, backhandLecture, agassiConcert };

            var eventDict = new Dictionary<Event, List<Person>>()
            {
                {ivosCoffee, ivosList },
                {backhandLecture, backhandList },
                {agassiConcert, concertList }
            };
            Console.WriteLine(eventDict);

            while (!exit)
            {
                Console.WriteLine("" +
                    "1 - Dodaj event\n" +
                    "2 - Brisi event\n" +
                    "3 - Editaj event\n" +
                    "4 - Dodaj osobu na event\n" +
                    "5 - Ukloni osobu s eventa\n" +
                    "6 - Podmeni za ispis detalja\n" +
                    "0 - Izlaz iz aplikacije\n" +
                    "Odaberite akciju:\n");

                var choosenAction = int.Parse(Console.ReadLine());
                switch (choosenAction)
                {
                    case 1:
                        AddEvent(events, eventDict);
                        break;
                    case 2:
                        DeleteEvent(events);
                        break;
                }
            }
            static void AddEvent(List<Event> events, Dictionary<Event, List<Person>> eventDict)
            {
                var newEvent = new Event();

                var flag1 = false;
                var flag2 = false;
                foreach (var Event in events)
                {
                    if ((newEvent.Start >= Event.Start && newEvent.Start <= Event.End) || (newEvent.End >= Event.Start && newEvent.End <= Event.End))
                    {
                        Console.WriteLine("Vrijeme eventa vec se podudara s vremenom postojeceg eventa!");
                        flag1 = true;
                        break;
                    }
                }
                foreach (var Event in events)
                {
                    if (newEvent.Name.ToUpper() == Event.Name.ToUpper())
                    {
                        Console.WriteLine("Vec postoji event istog imena!");
                        flag2 = true;
                        break;
                    }
                }
                if (flag1 == false && flag2 == false)
                {
                    events.Add(newEvent);
                    eventDict.Add(newEvent, newEvent.eventGoers);
                }
            }

            static void DeleteEvent(List<Event> events)
            {
                Console.WriteLine("Unesite ime eventa kojeg zelite ukloniti: ");
                var deleteChoice = Console.ReadLine();

                var flag = false;
                foreach (var Event in events)
                {
                    if (deleteChoice.ToUpper() == Event.Name.ToUpper())
                    {
                        events.Remove(Event);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    Console.WriteLine("Event s tim imenom ne postoji!");
            }
        }
    }
}


