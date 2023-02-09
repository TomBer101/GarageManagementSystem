using System;

namespace GarageManagerLibrary
{
    public abstract class VehicleEngine
    {
        protected float m_CurrentEnergy = 0;
        protected float m_MaxEnergy = 0;

        public VehicleEngine(float i_MaxEnergy)
        {
            m_MaxEnergy = i_MaxEnergy;
        }

        public float ChargeEnergy(float i_AmountOfEnergy)
        {
            if (m_CurrentEnergy + i_AmountOfEnergy <= m_MaxEnergy)
            {
                m_CurrentEnergy += i_AmountOfEnergy;
            }
            else
            {
                throw new ValueOutOfRangeException(0, m_MaxEnergy - m_CurrentEnergy, i_AmountOfEnergy, "energy");
            }

            return (m_CurrentEnergy / m_MaxEnergy) * 100;
        }
    }
}
