using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private enum eColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        private enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private const int k_NumOfWheels = 4;
        private const int k_NumOfUtils = 2;
        private const int k_MaxAirPressure = 29;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;
        private const float k_MaxFuelEnergyInLiters = 38f;
        private const float k_MaxBatteryEnergyInHours = 3.3f;
        private eColor m_Color;
        private eNumberOfDoors m_DoorsAmount;

        public Car(eEnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacturerName) : 
            base(i_EnergySource, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName, k_NumOfWheels, k_MaxAirPressure) 
        {
            InitializeEnergySource();
            InitializeOutputUtils();
        }

        protected override void InitializeEnergySource()
        {
            switch (EnergySourceType)
            {
                case eEnergySource.Electric:
                    EnergySource = new Electric(k_MaxBatteryEnergyInHours);
                    break;
                default:
                    EnergySource = new Fuel(k_MaxFuelEnergyInLiters, k_FuelType);
                    break;
            }
        }

        protected override void InitializeOutputUtils()
        {
            string outputColorUtil, outputNumberOfDoorsUtil;

            outputColorUtil = @"Color:
1. Red
2. White
3. Green
4. Blue";
            outputNumberOfDoorsUtil = @"Number of doors:
1. Two
2. Three
3. Four
4. Five";
            OutputUtils.Add(outputColorUtil);
            OutputUtils.Add(outputNumberOfDoorsUtil);
        }

        public override void CheckUtilsValidationAccordingUserInputAndUpdate(string i_Input, int i_CurrentUtil, out bool io_IsValid)
        {
            int input = int.Parse(i_Input);

            CheckIfUserInputIsValid(input, 1, 4, out io_IsValid);
            if (i_CurrentUtil == 0)
            {
                getColorFromInput(input);
            }
            else if(i_CurrentUtil == 1)
            {
                getDoorsAmountFromInput(input);
            }

            io_IsValid = true;
        }

        private void getColorFromInput(int i_Input)
        {
            switch (i_Input)
            {
                case 1:
                    m_Color = eColor.Red;
                    break;
                case 2:
                    m_Color = eColor.White;
                    break;
                case 3:
                    m_Color = eColor.Green;
                    break;
            }
        }

        private void getDoorsAmountFromInput(int i_Input)
        {
            switch (i_Input)
            {
                case 1:
                    m_DoorsAmount = eNumberOfDoors.Two;
                    break;
                case 2:
                    m_DoorsAmount = eNumberOfDoors.Three;
                    break;
                case 3:
                    m_DoorsAmount = eNumberOfDoors.Four;
                    break;
                default:
                    m_DoorsAmount = eNumberOfDoors.Five;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}Car color: {1}{2}Doors Amount: {3}{4}", base.ToString(), m_Color, Environment.NewLine, m_DoorsAmount, Environment.NewLine);
        }
    }
}
