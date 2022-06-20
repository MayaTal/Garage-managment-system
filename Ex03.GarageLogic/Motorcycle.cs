using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            B1,
            BB
        }

        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressure = 31;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan98;
        private const float k_MaxFuelEnergyInLiters = 6.2f;
        private const float k_MaxBatteryEnergyInHours = 2.5f;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacityInCC;

        public Motorcycle(eEnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacturerName) :
            base(i_EnergySource, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName, k_NumOfWheels, k_MaxAirPressure) 
        {
            InitializeEnergySource();
            InitializeOutputUtils();
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineCapacityInCC
        {
            get
            {
                return m_EngineCapacityInCC;
            }

            set
            {
                m_EngineCapacityInCC = value;
            }
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
            string output = @"License type: 
1. A
2. A1
3. B1
4. BB";

            OutputUtils.Add(output);
            OutputUtils.Add("Engine capacity in CC: ");
        }

        public override void CheckUtilsValidationAccordingUserInputAndUpdate(string i_Input, int i_CurrentUtil, out bool io_IsValid)
        {
            int input = int.Parse(i_Input);

            if (i_CurrentUtil == 0)
            {
                CheckIfUserInputIsValid(input, 1, 4, out io_IsValid);
                getLicenseTypeFromUser(input);
            }
            else if (i_CurrentUtil == 1)
            {
                checkIfEngineCapacityInCCIsValid(input, out io_IsValid);
                m_EngineCapacityInCC = input;
            }

            io_IsValid = true;
        }

        private void checkIfEngineCapacityInCCIsValid(int i_Input, out bool i_IsValid)
        {
            i_IsValid = i_Input >= 0;
            if (!i_IsValid)
            {
                throw new ArgumentException("Invalid input! Engine capacity is positive number.");
            }
        }

        private void getLicenseTypeFromUser(int i_Input)
        {
            switch (i_Input)
            {
                case 1:
                    m_LicenseType = eLicenseType.A;
                    break;
                case 2:
                    m_LicenseType = eLicenseType.A1;
                    break;
                case 3:
                    m_LicenseType = eLicenseType.B1;
                    break;
                case 4:
                    m_LicenseType = eLicenseType.BB;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}Motorcycle license type: {1}{2}Engine capacity in CC: {3}{4}",
                base.ToString(), m_LicenseType, Environment.NewLine, m_EngineCapacityInCC, Environment.NewLine);
        }
    }
}
