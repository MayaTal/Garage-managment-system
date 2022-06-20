using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private readonly float r_MaxEnergy;
        private float m_PercentageOfEnergyRemaining;
        private float m_EnergyRemaining;

        public EnergySource(float i_MaxEnergy)
        {
            r_MaxEnergy = i_MaxEnergy;
        }

        public float PercentageOfEnergyRemaining
        {
            get
            {
                return m_PercentageOfEnergyRemaining;
            }

            set
            {
                m_PercentageOfEnergyRemaining = value;
            }
        }

        public float EnergyRemaining
        {
            get
            {
                return m_EnergyRemaining;
            }

            set
            {
                m_EnergyRemaining = value;
            }
        }

        public float MaxEnergy
        {
            get
            {
                return r_MaxEnergy;
            }
        }

        internal void CheckIfEnergyRemainingLessThanMaxEnergy(float i_AmountToAdd)
        {
            float newEnergyRemaining = m_EnergyRemaining + i_AmountToAdd;

            if (newEnergyRemaining <= r_MaxEnergy)
            {
                m_EnergyRemaining = newEnergyRemaining;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxEnergy - m_EnergyRemaining);
            }
        }

        internal void UpdatePercentageOfEnergyRemaining()
        {
            m_PercentageOfEnergyRemaining = (m_EnergyRemaining / r_MaxEnergy) * 100;
        }

        public abstract string ToString();
    }
}
