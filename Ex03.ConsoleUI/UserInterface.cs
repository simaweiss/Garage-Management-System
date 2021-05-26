using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Vehicle;

namespace Ex03.ConsoleUI
{
    public class UserInterface
    {
        private const string k_menu = @"Welcome To Sima and David Garage:
            Choose between 1-8:
            1) Add Vehicle To garage.
            2) Show all license in the garage with filtering.
            3) Change vehicle status.
            4) Infalte vehicles wheel to MAX.
            5) Reful a vehicle by gas.
            6) Charge Electic vehicle.
            7) Show all Details by license nubmer.
            8) Quit.";

        private static bool s_Exit = false;
        private readonly Garage r_Garage; 

        public UserInterface()
        {
            r_Garage = new Garage();
        }

        private enum eMenuOption
        {
            AddVehicle,
            ShowLicenseNudmbers,
            ChangeVehicleStatus,
            InfalteVehiclesToMax,
            RefulGasVehicle,
            RefulElectricVehicle,
            ShowVehicleDetails,
            QuitProgram
        }

        private static void exitProgram()
        {
            Console.WriteLine("Bye Bye");
            s_Exit = true;
        }

        public void Run()
        {
            eMenuOption menuOptions;
            string userInputString;
            int userInput;
            int milliseconds = 5000;
            while (!s_Exit)
            {
                try
                {
                    showMenu();
                    userInputString = Console.ReadLine();
                    userInput = parseSelection(userInputString, Enum.GetNames(typeof(eMenuOption)).Length);
                    menuOptions = (eMenuOption)Enum.GetValues(typeof(eMenuOption)).GetValue(userInput - 1);
                    callUserMethodByOptions(menuOptions);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Wrong Input");
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Value out of range - input must be between {0} to {1}", ex.MinValue, ex.MaxValue);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Wrong Input");
                }

                Console.WriteLine("Please wait 5 seconds...");
                Thread.Sleep(milliseconds);
                Console.Clear();
            }
        }

        private void showMenu()
        {
            Console.WriteLine(k_menu);
        }

        private int parseSelection(string i_UserInput, int i_MenuOption)
        {
            int userInput;
            if (!int.TryParse(i_UserInput, out userInput))
            {
                throw new FormatException();
            }

            if (userInput < 1 || userInput > i_MenuOption)
            {
                throw new ValueOutOfRangeException(null, 1, i_MenuOption);
            }

            return userInput;
        }

        private void callUserMethodByOptions(eMenuOption i_MenuOptions)
        {
            switch (i_MenuOptions)
            {
                case eMenuOption.AddVehicle:
                    addVehicleToGarage();
                    break;
                case eMenuOption.ShowLicenseNudmbers:
                    displayLicensesNumbersWithFilter();
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOption.InfalteVehiclesToMax:
                    inflateAirVehicleWheelsToMax();
                    break;
                case eMenuOption.RefulGasVehicle:
                    refuelFuelVehicle();
                    break;
                case eMenuOption.RefulElectricVehicle:
                    chargeElectricVehicle();
                    break;
                case eMenuOption.ShowVehicleDetails:
                    fullDataForOnwerAndVehicle();
                    break;
                case eMenuOption.QuitProgram:
                    exitProgram();
                    break;
                default:
                    break;
            }
        }
       
        private void addVehicleToGarage()
        { 
            string licenceNum;
            List<string> userParameters = new List<string>();

            Console.WriteLine("Insert the licence number");
            licenceNum = Console.ReadLine();
            if (r_Garage.IsAlreadyInGarage(licenceNum))
            {
                Console.WriteLine("The car is already in the garage ; status was changed to in progress.");
            }
            else
            {
                createNewVehicleOwner(licenceNum);
            }
        }

        
        private void createNewVehicleOwner(string i_LicenceNum)
        {
            Vehicle newVehicle = createNewVehicle(i_LicenceNum, out eTypeOfVehicle curretVehicle);
            fillAndAddVehicleOwnerToTheGarage(newVehicle);
            vehiclePropertise(newVehicle);

            switch (curretVehicle)
            {
                case eTypeOfVehicle.FuelCar:
                case eTypeOfVehicle.ElectricCar:
                    carProperties(newVehicle);
                    break;
                case eTypeOfVehicle.FuelMotorCycle:
                case eTypeOfVehicle.ElectricMotorCycle:
                    motorcycleProperties(newVehicle);
                    break;
                case eTypeOfVehicle.FuelTruck:
                    truckProperties(newVehicle);
                    break;
            }
        }

        private Vehicle createNewVehicle(string i_LicenceNum, out eTypeOfVehicle o_CurretVehicle)
        {
            string modelName;
            Console.WriteLine(EnumChoises(typeof(eTypeOfVehicle)));
            Console.WriteLine("Insert your number of choice then press 'enter'");
            getEnumChoise(typeof(eTypeOfVehicle), out eTypeOfVehicle currentChoise);
            Console.WriteLine("Insert model for the {0}", currentChoise);
            modelName = Console.ReadLine();
            Vehicle newVehicle = VehicleCreator.CreateVehicle(currentChoise, i_LicenceNum, modelName);
            o_CurretVehicle = currentChoise;
            return newVehicle;
        }

        public string EnumChoises(Type i_Type)
        {
            string[] allEnumTypes = Enum.GetNames(i_Type);
            StringBuilder enumChoise = new StringBuilder(100);
            byte idx = 0;
            foreach (string vehicleType in allEnumTypes)
            {
                enumChoise.AppendFormat("{0}. {1}{2}", idx++, vehicleType, Environment.NewLine);
            }

            return enumChoise.ToString();
        }

        private void getEnumChoise<T>(Type i_Type, out T o_Choise) where T : struct
        {
            T userChoise;
            while (!(Enum.TryParse(Console.ReadLine(), out userChoise) && Enum.IsDefined(i_Type, userChoise)))
            {
                Console.WriteLine("Wrong input, try again.");
            }

            o_Choise = userChoise;
        }

        private void fillAndAddVehicleOwnerToTheGarage(Vehicle i_NewVehicle)
        {
            string VehicleOwnerName, VehicleOwnerPhoneNumber;
            Console.WriteLine("Insert your name");
            VehicleOwnerName = Console.ReadLine();
            Console.WriteLine("Insert your phone number");
            VehicleOwnerPhoneNumber = Console.ReadLine();
            r_Garage.InsertNewVehicleOwner(i_NewVehicle, VehicleOwnerName, VehicleOwnerPhoneNumber);
        }

        private void vehiclePropertise(Vehicle i_Vehicle)
        {
            wheelsProperty(i_Vehicle);
            setEnergyInVehicle(i_Vehicle);
        }

        private void wheelsProperty(Vehicle i_Vehicle)
        {
            bool userInsertedLegalPresser = false;
            string manufacturerNameOfWheels;
            float airPressure = 0;
            Console.WriteLine("Insert name of manufacturer of wheels");
            manufacturerNameOfWheels = Console.ReadLine();
            Console.WriteLine("Insert the air pressure in the wheels");

            while (!userInsertedLegalPresser)
            {
                if (float.TryParse(Console.ReadLine(), out airPressure))
                {
                    try
                    {
                        VehicleCreator.VehicleWheelsProperty(i_Vehicle, manufacturerNameOfWheels, airPressure);
                        userInsertedLegalPresser = true;
                    }
                    catch (ValueOutOfRangeException voore)
                    {
                        Console.WriteLine(voore.Message);
                    }
                }
                else
                {
                    Console.WriteLine("illegal input, try again");
                }
            }
        }

        private void setEnergyInVehicle(Vehicle i_Vehicle)
        {
            float currentPercentOfEnergy;
            Console.WriteLine("Insert current percent energy in your vehicle");
            bool VehicleOwnerInsertLegalPercent = false;
            while (!VehicleOwnerInsertLegalPercent)
            {
                if (float.TryParse(Console.ReadLine(), out currentPercentOfEnergy))
                {
                    try
                    {
                        VehicleCreator.VehicleEnergyProperty(i_Vehicle, currentPercentOfEnergy);
                        VehicleOwnerInsertLegalPercent = true;
                    }
                    catch (ValueOutOfRangeException)
                    {
                        Console.WriteLine("Insert percent between 0 to 100");
                    }
                }
                else
                {
                    Console.WriteLine("it ilegal input try again");
                }
            }
        }
            
        private void carProperties(Vehicle i_Car)
        {
            Console.WriteLine("Please insert the number of doors, in range 2 - 5");
            bool tryToParse = byte.TryParse(Console.ReadLine(), out byte doorsNumber);

            try
            {
                if (!tryToParse)
                {
                    throw new FormatException();
                }
                else if (doorsNumber < 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    Console.WriteLine("Now insert your car color");
                    Console.WriteLine("Insert your number of choice then press 'enter'");
                    Console.WriteLine(EnumChoises(typeof(Car.eCarColor)));
                    getEnumChoise(typeof(Car.eCarColor), out Car.eCarColor chosenColor);
                    VehicleCreator.CarProperties(i_Car, doorsNumber, chosenColor);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong Input, try again");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Logically wrong input, try again");
            }
            catch (ValueOutOfRangeException voore)
            {
                Console.WriteLine(voore.Message);
            }
        }

        private void motorcycleProperties(Vehicle i_Motorcycle)
        {
            Console.WriteLine("Insert please Engine Capacity and then press 'enter'");
            bool tryToParse = int.TryParse(Console.ReadLine(), out int engineCapacity);
            try
            {
                if (!tryToParse)
                {
                    throw new FormatException();
                }
                else if (engineCapacity < 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    Console.WriteLine("Choose type of license");
                    Console.WriteLine(EnumChoises(typeof(Motorcycle.eTypeOfLicense)));
                    getEnumChoise(typeof(Motorcycle.eTypeOfLicense), out Motorcycle.eTypeOfLicense typeOfLicense);
                    VehicleCreator.MotorcyleProperties(i_Motorcycle, engineCapacity, typeOfLicense);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong Input, try again");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Logically wrong input, try again");
            }       
        }

        private void truckProperties(Vehicle i_Truck)
        {
            Console.WriteLine("Insert please Trunk Capacity then press 'enter'");
            bool tryToParse = float.TryParse(Console.ReadLine(), out float trunkCapacity);
            try
            {
                if (!tryToParse)
                {
                    throw new FormatException();
                }
                else if (trunkCapacity < 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    Console.WriteLine("Insert 'true' if the truck contains hazradous materials, else - insert 'false'.");
                    if (!bool.TryParse(Console.ReadLine(), out bool isContainHazard))
                    {
                        isContainHazard = false; // if neither 'true' or 'false' were inserted, we set it as false.
                    }
 
                    VehicleCreator.TruckProperties(i_Truck, trunkCapacity, isContainHazard);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Wrong Input, try again");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Logically wrong input, try again");
            }
        }

        private void refuelFuelVehicle()
        {
            string licenseNumber;
            float fuelAmount;
            Console.WriteLine("Insert license number");
            licenseNumber = Console.ReadLine();
            Console.WriteLine("Insert the amount of fuel to add");
            while (!float.TryParse(Console.ReadLine(), out fuelAmount) || (fuelAmount <= 0))
            {
                Console.WriteLine("wrong input, try again");
            }

            Console.WriteLine(EnumChoises(typeof(eFuelType)));
            getEnumChoise(typeof(eFuelType), out eFuelType chosenTypeFuel);
            try
            {
                r_Garage.RefuelVehicle(licenseNumber, fuelAmount, chosenTypeFuel);
            }
            catch (ValueOutOfRangeException voore)
            {
                Console.WriteLine(voore.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void inflateAirVehicleWheelsToMax()
        {
            Console.WriteLine("Insert license number");
            try
            {
                r_Garage.FillMaxWheelsAir(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void chargeElectricVehicle()
        {
            string licenseNumber;
            float chargingHoursAmount;
            Console.WriteLine("Insert license number");
            licenseNumber = Console.ReadLine();
            Console.WriteLine("Insert amount of hours charging you want to add");
            while (!float.TryParse(Console.ReadLine(), out chargingHoursAmount))
            {
                Console.WriteLine("wrong input, try again");
            }

            try
            {
                r_Garage.ChargeBattary(licenseNumber, chargingHoursAmount);
            }
            catch (ValueOutOfRangeException voore)
            {
                Console.WriteLine(voore.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber;

            Console.WriteLine("Insert license number");
            licenseNumber = Console.ReadLine();
            Console.WriteLine(EnumChoises(typeof(eStatusInGarage)));
            getEnumChoise(typeof(eStatusInGarage), out eStatusInGarage chosenStatus);
            try
            {
                r_Garage.ChangeVehicleStatus(licenseNumber, chosenStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void displayLicensesNumbersWithFilter()
        {
            Console.WriteLine("Insert true (any way) if you want order by status , any other input without status");
            if (!bool.TryParse(Console.ReadLine(), out bool userWantOrderByStatus))
            {
                userWantOrderByStatus = false;
            }

            Console.WriteLine(r_Garage.MsgOfAllVehicleInGarageFillterStatus(userWantOrderByStatus));
        }

        private void fullDataForOnwerAndVehicle()
        {
            Console.WriteLine("Insert license number");
            try
            {
                Console.WriteLine(r_Garage.MsgFullDetailsVehicle(Console.ReadLine()));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}