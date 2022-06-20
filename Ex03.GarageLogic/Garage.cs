using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Factory m_CurrentObject;
        private Dictionary<string, VehicleData> m_VehiclesData;
        private string m_CurrentLicenseNumber;

        public Garage()
        {
            m_CurrentObject = new Factory();
            m_VehiclesData = new Dictionary<string, VehicleData>();
        }

        public Factory CurrentVehicle
        {
            get
            {
                return m_CurrentObject;
            }

            set
            {
                m_CurrentObject = value;
            }
        }

        public Dictionary<string, VehicleData> VehiclesData
        {
            get
            {
                return m_VehiclesData;
            }

            set
            {
                m_VehiclesData = value;
            }
        }

        public string CurrentLicenseNumber
        {
            get
            {
                return m_CurrentLicenseNumber;
            }

            set
            {
                m_CurrentLicenseNumber = value;
            }
        }

        public bool CheckIfVehicleInGarage()
        {
            return m_VehiclesData.ContainsKey(m_CurrentLicenseNumber);
        }

        public void InitializeVehicleStatus()
        {
            m_VehiclesData[m_CurrentLicenseNumber].Vehicle.Status = eStatus.InRepair;
        }

        public void UpdateVehicleStatus(eStatus i_NewStatus)
        {
            if(!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            m_VehiclesData[m_CurrentLicenseNumber].Vehicle.Status = i_NewStatus;
        }

        public void UpdeteEnergyRemaining(float i_EnergyRemaining)
        {
            m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySource.EnergyRemaining = i_EnergyRemaining;
        }

        public List<Wheel> GetCurrentVehicleWheels()
        {
            return m_VehiclesData[m_CurrentLicenseNumber].Vehicle.Wheels;
        }

        public int GetCurrentVehicleWheelsNumber()
        {
            return VehiclesData[m_CurrentLicenseNumber].Vehicle.Wheels.Count;
        }

        public float GetCurrentVehicleMaxAirPressure()
        {
            return VehiclesData[m_CurrentLicenseNumber].Vehicle.Wheels[0].MaxAirPressure;
        }

        public float GetcurrentVehicleMaxEnergy()
        {
            return m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySource.MaxEnergy;
        }

        public void UpdatePrecentageOfEnergyRemaining(float i_PercentageOfEnergyRemaining)
        {
            m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySource.PercentageOfEnergyRemaining = i_PercentageOfEnergyRemaining;
        }

        public eEnergySource GetCurrentVehiclesEnergySource()
        {
            return m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySourceType;
        }

        public VehicleData CreateNewVehicle(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_WheelsManufacturerName)
        {
            VehicleData newObject = m_CurrentObject.CreateNewObject(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, m_CurrentLicenseNumber, i_WheelsManufacturerName);
            m_VehiclesData.Add(m_CurrentLicenseNumber, newObject);

            return newObject;
        }

        public void InflateAirInWheels()
        {
            List<Wheel> wheelsList;

            if (!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            wheelsList = GetCurrentVehicleWheels();

            foreach(Wheel wheel in wheelsList)
            {
                wheel.InflateWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void CheckIfEnergySourceInputMatchesToCurrentVehiclesEnergySource(eEnergySource i_EnergySource)
        {
            if (!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            if (m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySourceType != i_EnergySource)
            {
                throw new ArgumentException(string.Format("Energy source doesn't match to your vehicle type!"));
            }
        }

        public void RefuelVehiclePoweredByFuel(Fuel.eFuelType i_FuelType, float i_FuelAmount)
        {
            Fuel fuel;

            if (!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            fuel = m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySource as Fuel;
            fuel.ToRefuel(i_FuelType, i_FuelAmount);
            fuel.UpdatePercentageOfEnergyRemaining();
        }

        public void ChargeAnElectricVehicle(float i_AmountOfMinutesToCharge)
        {
            Electric electric;

            if (!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            CheckIfEnergySourceInputMatchesToCurrentVehiclesEnergySource(eEnergySource.Electric);
            electric = m_VehiclesData[m_CurrentLicenseNumber].Vehicle.EnergySource as Electric;
            electric.ToRecharge(i_AmountOfMinutesToCharge);
            electric.UpdatePercentageOfEnergyRemaining();
        }

        public string GetVehicleData()
        {
            if (!CheckIfVehicleInGarage())
            {
                throw new ArgumentException(string.Format("License number {0} does not exist in the garage!", m_CurrentLicenseNumber));
            }

            return VehiclesData[m_CurrentLicenseNumber].ToString();
        }
    }
}
