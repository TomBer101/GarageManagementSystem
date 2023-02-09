using System;

namespace GarageManagerLibrary
{
    public class VehicleFuelEngine : VehicleEngine
    {
        private readonly eFuelType r_FuelType;

        public float ChargeEnergy(float i_AmountOfEnergy, eFuelType i_FuelType)
            {
                if(i_FuelType == r_FuelType)
                {
                    return base.ChargeEnergy(i_AmountOfEnergy);
                }
                else
                { 
                    throw new ArgumentException($"Fuel Type {i_FuelType} does not match with this vehicle, use {r_FuelType}.");
                }
            }

        public float CurrentFuelAmount
        {
            get { return m_CurrentEnergy; }
        }

        public VehicleFuelEngine(float i_FuelTank, eFuelType i_FuelType) : base(i_FuelTank)
        {
            r_FuelType = i_FuelType;
        }

        public override string ToString()
        {
            string engineInfo = string.Format(@"    Current amount of fuel: {1}L{0}    Fuel type: {2}{0}", Environment.NewLine, m_CurrentEnergy, r_FuelType.ToString());

            return engineInfo;
        }
    }
}
