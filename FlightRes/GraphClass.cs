using System;
using System.Collections.Generic;

namespace FlightRes
{
    public class Flight
    {
        public string FlightID { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Duration { get; set; }
        public int Price { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class Booking
    {
        public string BookingID { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public LinkedList<string> FlightIDs { get; set; }
        public int TotalPrice { get; set; }
        public string Destination { get; set; }
    }

    public static class GraphClass
    {
        private static LinkedList<Flight> flights = new LinkedList<Flight>();
        private static LinkedList<Booking> bookings = new LinkedList<Booking>();
        private static int flightCounter = 1;
        private static int bookingCounter = 1;

        public static void AddFlight(int to, string depTime, string arrTime, int price, int seats)
        {
            flights.Add(new Flight
            {
                FlightID = $"FL{flightCounter.ToString().PadLeft(3, '0')}",
                From = 0,
                To = to,
                DepartureTime = depTime,
                ArrivalTime = arrTime,
                Duration = CalculateDuration(depTime, arrTime),
                Price = price,
                AvailableSeats = seats
            });
            flightCounter++;
        }

        private static string CalculateDuration(string depTime, string arrTime)
        {
            TimeSpan dep = TimeSpan.Parse(depTime);
            TimeSpan arr = TimeSpan.Parse(arrTime);

            if (arr < dep) arr += TimeSpan.FromDays(1);

            TimeSpan duration = arr - dep;
            return $"{(int)duration.TotalHours}h{duration.Minutes}m";
        }
        public static List<Flight> GetFlightsByCountry(int countryIndex)
        {
            var result = new List<Flight>();
            foreach (var flight in flights.GetAll())
            {
                if (flight.To == countryIndex && flight.AvailableSeats > 0)
                {
                    result.Add(flight);
                }
            }
            return result;
        }

        public static void ViewFlightDetails(string[] countries, int? countryIndex = null)
        {
            Console.WriteLine("\nAll Available Flights:");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-10} {5,-15} {6,-10}",
                             "Flight ID", "From", "To", "Departure", "Arrival", "Duration", "Seats");

            foreach (var flight in flights.GetAll())
            {
                if (flight.AvailableSeats > 0 &&
                    (!countryIndex.HasValue || flight.To == countryIndex))
                {
                    Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-10} {5,-15} {6,-10}",
                                    flight.FlightID,
                                    countries[flight.From],
                                    countries[flight.To],
                                    flight.DepartureTime,
                                    flight.ArrivalTime,
                                    flight.Duration,
                                    flight.AvailableSeats);
                }
            }
        }

        public static void BookFlight(string userName, LinkedList<string> flightIds, string date, string[] countries)
        {
            var booking = new Booking
            {
                BookingID = $"FB{bookingCounter.ToString().PadLeft(3, '0')}",
                UserName = userName,
                Date = date,
                FlightIDs = new LinkedList<string>(),
                TotalPrice = 0,
                Destination = ""
            };

            string destination = "";
            foreach (var flightId in flightIds.GetAll())
            {
                var flight = flights.Find(f => f.FlightID == flightId);
                if (flight == null || flight.AvailableSeats <= 0)
                {
                    Console.WriteLine($"Flight {flightId} not available. Booking cancelled.");
                    return;
                }
                booking.FlightIDs.Add(flightId);
                booking.TotalPrice += flight.Price;
                destination = countries[flight.To];
            }

            booking.Destination = destination;

            foreach (var flightId in flightIds.GetAll())
            {
                var flight = flights.Find(f => f.FlightID == flightId);
                flight.AvailableSeats--;
            }

            bookings.Add(booking);
            bookingCounter++;
            Console.WriteLine($"Booking successful! Booking ID: {booking.BookingID}");
        }

        public static void CancelBooking(string bookingId)
        {
            var booking = bookings.Find(b => b.BookingID == bookingId);
            if (booking != null)
            {
                foreach (var flightId in booking.FlightIDs.GetAll())
                {
                    var flight = flights.Find(f => f.FlightID == flightId);
                    if (flight != null) flight.AvailableSeats++;
                }

                bookings.Remove(b => b.BookingID == bookingId);
                Console.WriteLine("Booking cancelled successfully!");

                if (bookings.Count == 0) bookingCounter = 1;
            }
            else
            {
                Console.WriteLine("Invalid booking ID");
            }
        }

        public static void ViewBookings()
        {
            Console.WriteLine("\nAll Bookings:");
            Console.WriteLine("{0,-10} {1,-12} {2,-15} {3,-15} {4,-10}",
                            "Booking ID", "Date", "User Name", "Destination", "Total Price");

            foreach (var booking in bookings.GetAll())
            {
                Console.WriteLine("{0,-10} {1,-12} {2,-15} {3,-15} {4,-10}",
                                  booking.BookingID,
                                  booking.Date,
                                  booking.UserName,
                                  booking.Destination,
                                  booking.TotalPrice);
            }
        }

        public static bool AdminLogin(string password) => password == "admin123";

        public static void SortWithCountry(string[] countries)
        {
            flights.MergeSortBy(flight => countries[flight.To]);
        }

        public static void SortWithFlightID()
        {
            flights.MergeSortBy(flight => flight.FlightID);
        }
    }

}