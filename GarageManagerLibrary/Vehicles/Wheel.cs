using System;

namespace GarageManagerLibrary
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure = 0;
        private string m_NameOfManufacture = string.Empty;
        private float m_CurrentAirPressure = 0;

        public Wheel(float i_MaxAirPressure)
        {
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float MaxAirPressue
        {
            get { return r_MaxAirPressure; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public void AddAir(float i_AmountOfAirToAdd)
        {
            if (m_CurrentAirPressure + i_AmountOfAirToAdd <= r_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AmountOfAirToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure - m_CurrentAirPressure, i_AmountOfAirToAdd, "wheel pressure");
            }
        }

        public override string ToString()
        {
            string wheelInfo = string.Format(@"    Air pressure: {1}{0}    Manifacturer: {2}{0}", Environment.NewLine, m_CurrentAirPressure, m_NameOfManufacture);

            return wheelInfo;
        }

        internal void SetWheel(float i_CurrentAirPressure, string i_Manufacturer)
        {
            AddAir(i_CurrentAirPressure);
            m_NameOfManufacture = i_Manufacturer;
        }
    }
}
