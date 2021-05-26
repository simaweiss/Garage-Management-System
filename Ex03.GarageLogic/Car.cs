using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        private const float k_MaxAirPressure = 31;
        private const byte k_NumOfWheels = 4;
        protected byte m_DoorsCount;
        protected eCarColor m_CarColor;

        public enum eCarColor : byte
        {
            Red, 
            Blue, 
            Black, 
            Gray
        }

        public Car(string i_LicenseNumber, string i_Model, Energy i_EngineToVehicle)
            : base(i_LicenseNumber, i_Model, k_NumOfWheels, i_EngineToVehicle)
        {
        }

        public override void SetWheelsProperty(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            initWheelsList(i_ManufacturerName, i_CurrentAirPressure, k_MaxAirPressure, k_NumOfWheels);
        }

        public byte DoorsCount
        {
            get
            {
                return m_DoorsCount;
            }

            internal set
            {
                if (value >= 2 && value <= 5)
                {
                    m_DoorsCount = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(null, 5, 2);
                }
            }
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
the car color is:{1} , the count of doors is:{2},",
base.ToString(),
m_CarColor,
m_DoorsCount);
        }
    }
}
