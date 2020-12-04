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
            var ivosCoffee = new Event("Coffee with Dr. Ivo", EventType.Coffee, new DateTime(2013, 1, 23, 17, 30, 00), new DateTime(2013, 1, 23, 19, 30, 00));
            var backhandLecture = new Event("Backhand intro", EventType.Lecture, new DateTime(2013, 1, 23, 19, 45, 00), new DateTime(2013, 1, 23, 21, 45, 00));
            var agassiConcert = new Event("Karaoke with Agassi", EventType.Concert, new DateTime(2013, 8, 15, 20, 45, 00), new DateTime(2013, 8, 15, 21, 45, 00));

            var persons = new List<Person>();
            persons.Add(rFederer);
            persons.Add(rNadal);
            persons.Add(mCilic);
            persons.Add(aZverev);
            persons.Add(bPaire);
            persons.Add(nKyrgios);

            ivosCoffee.eventGoers.Add(rNadal);
            ivosCoffee.eventGoers.Add(rFederer);
            backhandLecture.eventGoers.Add(mCilic);
            backhandLecture.eventGoers.Add(aZverev);
            agassiConcert.eventGoers.Add(bPaire);
            agassiConcert.eventGoers.Add(nKyrgios);

            var eventDict = new Dictionary<Event, List<Person>>()
            {
                {ivosCoffee, ivosCoffee.eventGoers },
                {backhandLecture, backhandLecture.eventGoers },
                {agassiConcert, agassiConcert.eventGoers }
            };

            while (!exit)
            {
                Console.WriteLine("\n" +
                    "1 - Dodaj event\n" +
                    "2 - Brisi event\n" +
                    "3 - Editaj event\n" +
                    "4 - Dodaj osobu na event\n" +
                    "5 - Ukloni osobu s eventa\n" +
                    "6 - Izbornik za ispis detalja\n" +
                    "0 - Izlaz iz aplikacije\n" +
                    "Odaberite akciju:\n");

                var choosenAction = int.Parse(Console.ReadLine());
                switch (choosenAction)
                {
                    case 1:
                        AddEvent(eventDict);
                        break;
                    case 2:
                        DeleteEvent(eventDict);
                        break;
                    case 3:
                        EditEvent(eventDict);
                        break;
                    case 4:
                        AddPerson(persons, eventDict);
                        break;
                    case 5:
                        RemovePerson(persons, eventDict);
                        break;
                    case 6:
                        Submenu(eventDict);                        
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Greška! Unijeli ste pogrešan broj. Unesite broj od 0 do 6!");
                        break;
                }
            }

            static void AddEvent(Dictionary<Event, List<Person>> eventDict)
            {
                var newEvent = new Event();
                
                var flag1 = SameNameCheck(newEvent, eventDict);

                var flag2 = TimeOverlapCheck(newEvent, eventDict);

                var flag3 = TimeErrorCheck(newEvent);
                
                if (flag1 == false && flag2 == false && flag3 == false)
                {
                    eventDict.Add(newEvent, newEvent.eventGoers);
                }
            }

            static void DeleteEvent(Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("Unesite ime eventa kojeg zelite ukloniti: ");
                var deleteChoice = Console.ReadLine();

                var flag = false;
                foreach (var Event in eventDict)
                {
                    if (deleteChoice.ToUpper() == Event.Key.Name.ToUpper())
                    {
                        eventDict.Remove(Event.Key);
                        flag = true;
                        break;
                    }
                }
                if (flag == false)
                    Console.WriteLine("Event s tim imenom ne postoji!");
            }

            static void EditEvent(Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("Unesite ime eventa kojeg biste htjeli urediti: ");
                var editChoice = Console.ReadLine();

                foreach (var Event in eventDict)
                {
                    if (editChoice.ToUpper() == Event.Key.Name.ToUpper())
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
                                var flag = SameNameCheck(Event.Key, eventDict);
                                if (flag == false)
                                    Event.Key.Name = newName;
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
                                            Event.Key.TypeOfEvent = EventType.Coffee;
                                            flag = true;
                                            break;

                                        case "LECTURE":
                                            Event.Key.TypeOfEvent = EventType.Lecture;
                                            flag = true;
                                            break;

                                        case "CONCERT":
                                            Event.Key.TypeOfEvent = EventType.Concert;
                                            flag = true;
                                            break;

                                        case "STUDYSESSION":
                                            Event.Key.TypeOfEvent = EventType.StudySession;
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
                                Event.Key.Start = newStart;                                
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
                                var flag = TimeOverlapCheck(Event.Key, eventDict);
                                if (flag == false && DateTime.Compare(newEnd, Event.Key.Start) > 0)
                                    Event.Key.End = newEnd;
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

            static void AddPerson(List<Person> persons, Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("Unesite ime eventa na koji biste htjeli dodati osobu: ");
                var addOnEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in eventDict)
                {
                    if (Event.Key.Name.ToUpper() == addOnEvent)
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

            static void RemovePerson(List<Person> persons, Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("\nUnesite ime eventa s kojeg bi htjeli maknuti osobu: ");
                var removeFromEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in eventDict)
                {
                    if (Event.Key.Name.ToUpper() == removeFromEvent)
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

            static string PrintEvents(Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("\nUnesite ime eventa čije želite saznati detalje: ");
                var showEvent = Console.ReadLine().ToUpper();

                var flag = false;
                foreach (var Event in eventDict)
                {                    
                    if (Event.Key.Name.ToUpper() == showEvent)
                    {
                        TimeSpan span = Event.Key.End - Event.Key.Start;
                        EventPrinter.PrintFormattedText(Event.Key.Name, Event.Key.TypeOfEvent, Event.Key.Start, Event.Key.End, span, Event.Value.Count);
                        flag = true;
                        return showEvent;                        
                    }
                }
                if (flag == false)                
                    Console.WriteLine("\nNe postoji event tog imena!");           
                var error = "Greška!";
                return error;
            }

            static Event ChooseEvent(Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("\nUnesite ime eventa čije sudionike želite znati: ");                

                while (true)
                {
                    var choosenEvent = Console.ReadLine().ToUpper();
                    foreach (var Event in eventDict)
                    {
                        if (Event.Key.Name.ToUpper() == choosenEvent)
                        {
                            foreach(var Item in eventDict)
                            {
                                if (Item.Key.Name.ToUpper() == choosenEvent)
                                    return Item.Key;
                            }
                        }
                        else
                            Console.WriteLine("\nEvent s tim imenom ne postoji!");
                    }                    
                }
                            
            }



            static bool TimeOverlapCheck(Event newEvent, Dictionary<Event, List<Person>> eventDict)
            {
                foreach (var Event in eventDict)
                {
                    if (((DateTime.Compare(newEvent.Start, Event.Key.Start) > 0 && DateTime.Compare(newEvent.Start, Event.Key.End) < 0) || (DateTime.Compare(newEvent.Start, Event.Key.Start) < 0 && DateTime.Compare(newEvent.Start, Event.Key.End) > 0) ||
                        (DateTime.Compare(newEvent.End, Event.Key.Start) > 0 && DateTime.Compare(newEvent.End, Event.Key.End) < 0) || (DateTime.Compare(newEvent.End, Event.Key.Start) < 0 && DateTime.Compare(newEvent.End, Event.Key.End) > 0)))                    
                    {
                        Console.WriteLine("Vrijeme eventa vec se podudara s vremenom postojeceg eventa!");
                        return true;
                    }                    
                }
                return false;
            }

            static bool TimeErrorCheck(Event newEvent)
            {
                if (DateTime.Compare(newEvent.End, newEvent.Start) > 0)
                {
                    Console.WriteLine("Greška! Vrijeme početka ne može biti nakon vremena završetka eventa!");
                    return true;
                }
                else
                    return false;
            }

            static bool SameNameCheck(Event newEvent, Dictionary<Event, List<Person>> eventDict)
            {
                foreach (var Event in eventDict)
                {
                    if (newEvent.Name.ToUpper() == Event.Key.Name.ToUpper())
                    {
                        Console.WriteLine("Vec postoji event istog imena!");
                        return true;                        
                    }
                }
                return false;
            }  
            
            static void PrintPersons(List<Person> persons)
            {
                Console.WriteLine("\nSudionici na eventu:");
                var i = 1;
                foreach (var Person in persons)
                {                    
                    PersonPrinter.PrintFormattedText(i, Person.FirstName, Person.LastName, Person.PhoneNumber);
                    i++;
                }
            }            
            
            static void Submenu(Dictionary<Event, List<Person>> eventDict)
            {
                Console.WriteLine("\n1 - Ispis detelja eventa\n" +
                    "2 - Ispis svih osoba na eventu\n" +
                    "3 - Ispis svih detalja\n" +
                    "4 - Natrag u glavni izbornik\n" +
                    "Odaberite akciju:");

                var choosenAction = int.Parse(Console.ReadLine());
                var flag = false;
                while (flag == false)
                {
                    switch (choosenAction)
                    {
                        case 1:
                            PrintEvents(eventDict);                            
                            break; 
                        case 2:
                            var choosenEvent = ChooseEvent(eventDict);
                            PrintPersons(choosenEvent.eventGoers);
                            break;
                        case 3:
                            var choosenEventAll = PrintEvents(eventDict);
                            if (choosenEventAll != "Greška!")
                                foreach (var Event in eventDict)
                                {
                                    if (choosenEventAll == Event.Key.Name.ToUpper())
                                    {
                                        PrintPersons(Event.Key.eventGoers);
                                        break;
                                    }
                                    break;
                                }
                            break;                       
                        case 4:
                            flag = true;
                            break;
                    }
                }                
            }
        }
    }
}


