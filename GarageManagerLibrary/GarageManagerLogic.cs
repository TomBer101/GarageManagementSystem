using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagerLibrary
{
    public class GarageManagerLogic
    {
        internal class GarageManagerLogicVehicleTicket
        {
            internal readonly Vehicle r_Vehicle = null;
            internal string m_OwnerName = string.Empty;
            internal string m_OwnerPhoneNumber = string.Empty;
            internal eVehicleStatus m_VehicleStatus = eVehicleStatus.FixingProcess;

            public GarageManagerLogicVehicleTicket(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                r_Vehicle = i_Vehicle;
            }

            public override string ToString()
            {
                string vehicleInfo = string.Format(@"Owner's name: {1}{0}Phone number: {2}{0}Vehicle's status: {3}{0}{4} ", Environment.NewLine, m_OwnerName, m_OwnerPhoneNumber, m_VehicleStatus, r_Vehicle.ToString());
                    
                return vehicleInfo;
            }
        }

        private static readonly Dictionary<string, GarageManagerLogicVehicleTicket> sr_CheckedInVehicles = new Dictionary<string, GarageManagerLogicVehicleTicket>();
        private static readonly VehicleFactory sr_VehicleFactory = new VehicleFactory();
        private static readonly List<string> sr_VehicleTypes = new List<string>() { "1.Fueled Car", "2.Electric Car", "3.Fueled Motorcycle", "4.Electric Motorcycle", "5.Truck" };
        
        public List<string> VehicleTypes
        {
            get { return sr_VehicleTypes; }
        }

        public Vehicle SetVehicle(int i_VehicleType, List<string> i_VehicleProperties) 
        {
            Vehicle vehicle = sr_VehicleFactory.CreateVehicle(i_VehicleType);
            vehicle.ModelName = i_VehicleProperties[0];
            vehicle.LicensePlate = i_VehicleProperties[1];
            vehicle.SetWheelsProperties(float.Parse(i_VehicleProperties[2]), i_VehicleProperties[3]);
            vehicle.SetVehicleProperties(i_VehicleProperties);

            return vehicle;
        }

        public void CheckInVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_PhoneNumber)
        {
            if(!sr_CheckedInVehicles.ContainsKey(i_Vehicle.LicensePlate))
            {
                sr_CheckedInVehicles.Add(i_Vehicle.LicensePlate, new GarageManagerLogicVehicleTicket(i_OwnerName, i_PhoneNumber, i_Vehicle));
            }
            else
            {
                sr_CheckedInVehicles[i_Vehicle.LicensePlate].m_VehicleStatus = eVehicleStatus.FixingProcess;
                throw new Exception($"The vehicle: {i_Vehicle.LicensePlate} has already checked in!");
            }
        }

        public string PrintAllCarDetails(string i_VehicleLicensePlate)
        {
            string stringToPrint = string.Empty;

            if(sr_CheckedInVehicles.ContainsKey(i_VehicleLicensePlate))
            {
                stringToPrint = $"{sr_CheckedInVehicles[i_VehicleLicensePlate]}{Environment.NewLine}";
            }
            else
            {
                throw new Exception($"This car {i_VehicleLicensePlate} is not in the garage");
            }

            return stringToPrint;
        }

        public void AddFuelToVehicle(string i_VehicleLicensePlate, eFuelType i_FuelType, float i_FuelAmount)
        {
            if (sr_CheckedInVehicles.ContainsKey(i_VehicleLicensePlate))
            {
                sr_CheckedInVehicles[i_VehicleLicensePlate].r_Vehicle.AddFuel(i_FuelType, i_FuelAmount);
            }
            else
            {
                throw new Exception($"This car {i_VehicleLicensePlate} is not in the garage");
            }
        }

        public string PrintAllCarLicensePlates(eVehicleStatus i_VehicleStatus, bool i_IsFilterResults)
        {
            bool printString = false;
            StringBuilder stringToPrint = new StringBuilder();

            foreach (KeyValuePair<string, GarageManagerLogicVehicleTicket> ticket in sr_CheckedInVehicles)
            {
                printString = !i_IsFilterResults || (i_IsFilterResults && i_VehicleStatus == ticket.Value.m_VehicleStatus);

                if (printString)
                {
                    stringToPrint.Append($"{ticket.Value.r_Vehicle.LicensePlate}{Environment.NewLine}");
                }
            }

            if(stringToPrint.Length == 0)
            {
                stringToPrint.Append("There are no cars to display");
            }

            return stringToPrint.ToString();
        }

        public string ChangeVehicleStatus(string i_VehicleLicensePlate, eVehicleStatus i_NewVehicleStatus)
        {
            if (sr_CheckedInVehicles.ContainsKey(i_VehicleLicensePlate))
            {
                eVehicleStatus oldStatus = sr_CheckedInVehicles[i_VehicleLicensePlate].m_VehicleStatus;
                sr_CheckedInVehicles[i_VehicleLicensePlate].m_VehicleStatus = i_NewVehicleStatus;
                return oldStatus.ToString();
            }
            else
            {
                throw new Exception($"This car {i_VehicleLicensePlate} is not in the garage");
            }
        }

        public void InflateWheelsToMaximum(string i_VehicleLicensePlate)
        {
            if (sr_CheckedInVehicles.ContainsKey(i_VehicleLicensePlate))
            {
                sr_CheckedInVehicles[i_VehicleLicensePlate].r_Vehicle.InflateWheelsToMaximum();
            }
            else
            {
                throw new Exception($"This car {i_VehicleLicensePlate} is not in the garage");
            }
        }

        public void AddEnergyToVehicle(string i_VehicleLicensePlate, float i_EnergyAmount)
        {
            if (sr_CheckedInVehicles.ContainsKey(i_VehicleLicensePlate))
            {
                sr_CheckedInVehicles[i_VehicleLicensePlate].r_Vehicle.AddEnergy(i_EnergyAmount);
            }
            else
            {
                throw new Exception($"This car {i_VehicleLicensePlate} is not in the garage");
            }
        }
    }
}
