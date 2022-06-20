using System;

namespace Ex03.GarageLogic
{
    public class Electric : EnergySource
    {
        public Electric(float i_MaxEnergy) : base(i_MaxEnergy) 
        { 
        }

        internal void ToRecharge(float i_AmountToAdd)
        {
            CheckIfEnergyRemainingLessThanMaxEnergy(i_AmountToAdd);
        }

        public override string ToString()
        {
            return string.Format("Percentage of energy remaining: {0}%{1}The battery has {2} hours left, out of {3}{4}", 
                PercentageOfEnergyRemaining, Environment.NewLine, EnergyRemaining, MaxEnergy, Environment.NewLine);
        }
    }
}
