using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomelandSecurity;

namespace Flight_Project
{
    class Menu
    {
        private Flight[] flightList = new Flight[20];
        private int flightCount = 0;

        public Menu()
        {

        }

        public void DisplayBanner() //displays a red banner at the top of the console
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteCenteredLine("~*~ Welcome to Phoenix Air Electronic Passenger Management System ~*~\t");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        //method loads inital flight data
        public void LoadData()
        {
            flightList[0] = new Flight(101, "Phoenix", "Seattle");
            flightCount++;
            flightList[1] = new Flight(102, "Mesa", "Austin");
            flightCount++;
            flightList[2] = new Flight(103, "San Francisco", "Chicago");
            flightCount++;
        }

        public int UserSelection() //handles all menu selections
        {
            Console.Write("Enter your selection: ");
            ConsoleKeyInfo inputKey = Console.ReadKey();
            switch (inputKey.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    return 2;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    return 3;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    return 4;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    return 5;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    return 6;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    return 7;
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                    return 8;
                case ConsoleKey.D9:
                case ConsoleKey.NumPad9:
                    return 9;
                default: //user enters anything other than 1-9 is invalid
                    return 0;
            }
        }

        public void WriteCenteredLine(string inputString) //writes a string in the center of the console
        {
            int centerValue = (Console.WindowWidth + inputString.Length) / 2;
            Console.WriteLine(String.Format("{0," + centerValue + "}", inputString));
        }

        //runs the majority of the program
        public void MainMenu()
        {
            bool appRunning = true;
            LoadData();
            flightList[0].LoadPassengers(flightList);

            do
            {
                DisplayBanner();
                Console.WriteLine("Please Choose from the Following Options\n\t1.) Display a list of flights\n\t2.) Create new flights\n\t3.) Search flights by passenger\n\t4.) Select a flight\n\t5.) Exit");

                switch (UserSelection())
                {
                    case 1:
                        DisplayFlights(flightList);
                        Console.Write("\nPress any key to return to the Main Menu . . .");
                        Console.ReadKey();
                        break;
                    case 2:
                        AddFlight();
                        break;
                    case 3:
                        DisplayBanner();
                        flightList[0].CheckPassenger(flightList, flightCount);
                        break;
                    case 4:
                        DisplayFlights(flightList);
                        Console.WriteLine();
                        int selectedFlight = UserSelection();
                        if (selectedFlight <= flightList.Length)
                            FlightMenu(selectedFlight);
                        else
                        {
                            Console.WriteLine("\n\tInvalid Flight Number Entered. Press Any Key to Continue . . .");
                            Console.ReadKey();
                        }
                        break;
                    case 5:
                        appRunning = false;
                        break;
                }//end switch statement
            }//end do statement
            while (appRunning);
        }//end main method

        //displays a list of flights
        public void DisplayFlights(Flight[] flightList)
        {
            DisplayBanner();
            Console.WriteLine("{0,20}", "Current Flights\n");
            for (int i = 0; i < flightCount; i++)
            {
                Console.WriteLine(String.Format("{0,5}.) {1} {2,15} {3,10} {4,10}", i + 1, flightList[i].FlightNumber, flightList[i].OriginCity, "--->", flightList[i].DestinationCity));
            }
        }

        //flight menu -> is displayed after choosing a flight from the main menu
        public void FlightMenu(int flightNum)
        {
            flightNum = flightNum - 1;
            bool flightMenuRunning = true;

            do
            {
                DisplayBanner();
                Console.WriteLine("Flight {0}: {1} to {2}\n\nPlease Choose from the Following Options:\n\t1.) Edit Existing Flight\n\t2.) Display Passenger Manifest\n\t3.) Add New Passenger to Existing Flight\n\t4.) Submit Manifest for Security Clearance\n\t5.) Exit", flightList[flightNum].FlightNumber, flightList[flightNum].OriginCity, flightList[flightNum].DestinationCity);

                switch (UserSelection())
                {
                    case 1:
                        EditFlight(flightNum);
                        break;
                    case 2:
                        DisplayBanner();
                        flightList[flightNum].DisplayPassengers(flightList, flightNum);
                        break;
                    case 3:
                        DisplayBanner();
                        flightList[flightNum].AddPassenger(flightList, flightNum);
                        break;
                    case 4:
                        DisplayBanner();
                        flightList[flightNum].SecurityCheck(flightList, flightNum);
                        break;
                    case 5:
                        flightMenuRunning = false;
                        break;
                }
            }
            while (flightMenuRunning);
        }
        
        //method adds a flight to the flight list
        public void AddFlight()
        {
            bool addFlightMenuRunning = true;
            int flightNumber = 0;
            string origin = "";
            string destination = "";
            string userInput = "";

            while (addFlightMenuRunning)
            {
                DisplayBanner();
               
                Console.Write("Flight Number: ");
                userInput = Console.ReadLine();
                if (Int32.TryParse(userInput, out flightNumber) == true) //won't let a user enter a non numeric value for a flight number
                {
                    flightNumber = Convert.ToInt32(userInput);
                    if (flightNumber >= 1) //flight number can't be negative
                    {
                        Console.Write("Origin City: ");
                        origin = Console.ReadLine();
                        Console.Write("Destination City: ");
                        destination = Console.ReadLine();
                        Flight tempFlight = new Flight(flightNumber, origin, destination);
                        flightList[flightCount] = tempFlight;
                        flightCount++;
                        Console.Write("Add another flight? (Y/N) ");
                        userInput = Console.ReadLine();
                        switch (userInput.ToLower())
                        {
                            case "y":
                            case "yes":
                                break;
                            case "n":
                            case "no":
                                addFlightMenuRunning = false;
                                break;
                        }//end switch statement
                    }//end inner if statement
                    else
                    {
                        Console.Write("Invalid Number Entered. \n\tPress Any Key to Try Again, or Q to quit . . . ");
                        userInput = Console.ReadLine();
                        switch (userInput.ToLower())
                        {
                            case "q":
                            case "quit":
                                addFlightMenuRunning = false;
                                break;
                            default:
                                break;
                        }//end switch statement
                    }
                }//end outer if statement
                else
                {
                    Console.Write("Invalid Number Entered. \n\tPress Any Key to Try Again, or Q to quit . . . ");
                    userInput = Console.ReadLine();
                    switch(userInput.ToLower())
                    {
                        case "q":
                        case "quit":
                            addFlightMenuRunning = false;
                            break;
                        default:
                            break;
                    }//end switch statement
                }//end else statement

            }//end while loop
        }//end AddFlight method
        
        //method edits a selected flight
        public void EditFlight(int flight)
        {
            DisplayBanner();
            string userInput;
            int newFlightNum;
            Console.Write("Flight Number: ");
            userInput = Console.ReadLine();
            if (Int32.TryParse(userInput, out newFlightNum) == true) //wont let user enter a non numeric value for a flight number
            {
                newFlightNum = Convert.ToInt32(userInput);
                if (newFlightNum >= 1) //flight number cant be negative
                {
                    flightList[flight].FlightNumber = newFlightNum;
                    Console.Write("Origin City: ");
                    flightList[flight].OriginCity = Console.ReadLine();
                    Console.Write("Destination City: ");
                    flightList[flight].DestinationCity = Console.ReadLine();
                }//end inner if statement
                else
                {
                    Console.Write("Invalid Number Entered. \n\tPress Any Key to Continue . . . ");
                    Console.ReadKey();
                }//end else statement
            }//end outer if statement

        }//end EditFlight method

    }//end class block
}//end namespace
