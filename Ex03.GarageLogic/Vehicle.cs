using System;
using System.Text;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;
        private readonly eEnergySource r_TypeOfEnergySource;
        private EnergySource m_EnergySource;
        private List<Wheel> m_Wheels;
        private eStatus m_Status;
        private List<string> m_OutputUtils;

        public Vehicle(eEnergySource i_EnergySource, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacturerName, int i_NumOfWheels, int i_MaxAirPressureInWheels)
        {
            r_TypeOfEnergySource = i_EnergySource;
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            initializeWheels(i_WheelsManufacturerName, i_NumOfWheels, i_MaxAirPressureInWheels);
            m_Status = eStatus.InRepair;
            m_OutputUtils = new List<string>();
        }

        public eEnergySource EnergySourceType
        {
            get
            {
                return r_TypeOfEnergySource;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }

            set
            {
                m_EnergySource = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }

            set
            {
                m_Wheels = value;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                m_Status = value;
            }
        }

        public List<string> OutputUtils
        {
            get
            {
                return m_OutputUtils;
            }

            set
            {
                m_OutputUtils = value;
            }
        }

        private void initializeWheels(string i_ManufacturerName, int i_NumOfWheels, int i_MaxAirPressure)
        {
            m_Wheels = new List<Wheel>();
            for (int i = 0; i < i_NumOfWheels; ++i)
            {
                Wheels.Add(new Wheel(i_ManufacturerName, i_MaxAirPressure));
            }
        }

        protected abstract void InitializeEnergySource();

        protected abstract void InitializeOutputUtils();

        public abstract void CheckUtilsValidationAccordingUserInputAndUpdate(string i_Input, int i_CurrentUtil, out bool io_IsValid);

        protected void CheckIfUserInputIsValid(float i_Input, float i_MinValue, float i_MaxValue, out bool io_IsValid)
        {
            io_IsValid = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
            if (!io_IsValid)
            {
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
            }
        }

        public override string ToString()
        {
            StringBuilder vehicle = new StringBuilder(string.Format("License number: {0}{1}Model name: {2}{3}Status: {4}{5}{6}",
               r_LicenseNumber, Environment.NewLine, r_ModelName, Environment.NewLine, m_Status, Environment.NewLine, m_EnergySource.ToString()));
            int currentWheel = 1;

            foreach (Wheel wheel in m_Wheels)
            {
                vehicle.Append(string.Format("Wheel number {0}: {1}", currentWheel, wheel.ToString()));
                currentWheel++;
            }

            return vehicle.ToString();
        }
    }
}
