using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumOfWheels = 16;
        private const int k_MaxAirPressure = 24;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;
        private const float k_MaxFuelEnergyInLiters = 120f;
        private bool m_DoesDriveRefrigeratedContents;
        private float m_CargoVolume;

        public Truck(eEnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacturerName) :
            base(i_EnergySource, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName, k_NumOfWheels, k_MaxAirPressure) 
        {
            InitializeEnergySource();
            InitializeOutputUtils();
        }

        public bool DoesDriveRefrigeratedContents
        {
            get
            {
                return m_DoesDriveRefrigeratedContents;
            }

            set
            {
                m_DoesDriveRefrigeratedContents = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }

            set
            {
                m_CargoVolume = value;
            }
        }

        protected override void InitializeEnergySource()
        {
            EnergySource = new Fuel(k_MaxFuelEnergyInLiters, k_FuelType);
        }

        protected override void InitializeOutputUtils()
        {
            string output = @"Does drive refrigerated contents: 
1. Yes
2. No ";

            OutputUtils.Add(output);
            OutputUtils.Add("Cargo volume: ");
        }

        public override void CheckUtilsValidationAccordingUserInputAndUpdate(string i_Input, int i_CurrentUtil, out bool io_IsValid)
        {
            int input = int.Parse(i_Input);
            io_IsValid = false;

            if (i_CurrentUtil == 0)
            {
                CheckIfUserInputIsValid(input, 1, 2, out io_IsValid);
                checkIfDriveRefrigeratedContentsFromUserInput(input);
            }
            else if (i_CurrentUtil == 1)
            {
                m_CargoVolume = float.Parse(i_Input);
                io_IsValid = true;
            }
        }

        private void checkIfDriveRefrigeratedContentsFromUserInput(int i_Input)
        {
            switch (i_Input)
            {
                case 1:
                    m_DoesDriveRefrigeratedContents = true;
                    break;
                case 2:
                    m_DoesDriveRefrigeratedContents = false;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}The truck {1}{2}Cargo volume: {3}{4}",
                base.ToString(), displayIfDrivesRefrigeratedContents(), Environment.NewLine, m_CargoVolume, Environment.NewLine);
        }

        private string displayIfDrivesRefrigeratedContents()
        {
            string message;

            if (DoesDriveRefrigeratedContents)
            {
                message = "does drive refrigerated contents.";
            }
            else
            {
                message = "doesn't drive refrigerated contents.";
            }

            return message;
        }
    }
}
