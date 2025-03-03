using System;

namespace FlightRes
{
    class Program
    {
        private static string[] countries = {
            "Sri Lanka", "Australia", "Canada", "USA", "Italy",
            "England", "Japan", "China", "India", "Dubai",
            "Russia", "Singapore", "Doha"
        };

        static void Main(string[] args)
        {
            InitializeSampleFlights();
            
            while (true)
            {
                Console.WriteLine("\nMain Menu:");
                Console.WriteLine("1. View Flight Details");
                Console.WriteLine("2. Book Flight");
                Console.WriteLine("3. Cancel Booking");
                Console.WriteLine("4. View All Bookings");
                Console.WriteLine("5. Admin Menu");
                Console.WriteLine("6. Exit");
                Console.Write("Select option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        GraphClass.ViewFlightDetails(countries);
                        break;
                    case "2":
                        BookFlightMenu();
                        break;
                    case "3":
                        CancelBookingMenu();
                        break;
                    case "4":
                        GraphClass.ViewBookings();
                        break;
                    case "5":
                        AdminMenu();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }
        }

        static void InitializeSampleFlights()
        {
            GraphClass.AddFlight(0, 5, "08:00", "12:00", "4h", 340038, 150);
            GraphClass.AddFlight(0, 9, "14:30", "18:00", "3h30m", 103619, 200);
            GraphClass.AddFlight(5, 3, "09:00", "21:00", "12h", 80000, 100);
        }

        static void BookFlightMenu()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            var flightIds = new LinkedList<string>();
            GraphClass.ViewFlightDetails(countries);
            
            Console.WriteLine("Enter flight IDs to book (separate by comma):");
            string[] ids = Console.ReadLine().Split(',');
            foreach (string id in ids)
            {
                flightIds.Add(id.Trim());
            }
            
            GraphClass.BookFlight(name, flightIds);
        }

        static void CancelBookingMenu()
        {
            GraphClass.ViewBookings();
            Console.Write("Enter Booking ID to cancel: ");
            GraphClass.CancelBooking(Console.ReadLine());
        }

        static void AdminMenu()
        {
            Console.Write("Enter admin password: ");
            if (!GraphClass.AdminLogin(Console.ReadLine()))
            {
                Console.WriteLine("Invalid password!");
                return;
            }

            Console.WriteLine("\nAdmin Menu:");
            Console.WriteLine("1. Add New Flight");
            Console.WriteLine("2. Return to Main Menu");
            
            if (Console.ReadLine() == "1")
            {
                Console.WriteLine("\nCountry Codes:");
                for (int i = 0; i < countries.Length; i++)
                    Console.WriteLine($"{i}. {countries[i]}");

                Console.Write("From Country Code: ");
                int from = int.Parse(Console.ReadLine());
                
                Console.Write("To Country Code: ");
                int to = int.Parse(Console.ReadLine());
                
                Console.Write("Departure Time (HH:mm): ");
                string dep = Console.ReadLine();
                
                Console.Write("Arrival Time (HH:mm): ");
                string arr = Console.ReadLine();
                
                Console.Write("Duration: ");
                string dur = Console.ReadLine();
                
                Console.Write("Price: ");
                int price = int.Parse(Console.ReadLine());
                
                Console.Write("Available Seats: ");
                int seats = int.Parse(Console.ReadLine());

                GraphClass.AddFlight(from, to, dep, arr, dur, price, seats);
                Console.WriteLine("New flight added successfully!");
            }
        }
    }
}