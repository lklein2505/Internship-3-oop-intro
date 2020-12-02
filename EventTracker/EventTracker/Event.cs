using System;
using System.Collections.Generic;
using System.Text;

namespace EventTracker
{
    public enum EventType
    {
        Coffee,
        Lecture,
        Concert,
        StudySession
    }
    public class Event
    {
        public Event(string name, EventType typeOfEvent, DateTime start, DateTime end)
        {
            Name = name;
            Start = start;
            End = end;
            TypeOfEvent = typeOfEvent;
        }

        public EventType TypeOfEvent { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<Person> eventGoers { get; set; }

        public Event()
        {
            EventInput();
        }

        public void EventInput()
        {
            Console.WriteLine("Unesite ime novog eventa: ");
            Name = Console.ReadLine();

            Console.WriteLine("Unesite tip novog eventa: (Coffee, Lecture, Concert, StudySession)");
            var eventTypeChoosen = Console.ReadLine().ToUpper();
            var flag = false;

            switch (eventTypeChoosen)
            {
                case "COFFEE":
                    TypeOfEvent = EventType.Coffee;
                    break;

                case "LECTURE":
                    TypeOfEvent = EventType.Lecture;
                    break;

                case "CONCERT":
                    TypeOfEvent = EventType.Concert;
                    break;

                case "STUDYSESSION":
                    TypeOfEvent = EventType.StudySession;
                    break;

                default:
                    Console.WriteLine("Greska! Unijeli ste pogresan tip eventa!\n");
                    flag = true;
                    break;
            }
            if (flag == false)
            {
                Console.WriteLine("Unesite datum i vrijeme pocetka eventa: ");
                while (true)
                {
                    try
                    {
                        Start = DateTime.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Pogresan unos datuma i vremena! Pokusajte ponovo!\n");
                    }
                }

                Console.WriteLine("Unesite datum i vrijeme kraja eventa: ");
                while (true)
                {
                    try
                    {
                        Start = DateTime.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Pogresan unos datuma i vremena! Pokusajte ponovo!\n");
                    }
                }
            }
            else return;
        }
    }
}
