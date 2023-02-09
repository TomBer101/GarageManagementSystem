using System;
using System.Collections.Generic;
using System.Net;

namespace GarageManagerLibrary
{
    public class Car : Vehicle
    {
        protected eCarColors m_Color;
        protected eAmountOfDoors m_AmountOfDoors;
        private static readonly float sr_MaximumAirPressure = 29;

        public Car(VehicleEngine i_Engine) : base(i_Engine)
        {
            m_Wheels = new List<Wheel>(4);
            for (int i = 0; i < 4; i++)
            {
                m_Wheels.Add(new Wheel(sr_MaximumAirPressure));
            }
        }

        public eCarColors Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public eAmountOfDoors AmountOfDoors
        {
            get { return m_AmountOfDoors; }
            set { m_AmountOfDoors = value; }
        }

        public override string ToString()
        {
            string carInfo = string.Format(@"Color: {1}{0}Amount of doors: {2}", Environment.NewLine, m_Color.ToString(), m_AmountOfDoors);

            return base.ToString() + carInfo;
        }

        public override void SetVehicleProperties(List<string> i_VehicleProperties)
        {
            m_Color = (eCarColors)int.Parse(i_VehicleProperties[4]); // TODO: Ido, notice this conversion - i dont really like it.
            m_AmountOfDoors = (eAmountOfDoors)int.Parse(i_VehicleProperties[5]);
        }
    }
}
