using System;
using System.Collections.Generic;

namespace GarageManagerLibrary
{
    public class Truck : Vehicle
    {
        private static readonly float sr_MaximumAirPressure = 25;
        private bool m_IsFreezer = false;
        private float m_TrunkVolume = 0;

        public Truck(VehicleEngine i_Engine)
            : base(i_Engine)
        {
            m_Wheels = new List<Wheel>(16);
            for (int i = 0; i < 16; i++)
            {
                m_Wheels.Add(new Wheel(sr_MaximumAirPressure));
            }
        }

        public bool IsFreezer
        {
            get { return m_IsFreezer; }
            set { m_IsFreezer = value; }
        }

        public float TrunkVolume
        {
            get { return m_TrunkVolume; }
            set { m_TrunkVolume = value; }
        }

        public override string ToString()
        {
            string engineInfo = string.Format(@"Freezer: {1}{0}Trunk capacity: {2}{0}", Environment.NewLine, IsFreezer ? "Yes" : "No", m_TrunkVolume);

            return base.ToString() + engineInfo;
        }

        public override void SetVehicleProperties(List<string> i_VehicleProperties)
        {
            m_IsFreezer = i_VehicleProperties[4] == "Y";
            m_TrunkVolume = float.Parse(i_VehicleProperties[5]);
        }
    }
}
