using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace FlightRes
{
    class Program
    {
        private static int[,] mat;
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
                
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("           ╔═══════════════════════════════════════════════════════════════╗");
    Console.WriteLine("           ║                     Airplane Ticket Booking System            ║");
    Console.WriteLine("           ╠═══════════════════════════════════════════════════════════════╣");
    Console.WriteLine("           ║                1. View Flight Details                         ║");
    Console.WriteLine("           ║                2. Book Flight                                 ║");
    Console.WriteLine("           ║                3. Cancel Booking                              ║");
    Console.WriteLine("           ║                4. View All Bookings                           ║");
    Console.WriteLine("           ║                5. Travel Paths                                ║");
    Console.WriteLine("           ║                6. Admin Menu                                  ║");
    Console.WriteLine("           ║                7. Exit                                        ║");
    Console.WriteLine("           ╚═══════════════════════════════════════════════════════════════╝");
    Console.ResetColor();

    Console.Write("Select an option: ");
    switch (Console.ReadLine())
    {
        case "1":
            ViewFlightMenu();
            break;
        case "2":
            BookFlightMenu();
            break;
        case "3":
            CancelBookingMenu();
            break;
        case "4":
            FlightClass.ViewBookings();
            break;
        case "5":
            Graph.DisplayCountries(countries);
            Console.WriteLine("Enter Destination Country: ");
            int end = Graph.SelectCountry(countries, "Enter the destination country number: ");
            int start = 0;
            Graph.FindAllPaths(mat, start, end, countries);
            break;
        case "6":
            AdminMenu();
            break;
        case "7":
            Console.WriteLine("Exiting program...");
            return;
        default:
            Console.WriteLine("Invalid option! Try again.");
            Console.ReadKey();
            break;
    }
            }

        }

        static void ViewFlightMenu()
        {
            Console.WriteLine("\nSort Flights By:");
            Console.WriteLine("1. Flight ID (Default)");
            Console.WriteLine("2. Country Name");
            Console.Write("Select sorting option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    FlightClass.SortWithFlightID();
                    Console.WriteLine("\nFlights sorted by Flight ID:");
                    break;
                case "2":
                    FlightClass.SortWithCountry(countries);
                    Console.WriteLine("\nFlights sorted by Country:");
                    break;
                default:
                    Console.WriteLine("\nShowing default Flight ID order:");
                    break;
            }

            FlightClass.ViewFlightDetails(countries);
        }

        static void InitializeSampleFlights()
        {
            // Short-Haul Flights (1-5 hours)
            FlightClass.AddFlight(8, "06:00", "07:30", 55000, 180);   // Colombo → Mumbai (1.5h)
            FlightClass.AddFlight(11, "08:00", "10:30", 75000, 200);  // Colombo → Singapore (2.5h)
            FlightClass.AddFlight(9, "09:30", "11:00", 65000, 220);   // Colombo → Dubai (1.5h)
            FlightClass.AddFlight(10, "11:00", "15:30", 120000, 160); // Colombo → Moscow (4.5h)
            FlightClass.AddFlight(7, "13:00", "18:30", 95000, 180);   // Colombo → Beijing (5.5h)

            // Medium-Haul Flights (5-8 hours)
            FlightClass.AddFlight(6, "07:30", "13:00", 150000, 200);  // Colombo → Tokyo (5.5h)
            FlightClass.AddFlight(4, "14:00", "19:30", 180000, 170);  // Colombo → Rome (5.5h)
            FlightClass.AddFlight(1, "18:00", "02:30", 220000, 210);  // Colombo → Sydney (8.5h)
            FlightClass.AddFlight(5, "20:00", "04:30", 190000, 240);  // Colombo → London (8.5h)
            FlightClass.AddFlight(2, "22:30", "06:00", 210000, 190);  // Colombo → Toronto (7.5h)

            // Long-Haul Flights (8+ hours)
            FlightClass.AddFlight(3, "00:30", "14:00", 350000, 180);  // Colombo → New York (13.5h)
            FlightClass.AddFlight(12, "05:00", "08:30", 85000, 200);  // Colombo → Doha (3.5h)
            FlightClass.AddFlight(8, "10:30", "12:00", 60000, 220);   // Colombo → Delhi (1.5h)
            FlightClass.AddFlight(11, "12:00", "15:30", 80000, 210);  // Colombo → Kuala Lumpur (3.5h)
            FlightClass.AddFlight(9, "16:00", "18:30", 70000, 230);   // Colombo → Abu Dhabi (2.5h)

            // Additional Flights
            FlightClass.AddFlight(5, "09:00", "17:30", 280000, 200);  // Colombo → Manchester (8.5h)
            FlightClass.AddFlight(3, "13:30", "03:00", 380000, 180);  // Colombo → Los Angeles (13.5h)
            FlightClass.AddFlight(1, "21:00", "06:30", 240000, 210);  // Colombo → Melbourne (9.5h)
            FlightClass.AddFlight(2, "08:30", "18:00", 230000, 190);  // Colombo → Vancouver (9.5h)
            FlightClass.AddFlight(4, "17:00", "22:30", 170000, 200);  // Colombo → Milan (5.5h)

            // More Variations
            FlightClass.AddFlight(6, "04:30", "10:00", 160000, 180);  // Colombo → Osaka (5.5h)
            FlightClass.AddFlight(7, "15:00", "20:30", 110000, 220);  // Colombo → Shanghai (5.5h)
            FlightClass.AddFlight(10, "19:30", "00:30", 130000, 200); // Colombo → St Petersburg (5h)
            FlightClass.AddFlight(12, "07:00", "09:30", 82000, 240);  // Colombo → Bahrain (2.5h)
            FlightClass.AddFlight(9, "14:30", "17:00", 68000, 210);   // Colombo → Muscat (2.5h)

            // Additional Routes
            FlightClass.AddFlight(5, "23:00", "07:30", 195000, 220);  // Colombo → Edinburgh (8.5h)
            FlightClass.AddFlight(3, "06:30", "20:00", 360000, 190);  // Colombo → Chicago (13.5h)
            FlightClass.AddFlight(1, "12:30", "22:00", 225000, 210);  // Colombo → Brisbane (9.5h)
            FlightClass.AddFlight(2, "18:30", "02:00", 215000, 200);  // Colombo → Montreal (7.5h)
            FlightClass.AddFlight(4, "10:00", "15:30", 165000, 230);  // Colombo → Venice (5.5h)

            // More Options
            FlightClass.AddFlight(11, "05:30", "09:00", 78000, 240);  // Colombo → Jakarta (3.5h)
            FlightClass.AddFlight(8, "16:30", "18:00", 58000, 220);   // Colombo → Chennai (1.5h)
            FlightClass.AddFlight(7, "20:00", "01:30", 125000, 210);  // Colombo → Guangzhou (5.5h)
            FlightClass.AddFlight(10, "13:00", "17:30", 135000, 190); // Colombo → Novosibirsk (4.5h)
            FlightClass.AddFlight(12, "09:30", "12:00", 88000, 230);  // Colombo → Kuwait City (2.5h)

            // Final Set
            FlightClass.AddFlight(5, "11:30", "20:00", 205000, 240);  // Colombo → Glasgow (8.5h)
            FlightClass.AddFlight(3, "17:30", "07:00", 370000, 210);  // Colombo → Dallas (13.5h)
            FlightClass.AddFlight(1, "03:00", "12:30", 235000, 220);  // Colombo → Perth (9.5h)
            FlightClass.AddFlight(2, "19:00", "02:30", 225000, 200);  // Colombo → Calgary (7.5h)
            FlightClass.AddFlight(4, "21:30", "03:00", 175000, 230);  // Colombo → Naples (5.5h)

            // Last Batch
            FlightClass.AddFlight(6, "02:00", "07:30", 155000, 210);  // Colombo → Fukuoka (5.5h)
            FlightClass.AddFlight(7, "16:00", "21:30", 115000, 240);  // Colombo → Shenzhen (5.5h)
            FlightClass.AddFlight(10, "22:30", "03:30", 140000, 190); // Colombo → Yekaterinburg (5h)
            FlightClass.AddFlight(12, "10:00", "12:30", 90000, 220);  // Colombo → Riyadh (2.5h)
            FlightClass.AddFlight(9, "15:30", "18:00", 72000, 210);   // Colombo → Dammam (2.5h)

            int v = 13;
            mat = new int[v, v];
            Graph.AddEdge(mat, 0, 1, 221000);
            Graph.AddEdge(mat, 0, 2, 431771);
            Graph.AddEdge(mat, 0, 4, 300000);
            Graph.AddEdge(mat, 0, 5, 340038);
            Graph.AddEdge(mat, 0, 6, 235145);
            Graph.AddEdge(mat, 0, 7, 140838);
            Graph.AddEdge(mat, 0, 8, 49199);
            Graph.AddEdge(mat, 0, 9, 103619);
            Graph.AddEdge(mat, 0, 10, 285000);
            Graph.AddEdge(mat, 0, 11, 107258);
            Graph.AddEdge(mat, 0, 12, 173037);

            // USA Flights
            Graph.AddEdge(mat, 5, 3, 80000);
            Graph.AddEdge(mat, 9, 3, 250000);
            Graph.AddEdge(mat, 12, 3, 240000);

            // TO Australia
            Graph.AddEdge(mat, 11, 1, 100000);

            // To England
            Graph.AddEdge(mat, 9, 5, 230000);

            Graph.AddEdge(mat, 11, 6, 120000);
            Graph.AddEdge(mat, 8, 7, 80000);
            Graph.AddEdge(mat, 11, 7, 300000);
            Graph.AddEdge(mat, 12, 4, 127000);
            Graph.AddEdge(mat, 5, 10, 50000);
        }

        static void BookFlightMenu()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();

            string date = "";
            while (true)
            {
                Console.Write("Enter travel date (DD/MM/YYYY): ");
                date = Console.ReadLine();
                if (DateTime.TryParseExact(date, "dd/MM/yyyy", null, DateTimeStyles.None, out _))
                    break;
                Console.WriteLine("Invalid date format! Please use DD/MM/YYYY.");
            }

            // Display countries for selection
            Console.WriteLine("\nSelect Destination Country:");
            for (int i = 0; i < countries.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {countries[i]}");
            }
            Console.Write("Enter country number: ");

            int countryIndex;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out countryIndex) &&
                    countryIndex > 0 && countryIndex <= countries.Length)
                {
                    countryIndex--; // Convert to zero-based index
                    break;
                }
                Console.WriteLine("Invalid country number. Please try again.");
            }

            // Show flights only for the selected country
            Console.WriteLine($"\nAvailable Flights to {countries[countryIndex]}:");
            var availableFlights = FlightClass.GetFlightsByCountry(countryIndex);
            if (availableFlights.Count == 0)
            {
                Console.WriteLine("No flights available to this destination.");
                return;
            }

            Console.WriteLine("{0,-10} {1,-15} {2,-10} {3,-10} {4,-15} {5,-10}",
                             "Flight ID", "Departure", "Arrival", "Duration", "Price", "Seats");

            foreach (var flight in availableFlights)
            {
                Console.WriteLine("{0,-10} {1,-15} {2,-10} {3,-10} {4,-15} {5,-10}",
                                 flight.FlightID,
                                 flight.DepartureTime,
                                 flight.ArrivalTime,
                                 flight.Duration,
                                 flight.Price,
                                 flight.AvailableSeats);
            }

            // Book flights
            var flightIds = new LinkedList<string>();
            Console.WriteLine("\nEnter flight IDs to book (separate by comma):");
            string[] ids = Console.ReadLine().Split(',');
            foreach (string id in ids)
            {
                flightIds.Add(id.Trim());
            }

            FlightClass.BookFlight(name, flightIds, date, countries);
        }

        static void CancelBookingMenu()
        {
            FlightClass.ViewBookings();
            Console.Write("Enter Booking ID to cancel: ");
            FlightClass.CancelBooking(Console.ReadLine());
        }

        static void AdminMenu()
        {
            Console.Write("Enter admin password: ");
            if (!FlightClass.AdminLogin(Console.ReadLine()))
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
                for (int i = 1; i < countries.Length; i++)
                    Console.WriteLine($"{i}. {countries[i]}");

                Console.Write("To Country Code: ");
                int to = int.Parse(Console.ReadLine());

                string depTime = "";
                string arrTime = "";

                while (true)
                {
                    Console.Write("Departure Time (HH:mm): ");
                    depTime = Console.ReadLine();
                    if (DateTime.TryParseExact(depTime, "HH:mm", null, DateTimeStyles.None, out _))
                        break;
                    Console.WriteLine("Invalid time format! Use 24h format (HH:mm).");
                }

                while (true)
                {
                    Console.Write("Arrival Time (HH:mm): ");
                    arrTime = Console.ReadLine();
                    if (DateTime.TryParseExact(arrTime, "HH:mm", null, DateTimeStyles.None, out _))
                        break;
                    Console.WriteLine("Invalid time format! Use 24h format (HH:mm).");
                }

                Console.Write("Price: ");
                int price = int.Parse(Console.ReadLine());

                Console.Write("Available Seats: ");
                int seats = int.Parse(Console.ReadLine());

                FlightClass.AddFlight(to, depTime, arrTime, price, seats);
                Console.WriteLine("New flight added successfully!");
            }
        }
    }
}