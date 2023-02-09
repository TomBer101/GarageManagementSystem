using System;
using System.Collections.Generic;
using System.Text;

namespace GarageManagerLibrary
{
    public abstract class Vehicle
    {
        protected string m_ModelName = string.Empty;
        protected string m_LicensePlate = string.Empty;
        protected float m_EnergyPercentage = 0;
        protected List<Wheel> m_Wheels;
        protected VehicleEngine m_Engine;

        public Vehicle(VehicleEngine i_Engine)
        {
            m_Engine = i_Engine;
        }

        public string LicensePlate
        {
            get { return m_LicensePlate; }
            set { m_LicensePlate = value; }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        private static string listToString<T>(List<T> i_List)
        {
            StringBuilder liStringBuilder = new StringBuilder();

            foreach (T item in i_List)
            {
                liStringBuilder.Append(item.ToString());
            }

            return liStringBuilder.ToString();
        }

        public override string ToString()
        {
            string vehicleInfo = string.Format(
                @"License Plate: {1}{0}Model Name: {2}{0}Energy percentage: {3:0.00}%{0}Engine status: {0}{4}Wheels:{0}{5}", 
                Environment.NewLine, 
                m_LicensePlate, 
                m_ModelName, 
                m_EnergyPercentage, 
                m_Engine.ToString(), 
                listToString<Wheel>(m_Wheels));
            
            return vehicleInfo;
        }

        public void InflateWheelsToMaximum()
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.CurrentAirPressure = wheel.MaxAirPressue;
            }
        }

        internal void AddEnergy(float i_EnergyAmount)
        {
            VehicleElectricEngine engine = m_Engine as VehicleElectricEngine;

            if(engine != null)
            {
                m_EnergyPercentage = engine.ChargeEnergy(i_EnergyAmount);
            }
            else
            {
                throw new ArgumentException("This Vehicle has a Fuel engine!");
            }
        }

        internal void AddFuel(eFuelType i_FuelType, float i_FuelAmount)
        {
            VehicleFuelEngine engine = m_Engine as VehicleFuelEngine;

            if(engine != null)
            {
                m_EnergyPercentage = engine.ChargeEnergy(i_FuelAmount, i_FuelType);
            }
            else
            {
                throw new ArgumentException("This Vehicle has an electric engine!");
            }
        }

        public abstract void SetVehicleProperties(List<string> i_VehicleProperties);

        public virtual void SetWheelsProperties(float i_CurrentAirPressure, string i_Manufacturer)
        {
            foreach(Wheel wheel in m_Wheels)
            {
                wheel.SetWheel(i_CurrentAirPressure, i_Manufacturer);
            }
        }
    }
}
