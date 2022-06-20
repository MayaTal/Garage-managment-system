using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    class UserInterface
    {
        private const int k_MaxLicenseNumberLength = 8;
        private const int k_MinLicenseNumberLength = 5;
        private const int k_Hour = 60;
        private Garage m_Garage;
        private UIManager m_UIManager;
        private bool m_IsUserInTheGarage;
        private float m_CurrentInput;

        internal UserInterface()
        {
            m_Garage = new Garage();
            m_UIManager = new UIManager();
            m_IsUserInTheGarage = true;
        }

        internal void RunMenu()
        {
            Console.WriteLine("Welcome to our GARAGE!");
            while (m_IsUserInTheGarage)
            {
                getInputFromUser();
                Thread.Sleep(3000);
                Console.Clear();
            }
        }

        private void getInputFromUser()
        {
            string output = "Please enter license number: ";
            string input;

            getActionChoice();
            if (m_CurrentInput != 2 && m_CurrentInput != 8)
            {
                m_UIManager.GetNumber(out input, output, k_MinLicenseNumberLength, k_MaxLicenseNumberLength);
                m_Garage.CurrentLicenseNumber = input;
            }

            try
            {
                implenmentUserRequest();
            }
            catch (ArgumentException argumentException)
            {
                Console.WriteLine(argumentException.Message);
            }
    }

        private void getActionChoice()
        {
            string output = @"Please enter what you whould like to do from the following options:
1. Add new vehicle to the Garage.
2. Display license numbers list of vehicle in the garage.
3. Change vehicle status.
4. Inflate air in wheels to maximum.
5. Refuel vehicle powered by fuel.
6. Charge an electric vehicle.
7. Display vehicle data.
8. Exit.";

           m_UIManager.HandleInputValidation("int", output, 1, 8, out m_CurrentInput);
        }
  
        private void implenmentUserRequest()
        {
            switch (m_CurrentInput)
            {
                case 1:
                    addNewVehicle();
                    break;
                case 2:
                    displayLicenseNumbers();
                    break;
                case 3:
                    changeVehicleStatus();
                    break;
                case 4:
                    inflateAirInWheelsToMax();
                    break;
                case 5:
                    m_Garage.CheckIfEnergySourceInputMatchesToCurrentVehiclesEnergySource(eEnergySource.Fuel);
                    refuelVehiclePoweredByFuel();
                    break;
                case 6:
                    m_Garage.CheckIfEnergySourceInputMatchesToCurrentVehiclesEnergySource(eEnergySource.Electric);
                    chargeAnElectricVehicle();
                    break;
                case 7:
                    displayVehicleData();
                    Thread.Sleep(2000);
                    break;
                case 8:
                    m_IsUserInTheGarage = false;
                    break;
            }
        }

        private void addNewVehicle()
        {
            bool isVehicleInTheGarage = m_Garage.CheckIfVehicleInGarage();

            if (isVehicleInTheGarage)
            {
                m_Garage.InitializeVehicleStatus();
            }
            else
            {
                getVehicleDataFromUser();
            }
        }

        private void getVehicleDataFromUser()
        {
            string ownerName, ownerPhoneNumber, modelName, wheelsManufacturerName, output;

            output = "Please enter your name: ";
            m_UIManager.GetName(out ownerName, output);
            output = "Please enter your phone number: ";
            m_UIManager.GetNumber(out ownerPhoneNumber, output, Owner.k_OwnerPhoneNumberLength, Owner.k_OwnerPhoneNumberLength);
            getVehicleTypeAndEnergySource();
            output = "Please enter your vehicle model name: ";
            m_UIManager.GetName(out modelName, output);
            output = "Please enter your wheels manufacturer name: ";
            m_UIManager.GetName(out wheelsManufacturerName, output);
            createVehicleFromUserInput(ownerName, ownerPhoneNumber, modelName, wheelsManufacturerName);
        }

        private void setVehiclesSpecificData()
        {
            setCurrentAirPressureInWheels();
            setPercentageOfEnergyRemaining();
            setEnergyRemainingAccordingEnergySourceType();
        }

        private void setCurrentAirPressureInWheels()
        {
            List<Wheel> currentVehicleWheels = m_Garage.GetCurrentVehicleWheels();
            int wheelsNumber = m_Garage.GetCurrentVehicleWheelsNumber();
            float currentVehicleMaxAirPressure = m_Garage.GetCurrentVehicleMaxAirPressure();

            for (int i = 0; i < wheelsNumber; ++i)
            {
                m_UIManager.HandleInputValidation("float", string.Format("Please enter current air pressure in wheel numebr {0}: ", i + 1),
                    0, currentVehicleMaxAirPressure, out m_CurrentInput);
                currentVehicleWheels[i].CurrentAirPressure = m_CurrentInput;
            }
        }

        private void setPercentageOfEnergyRemaining()
        {
            float percentageOfEnergyRemaining;

            getPercentageOfEnergyRemaining(out percentageOfEnergyRemaining);
            m_Garage.UpdatePrecentageOfEnergyRemaining(percentageOfEnergyRemaining);
        }

        private void setEnergyRemainingAccordingEnergySourceType()
        {
            string output;
            float currentVehiclesMaxEnergy = m_Garage.GetcurrentVehicleMaxEnergy();

            if (m_Garage.GetCurrentVehiclesEnergySource() == eEnergySource.Fuel)
            {
                output = "Please enter your current amount of fuel in liters: ";
            }
            else
            {
                output = "Please enter your battery time remaining in hours: ";
            }

            m_UIManager.HandleInputValidation("float", output, 0, currentVehiclesMaxEnergy, out m_CurrentInput);
            m_Garage.UpdeteEnergyRemaining(m_CurrentInput);
        }

        private void getPercentageOfEnergyRemaining(out float io_PercentageOfEnergyRemaining)
        {
            string output = "Please enter your vehicle percentage of energy remaining: ";

            m_UIManager.HandleInputValidation("float", output, 0, 100, out m_CurrentInput);
            io_PercentageOfEnergyRemaining = m_CurrentInput;
        }

        private void getVehicleTypeAndEnergySource()
        {
            int numberofVehicleTypes;
            StringBuilder output = m_UIManager.CreateScreenOutputAccordingVehicleType("Please choose your vehicle type from the following options:");

            numberofVehicleTypes = Enum.GetNames(typeof(Factory.eVehicleType)).Length;
            m_UIManager.HandleInputValidation("int", output.ToString(), 1, numberofVehicleTypes, out m_CurrentInput);
            m_Garage.CurrentVehicle.VehicleType = (Factory.eVehicleType)m_CurrentInput;
        }

        private void createVehicleFromUserInput(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName, string i_WheelsManufacturerName)
        {
            VehicleData newVehicle = m_Garage.CreateNewVehicle(i_OwnerName, i_OwnerPhoneNumber, i_ModelName, i_WheelsManufacturerName);

            setVehiclesSpecificData();
            m_UIManager.GetSpecificCharacteristicsAccordingVehicleType(newVehicle);
        }

        private List<string> getLicenseNumbersListOfVehicleInTheGarageAccordingStatus()
        {
            List<string> licesneNumbers = new List<string>(m_Garage.VehiclesData.Count);
            eStatus status;
            string output = @"Please choose from the following options the way you want to filter the license number list:
1. In repair 
2. Fixed 
3. Paid ";

            if (checkIfUserWantsToDisplayLicenseNumbersAccordingStatus())
            {
                status = getTheRequiredStatus(output);
                foreach (KeyValuePair<string, VehicleData> vehicle in m_Garage.VehiclesData)
                {
                    if (vehicle.Value.Vehicle.Status == status)
                    {
                        licesneNumbers.Add(vehicle.Key);
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, VehicleData> vehicle in m_Garage.VehiclesData)
                {
                    licesneNumbers.Add(vehicle.Key);
                }
            }
            
            return licesneNumbers;
        }

        private void displayLicenseNumbers()
        {
            List<string> licesneNumbers;
           
            licesneNumbers = getLicenseNumbersListOfVehicleInTheGarageAccordingStatus();
            if (licesneNumbers.Count == 0)
            {
                Console.WriteLine("There are no vehicles in the garage!");
            }
            else
            {
                Console.WriteLine("The license numbers of the vehicles in the garage are: ");
                foreach (string number in licesneNumbers)
                {
                    Console.WriteLine("{0}", number);
                }
            }
        }

        private bool checkIfUserWantsToDisplayLicenseNumbersAccordingStatus()
        {
            bool isDisplayLicenseNumbersAccordingStatus;
            string output = @"Do you want to filter the license number's list? 
1. Yes
2. No";

            m_UIManager.HandleInputValidation("int", output, 1, 2, out m_CurrentInput);
            if(m_CurrentInput == 1)
            {
                isDisplayLicenseNumbersAccordingStatus = true;
            }
            else
            {
                isDisplayLicenseNumbersAccordingStatus = false;
            }

            return isDisplayLicenseNumbersAccordingStatus;
        }

        private eStatus getTheRequiredStatus(string i_Output)
        {
            eStatus status;

            m_UIManager.HandleInputValidation("int", i_Output, 1, 3, out m_CurrentInput);
            switch (m_CurrentInput)
            {
                case 1:
                    status = eStatus.InRepair;
                    break;
                case 2:
                    status = eStatus.Fixed;
                    break;
                default:
                    status = eStatus.Paid;
                    break;
            }

            return status;
        }

        private void changeVehicleStatus()
        {
            string output = @"Please choose one of the following options:
 1. In repair 
 2. Fixed 
 3. Paid";
            eStatus newStatus = getTheRequiredStatus(output);

            m_Garage.UpdateVehicleStatus(newStatus);
        }

        private void inflateAirInWheelsToMax()
        {
            m_Garage.InflateAirInWheels();
        }

        private void refuelVehiclePoweredByFuel()
        {
            bool isValidInput = false;
            float fuelType, fuelAmount;
            string output = @"Please choose fuel type from the following options:
1. Octan98
2. Octan96
3. Octan95
4. Soler";
            m_Garage.CheckIfVehicleInGarage();
            do
            {
                try
                {
                    m_UIManager.HandleInputValidation("int", output, 1, 4, out fuelType);
                    Console.WriteLine("Please enter fuel amount in liters: ");
                    fuelAmount = float.Parse(Console.ReadLine());
                    isValidInput = true;
                    m_Garage.RefuelVehiclePoweredByFuel((Fuel.eFuelType)fuelType, fuelAmount);
                }
                catch(FormatException foematException)
                {
                    Console.WriteLine(foematException.Message);
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            } while (!isValidInput);
        }

        private void chargeAnElectricVehicle()
        {
            bool isValidInput = false;
            float chargeAmount;

            do
            {
                try
                {
                    Console.WriteLine("Please enter battery amount in minutes: ");
                    chargeAmount = float.Parse(Console.ReadLine());
                    chargeAmount /= k_Hour;
                    isValidInput = true;
                    m_Garage.ChargeAnElectricVehicle(chargeAmount);
                }
                catch (FormatException foematException)
                {
                    Console.WriteLine(foematException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine(argumentException.Message);
                }
            } 
            while (!isValidInput);
        }

        private void displayVehicleData()
        {
            Console.WriteLine(m_Garage.GetVehicleData());
        }
    }
}