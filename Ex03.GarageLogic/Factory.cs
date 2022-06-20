using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        public enum eVehicleType
        {
            Fuel_Car = 1,
            Electric_Car,
            Fuel_Motorcycle,
            Electric_Motorcycle,
            Truck
        }

        public const int k_MaxNameLength = 20;
        private eVehicleType m_VehicleType;

        public eVehicleType VehicleType
        {
            get
            {
                return m_VehicleType;
            }

            set
            {
                m_VehicleType = value;
            }
        }

        public VehicleData CreateNewObject(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_LicenseNumber, string i_WheelsManufacturerName)
        {
            VehicleData vehicleData;
            Owner owner = new Owner(i_OwnerName, i_OwnerPhoneNumber);
            switch (m_VehicleType)
            {
                case eVehicleType.Fuel_Car:
                    vehicleData = new VehicleData(owner, new Car(eEnergySource.Fuel, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName));
                    break;
                case eVehicleType.Electric_Car:
                    vehicleData = new VehicleData(owner, new Car(eEnergySource.Electric, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName));
                    break;
                case eVehicleType.Fuel_Motorcycle:
                    vehicleData = new VehicleData(owner, new Motorcycle(eEnergySource.Fuel, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName));
                    break;
                case eVehicleType.Electric_Motorcycle:
                    vehicleData = new VehicleData(owner, new Motorcycle(eEnergySource.Electric, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName));
                    break;
                default:
                    vehicleData = new VehicleData(owner, new Truck(eEnergySource.Fuel, i_ModelName, i_LicenseNumber, i_WheelsManufacturerName));
                    break;
            }

            return vehicleData;
        }
    }
}