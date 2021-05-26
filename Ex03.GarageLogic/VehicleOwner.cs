using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_OwnerName, r_PhoneNumber;
        private readonly Vehicle r_Vehicle;
        private eStatusInGarage m_StatusInGarage = eStatusInGarage.InRepair;

        internal VehicleOwner(Vehicle m_Vehicle, string i_OnwerName, string i_PhoneNumber)
        {
            r_OwnerName = i_OnwerName;
            r_PhoneNumber = i_PhoneNumber;
            r_Vehicle = m_Vehicle;
        }

        public Vehicle Vehicle
        {
            get
            {
                return r_Vehicle;
            }
        }

        public eStatusInGarage StatusInGarage
        {
            get
            {
                return m_StatusInGarage;
            }

            set
            {
                m_StatusInGarage = value;
            }
        }

        public string OwnerName => r_OwnerName;

        public string PhoneNumber => r_PhoneNumber;

        public override string ToString()
        {
            string fullVehicleOwnerDetails = string.Format(
@"VehicleOwner Details
Name: {0}
Phone number: {1}
Vehicle status: {2}
Vehicle Details
{3}",
r_OwnerName,
r_PhoneNumber,
m_StatusInGarage,
Vehicle.ToString());
            return fullVehicleOwnerDetails;
        }
    }
}