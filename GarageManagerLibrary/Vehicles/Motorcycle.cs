using System;
using System.Collections.Generic;

namespace GarageManagerLibrary
{
    public class Motorcycle : Vehicle
    {
        protected eMotorcycleLicenseType m_licenseType;
        protected int m_EngineCapacity = 0;
        private static readonly float sr_MaximumAirPressure = 30;

        public Motorcycle(VehicleEngine i_Engine)
            : base(i_Engine)
        {
            m_Wheels = new List<Wheel>(2);
            for (int i = 0; i < 2; i++)
            {
                m_Wheels.Add(new Wheel(sr_MaximumAirPressure));
            }
        }

        public eMotorcycleLicenseType LicenseType
        {
            get { return m_licenseType; }
            set { m_licenseType = value; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }

        public override string ToString()
        {
            string engineInfo = string.Format(@"License type: {1}{0}Engine Capacity: {2}{0}", Environment.NewLine, m_licenseType.ToString(), m_EngineCapacity);

            return base.ToString() + engineInfo;
        }

        public override void SetVehicleProperties(List<string> i_VehicleProperties)
        {
            m_licenseType = (eMotorcycleLicenseType)int.Parse(i_VehicleProperties[4]);
            m_EngineCapacity = int.Parse(i_VehicleProperties[5]);
        }
    }
}
