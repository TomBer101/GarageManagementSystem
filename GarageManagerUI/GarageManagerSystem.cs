using System;
using System.Collections.Generic;
using GarageManagerLibrary;

namespace GarageLogic
{
    public class GarageManagerSystem
    {
        private readonly GarageManagerLogic r_GarageManager = new GarageManagerLogic();
        private readonly InputHandler r_InputHandler = new InputHandler();

        public void StartGarageManager()
        {
            int userOperationInput = 0;
            const int k_ExitInput = 8;

            while(userOperationInput != k_ExitInput)
            {
                printOptionsMenu();

                try
                {
                    userOperationInput = r_InputHandler.GetUserOperationInput();
                    Console.Clear();
                    switchCaseCommands(userOperationInput);
                }
                catch(FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
                catch(ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void printOptionsMenu()
        {
            Console.WriteLine(string.Format(@"Please choose one of the following actions:
1. Check in a car to the garage
2. Display all the cars in garage by license plate number
3. Change car state
4. inflate vehicle wheels
5. Fuel vehicle
6. Charge electric vehicle
7. Show full vehicle details
8. Exit system"));
        }

        private void switchCaseCommands(int i_UserOptionInput)
        {
            switch (i_UserOptionInput)
            {
                case 1:
                    executeCheckInAction();
                    break;
                case 2:
                    executePrintAllLicensePlatesAction();
                    break;
                case 3:
                    executeChangeVehicleStateAction();
                    break;
                case 4:
                    executeInflateVehicleWheelsAction();
                    break;
                case 5:
                    executeFillFuelInVehicleAction();
                    break;
                case 6:
                    executeFillEnergyInVehicleAction();
                    break;
                case 7:
                    executeDisplayAllVehicleDetailsAction();
                    break;
            }
        }

        private void executeCheckInAction()
        {
            (int vehicleType, List<string> vehicleProperties) tuple = r_InputHandler.GetVehicleInformation(r_GarageManager.VehicleTypes);
            Vehicle vehicle = r_GarageManager.SetVehicle(tuple.vehicleType, tuple.vehicleProperties);
            (string ownerName, string ownerPhoneNumber) ownerDetails = r_InputHandler.GetOwnerInformation();
            r_GarageManager.CheckInVehicle(vehicle, ownerDetails.ownerName, ownerDetails.ownerPhoneNumber);
            Console.Clear();
            Console.WriteLine($"The car with the license plate {(string)tuple.vehicleProperties[1]} was successfully checked in.{Environment.NewLine}");
        }

        private void executePrintAllLicensePlatesAction()
        {
            eVehicleStatus vehicleStatus;
            bool isFilterVehiclesByStatus = r_InputHandler.GetFilterStatus(out vehicleStatus);
            Console.Clear();
            Console.WriteLine(r_GarageManager.PrintAllCarLicensePlates(vehicleStatus, isFilterVehiclesByStatus));
        }

        private void executeChangeVehicleStateAction()
        {
            string licensePlate = r_InputHandler.GetVehicleLicensePlate();
            eVehicleStatus vehicleStatus = (eVehicleStatus)r_InputHandler.GetEnumIntValue<eVehicleStatus>();
            string oldStatus = r_GarageManager.ChangeVehicleStatus(licensePlate, vehicleStatus);
            Console.Clear();
            Console.WriteLine($"Vehicle {licensePlate} has changed status from {oldStatus} to {vehicleStatus}.{Environment.NewLine}");
        }

        private void executeInflateVehicleWheelsAction()
        {
            string licensePlate = r_InputHandler.GetVehicleLicensePlate();
            r_GarageManager.InflateWheelsToMaximum(licensePlate);
            Console.Clear();
            Console.WriteLine($"The wheels of vehicle number {licensePlate}, were fully inflated.{Environment.NewLine}");
        }

        private void executeFillFuelInVehicleAction()
        {
            string licensePlate = r_InputHandler.GetVehicleLicensePlate();
            (eFuelType fuelType, string fuelAmount) gasInformation = r_InputHandler.GetVehicleFuelInformation();
            r_GarageManager.AddFuelToVehicle(licensePlate, gasInformation.fuelType, float.Parse(gasInformation.fuelAmount));
            Console.Clear();
            Console.WriteLine($"Vehicle number {licensePlate}, successfully got fueled.{Environment.NewLine}");
        }

        private void executeFillEnergyInVehicleAction()
        {
            string licensePlate = r_InputHandler.GetVehicleLicensePlate();
            float energyAmount = r_InputHandler.GetEnergyAmount();
            r_GarageManager.AddEnergyToVehicle(licensePlate, energyAmount);
            Console.Clear();
            Console.WriteLine($"Vehicle number {licensePlate}, successfully got charged.{Environment.NewLine}");
        }

        private void executeDisplayAllVehicleDetailsAction()
        {
            string licensePlate = r_InputHandler.GetVehicleLicensePlate();
            Console.Clear();
            Console.WriteLine(r_GarageManager.PrintAllCarDetails(licensePlate));
        }
    }
}
