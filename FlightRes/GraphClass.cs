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
        public LinkedList<string> FlightIDs { get; set; }
        public int TotalPrice { get; set; }
    }

    public static class GraphClass
    {
        private static LinkedList<Flight> flights = new LinkedList<Flight>();
        private static LinkedList<Booking> bookings = new LinkedList<Booking>();
        private static int flightCounter = 1;
        private static int bookingCounter = 1;
        
        public static void AddFlight(int from, int to, string depTime, string arrTime, 
                                   string duration, int price, int seats)
        {
            flights.Add(new Flight
            {
                FlightID = $"FL{flightCounter.ToString().PadLeft(3, '0')}",
                From = from,
                To = to,
                DepartureTime = depTime,
                ArrivalTime = arrTime,
                Duration = duration,
                Price = price,
                AvailableSeats = seats
            });
            flightCounter++;
        }

        public static void ViewFlightDetails(string[] countries)
        {
            Console.WriteLine("\nAll Available Flights:");
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10} {4,-10} {5,-15} {6,-10}", 
                            "Flight ID", "From", "To", "Departure", "Arrival", "Duration", "Seats");
            
            foreach (var flight in flights.GetAll())
            {
                if (flight.AvailableSeats > 0)
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

        public static void BookFlight(string userName, LinkedList<string> flightIds)
        {
            var booking = new Booking
            {
                BookingID = $"FB{bookingCounter.ToString().PadLeft(3, '0')}",
                UserName = userName,
                FlightIDs = new LinkedList<string>(),
                TotalPrice = 0
            };

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
            }

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
                // Restore seats for all flights in the booking
                foreach (var flightId in booking.FlightIDs.GetAll())
                {
                    var flight = flights.Find(f => f.FlightID == flightId);
                    if (flight != null) flight.AvailableSeats++;
                }
                
                // Remove the booking
                bookings.Remove(b => b.BookingID == bookingId);
                Console.WriteLine("Booking cancelled successfully!");
                
                // Reset booking counter if no bookings remain
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
            Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10}", 
                            "Booking ID", "User Name", "Flights", "Total Price");
            
            foreach (var booking in bookings.GetAll())
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-15} {3,-10}",
                                  booking.BookingID,
                                  booking.UserName,
                                  string.Join("+", booking.FlightIDs.GetAll()),
                                  booking.TotalPrice);
            }
        }

        public static bool AdminLogin(string password) => password == "admin123";
    }
}