using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomelandSecurity;

namespace Flight_Project
{
    class Flight
    {
        //instance variables
        private int flightNum;
        private string originCity;
        private string destinationCity;
        private Passenger[] passengerManifest = new Passenger[20];
        private int passengerCount;
        private TSA checkTSA = new TSA();

        //properties
        public int FlightNumber
        {
            get { return flightNum; }
            set { flightNum = value; }
        }
        public string OriginCity
        {
            get { return originCity; }
            set { originCity = value; }
        }
        public string DestinationCity
        {
            get { return destinationCity; }
            set { destinationCity = value; }
        }
        public int PassengerCount
        {
            get { return passengerCount; }
            set { passengerCount = value; }
        }
        public Passenger[] PassengerManifest
        {
            get { return passengerManifest; }
            set { passengerManifest = value; }
        }

        //default constructor
        public Flight()
        {
            
        }

        //overloaded constructor
        public Flight(int flightNum, string originCity, string destinationCity)
        {
            this.flightNum = flightNum;
            this.originCity = originCity;
            this.destinationCity = destinationCity;
        }

        //loads initial passenger data
        public void LoadPassengers(Flight[] flightList)
        {
            flightList[0].PassengerManifest[0] = new Passenger("Kelly", "Condon", "218");
            flightList[0].PassengerCount++;
            flightList[0].PassengerManifest[1] = new Passenger("Tylor", "Cammarato", "314");
            flightList[0].PassengerCount++;
            flightList[0].PassengerManifest[2] = new Passenger("Alyssa", "Rizzo", "827");
            flightList[0].PassengerCount++;

            flightList[1].PassengerManifest[0] = new Passenger("Jennifer", "Chavez", "489");
            flightList[1].PassengerCount++;
            flightList[1].PassengerManifest[1] = new Passenger("John", "Quinn", "198");
            flightList[1].PassengerCount++;
            flightList[1].PassengerManifest[2] = new Passenger("Kristine", "Quinn", "199");
            flightList[1].PassengerCount++;

            flightList[2].PassengerManifest[0] = new Passenger("Justin", "McCoy", "917");
            flightList[2].PassengerCount++;
            flightList[2].PassengerManifest[1] = new Passenger("Ray", "Ruanto", "643");
            flightList[2].PassengerCount++;
            flightList[2].PassengerManifest[2] = new Passenger("Donald", "Marovich", "042");
            flightList[2].PassengerCount++;
        }

        //method displays the passenger manifest for selected flight
        public void DisplayPassengers(Flight[] flightList, int flight)
        {
            Console.WriteLine("Passenger Manifest for Flight {0} - {1} to {2}\n", flightList[flight].FlightNumber, flightList[flight].OriginCity, flightList[flight].DestinationCity);
            for (int i = 0; i < flightList[flight].PassengerCount; i++)
            {
                if (flightList[flight].PassengerManifest[i].SecurityFlag == true)
                    Console.WriteLine("{0} {1}\t\tLoyalty #: {2}\t\tFLAGGED", flightList[flight].PassengerManifest[i].FirstName, flightList[flight].PassengerManifest[i].LastName, flightList[flight].PassengerManifest[i].LoyaltyNumber);
                else
                    Console.WriteLine("{0} {1}\t\tLoyalty #: {2}", flightList[flight].PassengerManifest[i].FirstName, flightList[flight].PassengerManifest[i].LastName, flightList[flight].PassengerManifest[i].LoyaltyNumber);
            }

            Console.Write("\nPress any key to continue . . . ");
            Console.ReadKey();
        }//end DisplayPassengers method

        //method adds passengers to the selected flight
        public void AddPassenger(Flight[] flightList, int flight)
        {
            bool addPassengerMethodRunning = true;

            while (addPassengerMethodRunning)
            {
                string newFirstName;
                string newLastName;
                string newLoyaltyNum;
                Console.Write("First Name: ");
                newFirstName = Console.ReadLine();
                Console.Write("Last Name: ");
                newLastName = Console.ReadLine();
                Console.Write("Loyalty #: ");
                newLoyaltyNum = Console.ReadLine();

                flightList[flight].PassengerManifest[flightList[flight].PassengerCount] = new Passenger(newFirstName, newLastName, newLoyaltyNum);
                flightList[flight].PassengerCount++;

                Console.Write("\nAdd another passenger? (Y/N) ");
                switch (Console.ReadLine().ToLower())
                {
                    case "y":
                    case "yes":
                        break;
                    case "n":
                    case "no":
                    default:
                        addPassengerMethodRunning = false;
                        break;
                }//end switch statement
            }//end while statement
        }//end AddPassenger method

        //method checks each flight for a passenger
        public void CheckPassenger(Flight[] flightList, int flightCount)
        {
            bool foundPassenger = false;
            Console.Write("First Name: ");
            string fName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lName = Console.ReadLine();
            for (int i = 0; i < flightCount; i++)
            {
                for (int j = 0; j < flightList[i].PassengerCount; j++)
                {
                    if (flightList[i].PassengerManifest[j].FirstName == fName && flightList[i].PassengerManifest[j].LastName == lName)
                    {
                        Console.WriteLine("{0} {1} is on Flight {2} - {3} to {4}", fName, lName, flightList[i].FlightNumber, flightList[i].OriginCity, flightList[i].DestinationCity);
                        foundPassenger = true;
                        break;
                    }//end if statement
                }//end inner for loop
            }//end outer for loop
            if (foundPassenger == false)
                Console.WriteLine("Sorry, no passenger by that name seems to booked.");
            Console.Write("\nPress Any Key to Continue . . . ");
            Console.ReadLine();
        }//end method CheckPassenger

        //performs NSA security check
        public void SecurityCheck(Flight[] flightList, int flight)
        {
            Flight tempFlight = flightList[flight];
            Passenger[] tempPassengers = tempFlight.PassengerManifest;

            for (int i = 0; i < tempFlight.PassengerCount; i++)
            {
                tempFlight.PassengerManifest[i].SecurityFlag = false;
            }//end for loop

            FlaggedPassenger[] flaggedPassengers = checkTSA.RunSecurityCheck(tempFlight.PassengerManifest, tempFlight.PassengerCount);


            for (int i = 0; i < flaggedPassengers.Length; i++)
            {
                for (int j = 0; j < tempFlight.PassengerCount; j++)
                {
                    //checks the flagged passenger list against the passenger manifest
                    if (flaggedPassengers[i].FirstName ==
                        tempPassengers[j].FirstName
                        &&
                        flaggedPassengers[i].LastName ==
                        tempPassengers[j].LastName
                        )
                    {
                        tempFlight.PassengerManifest[j].SecurityFlag = true;
                    }//end if statement
                }//end inner for loop
            }//end outer for loop

            Console.Write("Security Check Successful.\n\nPress Any Key to Continue . . . ");
            Console.ReadKey();
        }//end SecurityCheck method
    }
}
