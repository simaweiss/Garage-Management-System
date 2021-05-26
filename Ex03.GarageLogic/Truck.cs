using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Truck : Vehicle
    {
        private const float k_MaxAirPressure = 26;
        private const byte k_NumOfWheels = 12;
        private float m_TrunkCapacity;
        private bool m_HaveHazardousMaterial;

        internal Truck(string i_LicenseNumber, string i_Model, Energy i_EnergyToVehicle)
            : base(i_LicenseNumber, i_Model, k_NumOfWheels, i_EnergyToVehicle)
        {
        }

        public override void SetWheelsProperty(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            initWheelsList(i_ManufacturerName, i_CurrentAirPressure, k_MaxAirPressure, k_NumOfWheels);
        }

        public float TrunkCapacity
        {
            get
            {
                return m_TrunkCapacity;
            }

            set
            {
                m_TrunkCapacity = value;
            }         
        }

        public bool HaveHazardousMaterial
        {
            get
            {
                return m_HaveHazardousMaterial;
            }

            set
            {
                m_HaveHazardousMaterial = value;
            }
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
the Truck capacity is:{1} , Has Hazardous Material:{2},",
base.ToString(),
m_TrunkCapacity,
m_HaveHazardousMaterial);
        }
    }
}
