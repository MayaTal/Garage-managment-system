using System;

namespace Ex03.GarageLogic
{
    public class Owner
    {
        public const int k_OwnerPhoneNumberLength = 10;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;

        public Owner(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }

            set
            {
                m_OwnerName = value;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return m_OwnerPhoneNumber;
            }

            set
            {
                m_OwnerPhoneNumber = value;
            }
        }

        public override string ToString()
        {
            return string.Format("Owner name: {0}{1}Phone number: {2}{3}", m_OwnerName, Environment.NewLine, m_OwnerPhoneNumber, Environment.NewLine);
        }
    }
}
