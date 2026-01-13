// See https://aka.ms/new-console-template for more information
// Online C# Editor for free
// Write, Edit and Run your C# code using C# Online Compiler

// using System;

// Project Structure
// ToolVehicleTaxSystem
// │
// ├── ToolVehicle.cs
// ├── Car.cs
// ├── Bike.cs
// ├── HeavyVehicle.cs
// └── Program.cs

using System;

namespace ToolVehicleTaxSystem
{
    // ================= BASE CLASS =================
    abstract class ToolVehicle
    {
        public int VehicleID { get; set; }
        public string VehicleType { get; set; }

        public static int TotalVehicles { get; private set; }
        public static int TotalTaxPayingVehicles { get; private set; }
        public static int TotalNonTaxPayingVehicles { get; private set; }
        public static decimal TotalTaxCollected { get; private set; }

        protected ToolVehicle(int id, string type)
        {
            VehicleID = id;
            VehicleType = type;
            TotalVehicles++;
        }

        public abstract decimal GetTaxAmount();

        public void PayTax()
        {
            decimal tax = GetTaxAmount();
            TotalTaxCollected += tax;
            TotalTaxPayingVehicles++;
            Console.WriteLine($"Tax paid successfully: ${tax}");
        }

        public void PassWithoutPaying()
        {
            TotalNonTaxPayingVehicles++;
            Console.WriteLine("Vehicle passed without paying tax.");
        }
    }

    // ================= BIKE CLASS =================
    class Bike : ToolVehicle
    {
        public Bike(int id) : base(id, "Bike") { }

        public override decimal GetTaxAmount()
        {
            return 1m;
        }
    }

    // ================= CAR CLASS =================
    class Car : ToolVehicle
    {
        public Car(int id) : base(id, "Car") { }

        public override decimal GetTaxAmount()
        {
            return 2m;
        }
    }

    // ================= HEAVY VEHICLE CLASS =================
    class HeavyVehicle : ToolVehicle
    {
        public HeavyVehicle(int id) : base(id, "Heavy Vehicle") { }

        public override decimal GetTaxAmount()
        {
            return 4m;
        }
    }

    // ================= MAIN PROGRAM =================
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Tool Vehicle Tax Management System ===\n");

            char continueChoice = 'Y';
            int vehicleCounter = 1;

            do
            {
                Console.WriteLine("Select Vehicle Type:");
                Console.WriteLine("1. Bike");
                Console.WriteLine("2. Car");
                Console.WriteLine("3. Heavy Vehicle");
                Console.Write("Enter your choice: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input.\n");
                    continue;
                }

                ToolVehicle vehicle = null;

                switch (choice)
                {
                    case 1:
                        vehicle = new Bike(vehicleCounter++);
                        break;
                    case 2:
                        vehicle = new Car(vehicleCounter++);
                        break;
                    case 3:
                        vehicle = new HeavyVehicle(vehicleCounter++);
                        break;
                    default:
                        Console.WriteLine("Invalid vehicle type.\n");
                        continue;
                }

                decimal tax = vehicle.GetTaxAmount();
                Console.WriteLine($"\nTax for {vehicle.VehicleType}: ${tax}");
                Console.Write("Do you want to pay tax? (Y/N): ");

                char payChoice = char.ToUpper(Console.ReadLine()[0]);

                if (payChoice == 'Y')
                {
                    vehicle.PayTax();
                }
                else
                {
                    vehicle.PassWithoutPaying();
                }

                Console.Write("\nDo you want to enter another vehicle? (Y/N): ");
                continueChoice = char.ToUpper(Console.ReadLine()[0]);
                Console.WriteLine();

            } while (continueChoice == 'Y');

            Console.WriteLine("\n--- Final Report ---");
            Console.WriteLine($"Total Vehicles: {ToolVehicle.TotalVehicles}");
            Console.WriteLine($"Tax Paying Vehicles: {ToolVehicle.TotalTaxPayingVehicles}");
            Console.WriteLine($"Non-Tax Paying Vehicles: {ToolVehicle.TotalNonTaxPayingVehicles}");
            Console.WriteLine($"Total Tax Collected: ${ToolVehicle.TotalTaxCollected}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

