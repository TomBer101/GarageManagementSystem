using System;

namespace GarageManagerLibrary
{
    public class VehicleElectricEngine : VehicleEngine
    {
        public VehicleElectricEngine(float i_MaxAmountOfHours)
            : base(i_MaxAmountOfHours)
        {
        }

        public override string ToString()
        {
            string engineInfo = string.Format(@"    Time left: {1}H{0}", Environment.NewLine, m_CurrentEnergy);

            return engineInfo;
        }
    }
}
