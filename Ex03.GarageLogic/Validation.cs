using System;

namespace Ex03.GarageLogic
{
    public class Validation
    {
        public void CheckIfChoiceIsInRange(float i_Input, float i_MinValue, float i_MaxValue, out bool io_IsLegal)
        {
            io_IsLegal = (i_Input >= i_MinValue) && (i_Input <= i_MaxValue);
            if (!io_IsLegal)
            {
                throw new ValueOutOfRangeException(i_MinValue, i_MaxValue);
            }
        }

        public void CheckIfNumberIsInFormat(string i_OwnerPhoneNumber, out bool io_IsInFormat)
        {
            io_IsInFormat = checkIfStringContainsOnlyDigits(i_OwnerPhoneNumber);
            if (!io_IsInFormat)
            {
                throw new FormatException("Invalid input! Please enter only digits.");
            }
        }

        public void CheckIfNameIsInFormat(string i_OwnerName, out bool io_IsInFormat)
        {
            io_IsInFormat = checkIfStringContainsOnlyLetters(i_OwnerName);
            if (!io_IsInFormat)
            {
                throw new FormatException("Invalid input! Please enter only letters.");
            }
        }

        private bool checkIfStringContainsOnlyDigits(string i_String)
        {
            bool isStringContainsOnlyDigits = true;

            foreach (char digit in i_String)
            {
                if (!char.IsDigit(digit))
                {
                    isStringContainsOnlyDigits = false;
                }
            }

            return isStringContainsOnlyDigits;
        }

        private bool checkIfStringContainsOnlyLetters(string i_String)
        {
            bool isStringContainsOnlyLetters = true;

            foreach (char digit in i_String)
            {
                if (!char.IsLetter(digit))
                {
                    isStringContainsOnlyLetters = false;
                }
            }

            return isStringContainsOnlyLetters;
        }
    }
}
