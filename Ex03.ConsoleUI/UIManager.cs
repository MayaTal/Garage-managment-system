using System;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private Validation m_Validation;

        public UIManager()
        {
            m_Validation = new Validation();
        }

        internal void GetNumber(out string io_Number, string i_Output, float i_MinNumber, float i_MaxNumber)
        {
            bool isValidInput = false;

            do
            {
                Console.WriteLine(i_Output);
                io_Number = Console.ReadLine();
                try
                {
                    m_Validation.CheckIfNumberIsInFormat(io_Number, out isValidInput);
                    m_Validation.CheckIfChoiceIsInRange(io_Number.Length, i_MinNumber, i_MaxNumber, out isValidInput);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
            }
            while (!isValidInput);
        }

        internal void GetName(out string io_Name, string i_Output)
        {
            bool isValidInput = false;

            do
            {
                Console.WriteLine(i_Output);
                io_Name = Console.ReadLine();
                try
                {
                    m_Validation.CheckIfNameIsInFormat(io_Name, out isValidInput);
                    m_Validation.CheckIfChoiceIsInRange(io_Name.Length, 1, Factory.k_MaxNameLength, out isValidInput);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
            }
            while (!isValidInput);
        }

        internal void HandleInputValidation(string i_InputType, string i_Output, float i_MinValue, float i_MaxValue, out float io_Input)
        {
            bool isValidInput = false;

            io_Input = 0;
            do
            {
                Console.WriteLine(i_Output);
                try
                {
                    if (i_InputType == "int")
                    {
                        io_Input = int.Parse(Console.ReadLine());
                    }
                    else
                    {
                        io_Input = float.Parse(Console.ReadLine());
                    }

                    isValidInput = true;
                    m_Validation.CheckIfChoiceIsInRange(io_Input, i_MinValue, i_MaxValue, out isValidInput);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    Console.WriteLine(valueOutOfRangeException.Message);
                }
            }
            while (!isValidInput);
        }

        internal StringBuilder CreateScreenOutputAccordingVehicleType(string i_Output)
        {
            StringBuilder screenOutput = new StringBuilder(i_Output);
            string[] currentVehicleType;
            int currentTypeNumber = 1;

            screenOutput.Append(Environment.NewLine);
            foreach (string type in Enum.GetNames(typeof(Factory.eVehicleType)))
            {
                currentVehicleType = type.Split('_');
                screenOutput.Append(currentTypeNumber + ". ");
                for (int currentString = 0; currentString < currentVehicleType.Length; ++currentString)
                {
                    screenOutput.Append(currentVehicleType[currentString] + " ");
                }

                screenOutput.Append(Environment.NewLine);
                currentTypeNumber++;
            }

            return screenOutput;
        }

        internal void GetSpecificCharacteristicsAccordingVehicleType(VehicleData i_Object)
        {
            string input;
            int currentUtil = 0;
            bool isValidInput = false;

            Console.WriteLine("Please enter the following utils.");
            foreach (string utilOutput in i_Object.Vehicle.OutputUtils)
            {
                do
                {
                    Console.WriteLine(utilOutput);
                    try
                    {
                        input = Console.ReadLine();
                        i_Object.Vehicle.CheckUtilsValidationAccordingUserInputAndUpdate(input, currentUtil, out isValidInput);
                    }
                    catch (FormatException formatException)
                    {
                        Console.WriteLine(formatException.Message);
                    }
                    catch (ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.WriteLine(valueOutOfRangeException.Message);
                    }
                    catch(ArgumentException argumentException)
                    {
                        Console.WriteLine(argumentException.Message);
                    }
                }
                while (!isValidInput);
                currentUtil++;
            }
        }
    }
}
