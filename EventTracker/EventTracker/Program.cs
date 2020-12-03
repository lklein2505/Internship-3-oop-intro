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
            var backhandLecture = new Event("Backhand intro", EventType.Lecture, new DateTime(2013, 1, 23, 19, 45, 00), new DateTime(2013, 1, 23, 21, 45, 00));
            var agassiConcert = new Event("Karaoke with Agassi", EventType.Concert, new DateTime(2013, 8, 15, 20, 45, 00), new DateTime(2013, 8, 15, 21, 45, 00));

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
                    case 3:
                        EditEvent(events);
                        break;
                    case 4:
                        AddPerson(events, persons, eventDict);
                        break;
                    case 5:
                        RemovePerson(events, persons, eventDict);
                        break;
                    case 6:
                        PrintEvents(events);
                        break;
                }
            }
            static void AddEvent(List<Event> events, Dictionary<Event, List<Person>> eventDict)
            {
                var newEvent = new Event();
                
                var flag1 = SameNameCheck(newEvent, events);

                var flag2 = TimeOverlapCheck(newEvent, events);

                var flag3 = TimeErrorCheck(newEvent);
                
                if (flag1 == false && flag2 == false && flag3 == false)
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

            static void EditEvent(List<Event> events)
            {
                Console.WriteLine("Unesite ime eventa kojeg biste htjeli urediti: ");
                var editChoice = Console.ReadLine();

                foreach (var Event in events)
                {
                    if (editChoice.ToUpper() == Event.Name.ToUpper())
                    {
                        var endFlag = false;
                        while (endFlag == false)
                        {
                            Console.WriteLine("Želite li mijenjati ime eventa? (y/n)");
                            var editName = Console.ReadLine();
                            if (editName.ToUpper() != "Y" && editName.ToUpper() != "N")
                            {
                                Console.WriteLine("Pogrešan unos! Unesite ili 'y' ili 'n'");
                                continue;
                            }
                            else if (editName.ToUpper() == "Y")
                            {
                                Console.WriteLine("Unesite novo ime: ");
                                var newName = Console.ReadLine();
                                var flag = SameNameCheck(Event, events);
                                if (flag == false)
                                    Event.Name = newName;
                                else
                                    break;
                            }
                            
                            Console.WriteLine("Želite li mijenjati tip eventa? (y/n)");
                            var editType = Console.ReadLine();
                            if (editType.ToUpper() != "Y" && editType.ToUpper() != "N")
                            {
                                Console.WriteLine("Pogrešan unos! Unesite ili 'y' ili 'n'");
                                continue;
                            }
                            else if (editType.ToUpper() == "Y")
                            {
                                var flag = false;
                                while (flag == false)
                                {
                                    Console.WriteLine("Unesite novi tip (Coffee, Lecture, Concert, StudySession): ");
                                    var newType = Console.ReadLine().ToUpper();

                                    switch (newType)
                                    {
                                        case "COFFEE":
                                            Event.TypeOfEvent = EventType.Coffee;
                                            flag = true;
                                            break;

                                        case "LECTURE":
                                            Event.TypeOfEvent = EventType.Lecture;
                                            flag = true;
                                            break;

                                        case "CONCERT":
                                            Event.TypeOfEvent = EventType.Concert;
                                            flag = true;
                                            break;

                                        case "STUDYSESSION":
                                            Event.TypeOfEvent = EventType.StudySession;
                                            flag = true;
                                            break;

                                        default:
                                            Console.WriteLine("Greska! Unijeli ste pogresan tip eventa!\n");
                                            break;
                                    }
                                }                                
                            }
                            Console.WriteLine("Želite li mijenjati datum i vrijeme početka eventa? (y/n)");
                            var editStart = Console.ReadLine();
                            if (editStart.ToUpper() != "Y" && editStart.ToUpper() != "N")
                            {
                                Console.WriteLine("Pogrešan unos! Unesite ili 'y' ili 'n'");
                                continue;
                            }
                            else if (editStart.ToUpper() == "Y")
                            {
                                Console.WriteLine("Unesite novi datum i vrijeme početka: ");
                                var newStart = DateTime.Parse(Console.ReadLine());                             
                                Event.Start = newStart;                                
                            }

                            Console.WriteLine("Želite li mijenjati datum i vrijeme zavšetka eventa? (y/n)");
                            var editEnd = Console.ReadLine();
                            if (editEnd.ToUpper() != "Y" && editEnd.ToUpper() != "N")
                            {
                                Console.WriteLine("Pogrešan unos! Unesite ili 'y' ili 'n'");
                                continue;
                            }
                            else if (editEnd.ToUpper() == "Y")
                            {
                                Console.WriteLine("Unesite novi datum i vrijeme završetka: ");
                                var newEnd = DateTime.Parse(Console.ReadLine());
                                var flag = TimeOverlapCheck(Event, events);
                                if (flag == false && DateTime.Compare(newEnd, Event.Start) > 0)
                                    Event.End = newEnd;
                                else
                                {
                                    Console.WriteLine("Greška pri postavljanju novog datuma!");
                                    break;
                                }                                    
                            }
                            else if (editEnd.ToUpper() == "N")
                                endFlag = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Greška! Ne postoji event s tim imenom!");
                        break;
                    }
                }
            }

            static void AddPerson(List<Event> events, List<Person> persons, Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("Unesite ime eventa na koji biste htjeli dodati osobu: ");
                var addOnEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in events)
                {
                    if (Event.Name.ToUpper() == addOnEvent)
                    {
                        flag = true;
                        break;
                    }                    
                }
                if (flag == true)
                    Console.WriteLine("\nEvent postoji\n");
                else
                {
                    Console.WriteLine("\nEvent ne postoji\n");
                    return;
                }                   

                var newPerson = new Person();

                foreach (var person in persons)
                {
                    if (newPerson.Oib == person.Oib)
                    {
                        Console.WriteLine("\nGreška! Već postoji osoba s istim OIB-om!");
                        return;
                    }                        
                }
                persons.Add(newPerson);

                foreach (var Event in eventDict)
                {
                    if (Event.Key.Name.ToUpper() == addOnEvent.ToUpper())
                        Event.Value.Add(newPerson);                    
                }
            }

            static void RemovePerson(List<Event> events, List<Person> persons, Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("\nUnesite ime eventa s kojeg bi htjeli maknuti osobu: ");
                var removeFromEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in events)
                {
                    if (Event.Name.ToUpper() == removeFromEvent)
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag == true)
                    Console.WriteLine("\nEvent postoji\n");
                else
                {
                    Console.WriteLine("\nEvent ne postoji\n");
                    return;
                }

                Console.WriteLine("\nUnesite OIB osobe koju želite maknuti s eventa: ");
                var personToRemove = Console.ReadLine();

                foreach (var Person in persons)
                {
                    if (Person.Oib == personToRemove)
                    {
                        foreach (var Event in eventDict)
                        {
                            if (Event.Key.Name.ToUpper() == removeFromEvent)
                            {
                                foreach (var Oib in persons)
                                {                                    
                                    if (Oib.Oib == personToRemove)
                                    {
                                        foreach (var Item in eventDict)
                                        {
                                            if (Item.Key.Name.ToUpper() == removeFromEvent)
                                            {
                                                Item.Value.Remove(Person);
                                                return;
                                            }
                                        }
                                    }                                        
                                }                                
                            }
                        }
                    }
                }
                Console.WriteLine("Greška! Osoba nije na eventu!");
            }

            static void PrintEvents(List<Event> events)
            {
                Console.WriteLine("\nUnesite ime eventa čije želite saznati detalje: ");
                var showEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in events)
                {                    
                    if (Event.Name.ToUpper() == showEvent)
                    {
                        TextHelper.PrintFormattedText(Event.Name, Event.TypeOfEvent, Event.Start, Event.End);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    Console.WriteLine("\nNe postoji event tog imena!");
            }



            static bool TimeOverlapCheck(Event newEvent, List<Event> events)
            {
                foreach (var Event in events)
                {
                    if (((DateTime.Compare(newEvent.Start, Event.Start) > 0 && DateTime.Compare(newEvent.Start, Event.End) > 0) || (DateTime.Compare(newEvent.Start, Event.Start) < 0 && DateTime.Compare(newEvent.Start, Event.End) < 0) ||
                        (DateTime.Compare(newEvent.End, Event.Start) > 0 && DateTime.Compare(newEvent.End, Event.End) > 0) || (DateTime.Compare(newEvent.End, Event.Start) < 0 && DateTime.Compare(newEvent.End, Event.End) < 0)))
                    {
                        Console.WriteLine("Vrijeme eventa vec se podudara s vremenom postojeceg eventa!");
                        return true;
                    }                    
                }
                return false;
            }

            static bool TimeErrorCheck(Event newEvent)
            {
                if (DateTime.Compare(newEvent.End, newEvent.Start) < 0)
                {
                    Console.WriteLine("Greška! Vrijeme početka ne može biti nakon vremena završetka eventa!");
                    return true;
                }
                else
                    return false;
            }

            static bool SameNameCheck(Event newEvent, List<Event> events)
            {
                foreach (var Event in events)
                {
                    if (newEvent.Name.ToUpper() == Event.Name.ToUpper())
                    {
                        Console.WriteLine("Vec postoji event istog imena!");
                        return true;                        
                    }
                }
                return false;
            }            
        }
    }
}


