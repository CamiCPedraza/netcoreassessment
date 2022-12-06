using System;
using System.Collections.Generic;
using System.Linq;
using static TicketsConsole.Program;

/*

Let's say we're running a small entertainment business as a start-up. This means we're selling tickets to live events on a website. An email campaign service is what we are going to make here. We're building a marketing engine that will send notifications (emails, text messages) directly to the client and we'll add more features as we go.

Please, instead of debuging with breakpoints, debug with "Console.Writeline();" for each task because the Interview will be in Coderpad and in that platform you cant do Breakpoints.

*/

namespace TicketsConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            /*

            1. You can see here a list of events, a customer object. Try to understand the code, make it compile. 

            2.  The goal is to create a MarketingEngine class sending all events through the constructor as parameter and make it print the events that are 
            happening in the same city as the customer. To do that, inside this class, create a SendCustomerNotifications method which will receive a customer as 
            parameter and will mock the Notification Service. Add this ConsoleWriteLine inside the Method to mock the service. Inside this method you can add the 
            code you need to run this task correctly but you cant modify the console writeline: Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} 
            at {e.Date}");

            3. As part of a new campaign, we need to be able to let customers know about events that are coming up close to their next birthday. You can make a guess and 
            add it to the MarketingEngine class if you want to. So we still want to keep how things work now, which is that we email customers about events in their city or 
            the event closest to next customer's birthday, and then we email them again at some point during the year. The current customer, his birthday is on may. 
            So it's already in the past. So we want to find the next one, which is 23. How would you like the code to be built? We don't just want functionality; we want more 
            than that. We want to know how you plan to make that work. Please code it.

            4. The next requirement is to extend the solution to be able to send notifications for the five closest events to the customer. The interviewer here can 
            paste a method to help you, or ask you to search it. We will attach 2 different ways to calculate the distance. 

            // ATTENTION this row they don't tell you, you can google for it. In some cases, they pasted it so you can use it

            Option 1:
            var distance = Math.Abs(customerCityInfo.X - eventCityInfo.X) + Math.Abs(customerCityInfo.Y - eventCityInfo.Y);

            Option 2:
            private static int AlphebiticalDistance(string s, string t)
            {
                var result = 0;
                var i = 0;
                for(i = 0; i < Math.Min(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 1 i={i} {s.Length} {t.Length}");
                        result += Math.Abs(s[i] - t[i]);
                    }
                    for(; i < Math.Max(s.Length, t.Length); i++)
                    {
                        // Console.Out.WriteLine($"loop 2 i={i} {s.Length} {t.Length}");
                        result += s.Length > t.Length ? s[i] : t[i];
                    }
                    
                    return result;
            } 

            Tips of this Task:
            Try to use Lamba Expressions. Data Structures. Dictionary, ContainsKey method.

            5. If the calculation of the distances is an API call which could fail or is too expensive, how will you improve the code written in 4? 
            Think in caching the data which could be code it as a dictionary. You need to store the distances between two cities. Example:

            New York - Boston => 400 
            Boston - Washington => 540
            Boston - New York => Should not exist because "New York - Boston" is already stored and the distance is the same. 

            6. If the calculation of the distances is an API call which could fail, what can be done to avoid the failure? Think in HTTPResponse Answers: 
            Timeoute, 404, 403. How can you handle that exceptions? Code it.

            7.  If we also want to sort the resulting events by other fields like price, etc. to determine whichones to send to the customer, 
            how would you implement it? Code it.
            */

            var events = new List<Event>{
                new Event(1, "Phantom of the Opera", "New York", new DateTime(2023,12,23), 5000),
                new Event(2, "Metallica", "Los Angeles", new DateTime(2023,12,02), 1200),
                new Event(3, "Metallica", "New York", new DateTime(2023,12,06), 800),
                new Event(4, "Metallica", "Boston", new DateTime(2023,10,23), 1000),
                new Event(5, "LadyGaGa", "New York", new DateTime(2023,09,20), 2000),
                new Event(6, "LadyGaGa", "Boston", new DateTime(2023,08,01), 2500),
                new Event(7, "LadyGaGa", "Chicago", new DateTime(2023,07,04), 2800),
                new Event(8, "LadyGaGa", "San Francisco", new DateTime(2023,07,07), 4000),
                new Event(9, "LadyGaGa", "Washington", new DateTime(2023,05,22), 3000),
                new Event(10, "Metallica", "Chicago", new DateTime(2023,01,01), 3500),
                new Event(11, "Phantom of the Opera", "San Francisco", new DateTime(2023,07,04), 1500),
                new Event(12, "Phantom of the Opera", "Chicago", new DateTime(2024,05,15), 2500)
            };

            var customer = new Customer()
            {
                Id = 1,
                Name = "John",
                City = "New York",
                BirthDate = new DateTime(1995, 05, 10)
            };
            var market = new MarketingEngine(events);
            //market.CallForCustomerCity(customer);
            //market.CallForNextBirthDay(customer);
            //market.CallForCustomerCityDistance(customer);
            market.CallByPrice(customer);
        }

        public class Event
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime Date { get; set; }
            public double Price { get; set; }

            public Event(int id, string name, string city, DateTime date, double price)
            {
                this.Id = id;
                this.Name = name;
                this.City = city;
                this.Date = date;
                this.Price = price;
            }
        }

        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime BirthDate { get; set; }
        }

        public class EventDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime Date { get; set; }
            public double Price { get; set; }
            public int BirthDayTime { get; set; }
            public int Distance { get; set; }

            public EventDto(int id, string name, string city, DateTime date, double price, int birthDayTime, int distance)
            {
                this.Id = id;
                this.Name = name;
                this.City = city;
                this.Date = date;
                this.Price = price;
                this.BirthDayTime = birthDayTime;
                Distance = distance;
            }
        }

        public class MarketingEngine
        {
            private readonly List<Event> eventList;
            public record City(string Name, int X, int Y);
            private readonly Dictionary<string, int> distanceDictionary = new();

            public MarketingEngine(List<Event> eventListParameter)
            {
                eventList = eventListParameter;
            }

            private void SendCustomerNotifications(Customer customer, Event e)
            {
                if (distanceDictionary.Any())
                {
                    var eventDistanceValuePair = distanceDictionary.First(d => d.Key.Contains($"{customer.City}-{e.City}"));
                    Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date} in {e.City} distante to the City {eventDistanceValuePair.Value}");
                }
                else
                {
                    Console.WriteLine($"{customer.Name} from {customer.City} event {e.Name} at {e.Date} in {e.City}");
                }
            }

            public void CallForCustomerCity(Customer customer)
            {
                var eventsByCity = eventList.Where(c => c.City.Equals(customer.City)).ToList();
                foreach (var ev in eventsByCity)
                {
                    SendCustomerNotifications(customer, ev);
                }
            }

            public void CallForNextBirthDay(Customer customer)
            {
                var nextB = new DateTime();
                var eventsByTimeToBirthDay = new List<EventDto>();
                var birthDay = customer.BirthDate.AddYears(DateTime.Today.Year - customer.BirthDate.Year);
                if (birthDay < DateTime.Today) nextB = birthDay.AddYears(1);
                foreach (var eventToBirthDay in eventList)
                {
                    if (eventToBirthDay.Date > nextB)
                    {
                        var timetoBirthDay = (eventToBirthDay.Date - nextB).Days;
                        eventsByTimeToBirthDay.Add(new EventDto(eventToBirthDay.Id, eventToBirthDay.Name, eventToBirthDay.City, eventToBirthDay.Date, 0, timetoBirthDay, 0));
                    }
                }
                var orderedList = eventsByTimeToBirthDay.OrderBy(o => o.BirthDayTime);
                var eventFromDto = orderedList.Take(5).ToList();
                foreach (var eventToSend in eventFromDto)
                {
                    SendCustomerNotifications(customer, new Event(eventToSend.Id, eventToSend.Name, eventToSend.City, eventToSend.Date, eventToSend.Price));

                }
            }

            public static readonly IDictionary<string, City> Cities = new Dictionary<string, City>()
        {
            { "New York", new City("New York", 3572, 1455) },
            { "Los Angeles", new City("Los Angeles", 462, 975) },
            { "San Francisco", new City("San Francisco", 183, 1233) },
            { "Boston", new City("Boston", 3778, 1566) },
            { "Chicago", new City("Chicago", 2608, 1525) },
            { "Washington", new City("Washington", 3358, 1320) },
        };

            public void CallForCustomerCityDistance(Customer customer)
            {
                var listOfDistance = new List<EventDto>();
                try
                {
                    var customerCityCoord = Cities.FirstOrDefault(c => c.Key.Equals(customer.City));
                    foreach (var eventToGet in eventList)
                    {
                        var cityEvent = Cities.First(e => e.Key.Equals(eventToGet.City));
                        var distance = Math.Abs(customerCityCoord.Value.X - cityEvent.Value.X) + Math.Abs(customerCityCoord.Value.Y - cityEvent.Value.Y);
                        if (!distanceDictionary.ContainsKey($"{customer.City}-{eventToGet.City}"))
                            AddToDictionary(customer.City, eventToGet.City, distance);
                        else
                            distance = distanceDictionary.First(p => p.Key.Contains($"{eventToGet.City}")).Value;
                        listOfDistance.Add(new EventDto(eventToGet.Id, eventToGet.Name, eventToGet.City, eventToGet.Date, 0, 0, distance));
                    }

                    foreach (var eventToSend in listOfDistance.OrderBy(o => o.Distance).Take(5))
                    {
                        var e = new Event(eventToSend.Id, eventToSend.Name, eventToSend.City, eventToSend.Date, eventToSend.Price);
                        SendCustomerNotifications(customer, e);
                    }
                }
                catch (TimeoutException tEx)
                {
                    Console.WriteLine($"There was a time out exception getting the Dictionary information at {typeof(MarketingEngine)} :: Exception {tEx.Message}");
                }
            }

            public void CallByPrice(Customer customer)
            {
                var eventsOrderedByPrice = eventList.OrderBy(p => p.Price).Take(5).ToList();
                foreach (var eventToSend in eventsOrderedByPrice)
                {
                    SendCustomerNotifications(customer, eventToSend);
                }
            }

            public void AddToDictionary(string customerCity, string eventCity, int distance)
            {
                distanceDictionary.Add($"{customerCity}-{eventCity}", distance);
            }
        }


        /*-------------------------------------
        Coordinates are roughly to scale with miles in the USA
           2000 +----------------------+  
                |                      |  
                |                      |  
             Y  |                      |  
                |                      |  
                |                      |  
                |                      |  
                |                      |  
             0  +----------------------+  
                0          X          4000
        ---------------------------------------*/

    }
}
