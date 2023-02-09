using System;
using System.Collections.Generic;
using GarageManagerLibrary;

namespace GarageLogic
{
    public class InputHandler
    {
        protected const string k_YesInput = "Y";
        protected const string k_NoInput = "N";
        protected const int k_MinimumOption = 1;
        
        public (int, List<string>) GetVehicleInformation(List<string> i_VehicleTypes)
        {
            Console.WriteLine("Hello! What type of vehicle do you own?");
            printList(i_VehicleTypes);
            int vehicleType = convertStringToIntInRange(k_MinimumOption, i_VehicleTypes.Count);
            List<string> vehicleProperties = new List<string>
            {
                getVehicleModelName(),
                getVehicleLicenseNumber(),
                getCapacity("wheels current air pressure"),
                getWheelsManufacturer()
            };

            getVehicleProperties(vehicleType, vehicleProperties);

            return (vehicleType, vehicleProperties);
        }

        private void getVehicleProperties(int i_Option, List<string> i_PropertiesList)
        {
            switch(i_Option)
            {
                case 1:
                    goto case 2;
                case 2:
                    getCarProperties(i_PropertiesList);
                    break;
                case 3:
                    goto case 4;
                case 4:
                    getMotorcycleProperties(i_PropertiesList);
                    break;
                case 5:
                    getTruckProperties(i_PropertiesList);
                    break;
            }
        }

        private string getWheelsManufacturer()
        {
            Console.WriteLine("Please enter the wheels manufacturer:");
            return Console.ReadLine();
        }

        public int GetEnumIntValue<T>()
        {
            printEnumFields<T>();
            return convertStringToIntInRange(k_MinimumOption, Enum.GetNames(typeof(T)).Length);
        }
        
        private void getCarProperties(List<string> i_PropertiesList)
        {
            i_PropertiesList.Add(GetEnumIntValue<eCarColors>().ToString());
            i_PropertiesList.Add(GetEnumIntValue<eAmountOfDoors>().ToString());
        }

        private void getMotorcycleProperties(List<string> i_PropertiesList)
        {
            i_PropertiesList.Add(GetEnumIntValue<eMotorcycleLicenseType>().ToString());
            i_PropertiesList.Add(getCapacity("motorcycle engine"));
        }

        private void getTruckProperties(List<string> i_PropertiesList)
        {
            i_PropertiesList.Add(isTruckHasFreezer());
            i_PropertiesList.Add(getCapacity("truck trunk"));
        }

        public (string, string) GetOwnerInformation()
        {
            string ownerName = string.Empty;
            string phoneNumber = string.Empty;
            Console.WriteLine("Please enter owner's name:");
            ownerName = Console.ReadLine();
            Console.WriteLine("Please enter the owner's phone number:");
            phoneNumber = Console.ReadLine();

            if(!int.TryParse(phoneNumber, out int phoneNumberNumericValue))
            {
                throw new FormatException($"This input \"{phoneNumber}\" is not a valid phone number, use only digits");
            }

            return (ownerName, phoneNumber);
        }

        private void printList<T>(List<T> i_List)
        {
            foreach(T item in i_List)
            {
                Console.WriteLine(item);
            }
        }

        private string getCapacity(string i_OutputMessage)
        {
            Console.WriteLine($"Please enter the {i_OutputMessage} capacity:");
            string capacityInput = Console.ReadLine();
            float capacityFloatValue;

            if(!float.TryParse(capacityInput, out capacityFloatValue))
            {
                throw new FormatException($"This is not a numeric value for the {i_OutputMessage} capacity");
            }

            return capacityFloatValue.ToString();
        }

        private string isTruckHasFreezer()
        {
            Console.WriteLine("Does the truck have freezer? <Y/N>");
            string freezerInput = Console.ReadLine();

            if (freezerInput != k_YesInput && freezerInput != k_NoInput)
            {
                throw new FormatException($"This input \"{freezerInput}\" is not a valid input");
            }

            return freezerInput;
        }

        private string getVehicleModelName()
        {
            Console.WriteLine("Please enter the vehicle's model name:");
            string modelName = Console.ReadLine();

            return modelName;
        }

        private string getVehicleLicenseNumber()
        {
            Console.WriteLine("Please enter the vehicle's license number:");
            string licenseNumber = Console.ReadLine();

            return licenseNumber;
        }

        private int convertStringToIntInRange(int i_MinimumRange, int i_MaximumRange)
        {
            int intValue;
            string userInput = Console.ReadLine();
            bool isNumber = int.TryParse(userInput, out intValue);

            if (!isNumber)
            {
                throw new FormatException($"This input \"{userInput}\" is not a number");
            }

            if (intValue < i_MinimumRange || i_MaximumRange < intValue)
            {
                throw new ValueOutOfRangeException(i_MinimumRange, i_MaximumRange, intValue, "numeric");
            }

            return intValue;
        }

        public int GetUserOperationInput()
        {
            const int k_MaximumOperationOption = 8;

            return convertStringToIntInRange(k_MinimumOption, k_MaximumOperationOption);
        }

        public bool GetFilterStatus(out eVehicleStatus o_FilterStatus)
        {
            Console.WriteLine("Would you like to filter by the status? <Y/N>");
            string userInput = Console.ReadLine();
            o_FilterStatus = eVehicleStatus.FixingProcess; ////This is an init

            if(userInput != k_YesInput && userInput != k_NoInput)
            {
                throw new FormatException($"This input: \"{userInput}\" does not match the options");
            }

            if(userInput == k_YesInput)
            {
                o_FilterStatus = (eVehicleStatus)GetEnumIntValue<eVehicleStatus>();
            }

            return userInput == k_YesInput;
        }

        public string GetVehicleLicensePlate()
        {
            Console.WriteLine("Please enter the license plate number: ");
            return Console.ReadLine();
        }

        public (eFuelType, string) GetVehicleFuelInformation()
        {
            eFuelType fuelType = (eFuelType)GetEnumIntValue<eFuelType>();
            string fuelAmount = getCapacity("fuel amount");

            return (fuelType, fuelAmount);
        }

        public float GetEnergyAmount()
        {
            return float.Parse(getCapacity("time"));
        }

        private void printEnumFields<T>()
        {
            string[] enumFields = Enum.GetNames(typeof(T));
            Console.WriteLine("Please choose one of the following options: ");

            for (int i = 0; i < enumFields.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {enumFields[i]}");
            }
        }
    }
}
