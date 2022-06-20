using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0;
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        internal void InflateWheel(float i_AirPressureToInflate)
        {
            float newAirPressure = m_CurrentAirPressure + i_AirPressureToInflate;

            if (newAirPressure <= r_MaxAirPressure)
            {
                m_CurrentAirPressure = newAirPressure;
            }
            else
            {
                throw new ValueOutOfRangeException(0, MaxAirPressure - CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            return string.Format("Manufacturer name: {0}{1}Current air pressure is {2} out of {3}{4}",
                r_ManufacturerName, Environment.NewLine, m_CurrentAirPressure, MaxAirPressure, Environment.NewLine);
        }
    }
}
