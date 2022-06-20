using System;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType
        {
            Octan98 = 1,
            Octan96,
            Octan95,
            Soler
        }

        private eFuelType m_Type;

        public Fuel(float i_MaxEnergy, eFuelType i_Type) : base(i_MaxEnergy)
        {
            m_Type = i_Type;
        }

        private void checkIfFuelTypeMatches(Fuel.eFuelType i_FuelType)
        {
            if (m_Type != i_FuelType)
            {
                throw new ArgumentException(string.Format("{0} does not match to your vehicle fuel type!", i_FuelType));
            }
        }

        internal void ToRefuel(Fuel.eFuelType i_FuelType, float i_AmountToAdd)
        {
            checkIfFuelTypeMatches(i_FuelType);
            CheckIfEnergyRemainingLessThanMaxEnergy(i_AmountToAdd);
        }

        public override string ToString()
        {
            return string.Format("Percentage of energy remaining: {0}%{1}Fuel type: {2}{3}{4} liters of fuel remains out of {5}{6}", 
                PercentageOfEnergyRemaining, Environment.NewLine, m_Type, Environment.NewLine, EnergyRemaining, MaxEnergy, Environment.NewLine);
        }
    }
}