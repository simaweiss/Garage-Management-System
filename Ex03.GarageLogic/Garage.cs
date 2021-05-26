using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    // $G$ CSS-999 (-5) Each enum\struct\class which is non nested should be in seperate file
    public enum eStatusInGarage
    {
        InRepair,
        DoneRepair,
        Paid
    }

    public class Garage
    {
        private readonly Dictionary<string, VehicleOwner> r_WorkCards;

        public Garage()
        {
            r_WorkCards = new Dictionary<string, VehicleOwner>();
        }

        public bool IsAlreadyInGarage(string i_LicenceNum)
        {
            bool vehicleExistsInGarage = false;
            vehicleExistsInGarage = r_WorkCards.ContainsKey(i_LicenceNum);

            VehicleOwner vehicleOwner;
            if (vehicleExistsInGarage)
            {
                r_WorkCards.TryGetValue(i_LicenceNum, out vehicleOwner);
                vehicleOwner.StatusInGarage = eStatusInGarage.InRepair;
                vehicleExistsInGarage = true;
            }

            return vehicleExistsInGarage;
        }

        public void ChargeBattary(string i_LicenseNum, float i_MinuteToCharge)
        {
            const string electric = "Electric";

            if (!r_WorkCards.TryGetValue(i_LicenseNum, out VehicleOwner VehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            if (VehicleOwner.Vehicle.EnergySystem.EnergyType == electric)
            {
                VehicleOwner.Vehicle.FillEnergy(i_MinuteToCharge, electric);
            }
            else
            {
                throw new Exception("Only electric");
            }
        }

        public void RefuelVehicle(string i_LicenseNum, float i_FuelAmount, eFuelType i_FuelType)
        {
            if (!Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                throw new Exception("Not available Fuel");
            }

            if (!r_WorkCards.TryGetValue(i_LicenseNum, out VehicleOwner vehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            vehicleOwner.Vehicle.FillEnergy(i_FuelAmount, i_FuelType.ToString());
        }

        public void FillMaxWheelsAir(string i_LicenseNum)
        {
            if (!r_WorkCards.TryGetValue(i_LicenseNum, out VehicleOwner VehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            VehicleOwner.Vehicle.FillMaxWheelsAir();
        }

        public void ChangeVehicleStatus(string i_LicenseNum, eStatusInGarage i_NewStatus)
        {
            if (!r_WorkCards.TryGetValue(i_LicenseNum, out VehicleOwner VehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            VehicleOwner.StatusInGarage = i_NewStatus;
        }

        public string MsgFullDetailsVehicle(string i_LicenseNum)
        {
            if (!r_WorkCards.TryGetValue(i_LicenseNum, out VehicleOwner VehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            return VehicleOwner.ToString();
        }

        public void ChargeElecticVehicle(string i_LicenseNum, float i_BattaryTime)
        {
            Vehicle vehicle = getCurretVehicle(i_LicenseNum);
        }

        public void InsertNewVehicleOwner(Vehicle i_NewVehicle, string i_VehicleOwnerName, string i_VehicleOwnerPhoneNumber)
        {
            VehicleOwner newVehicleOwner = new VehicleOwner(i_NewVehicle, i_VehicleOwnerName, i_VehicleOwnerPhoneNumber);
            r_WorkCards.Add(newVehicleOwner.Vehicle.LicenseNumber, newVehicleOwner);
        }

        private Vehicle getCurretVehicle(string i_CurrentKeyNumber)
        {
            VehicleOwner VehicleOwner = null;
            if (!r_WorkCards.TryGetValue(i_CurrentKeyNumber, out VehicleOwner))
            {
                throw new Exception("Vehicle doesn't exist");
            }

            return VehicleOwner.Vehicle;
        }

        public string MsgOfAllVehicleInGarageFillterStatus(bool i_OrderByStatus)
        {
            string msgAllVehicles;

            msgAllVehicles = i_OrderByStatus ? msgOfAllByStatus() : msgOfAllVehicleInGarage();

            return msgAllVehicles;
        }

        private string msgOfAllVehicleInGarage()
        {
            StringBuilder vehicles = new StringBuilder(120);
            vehicles.AppendLine("Vehicles in garage");
            foreach (VehicleOwner vehicleOwner in r_WorkCards.Values)
            {
                vehicles.AppendLine(vehicleOwner.Vehicle.LicenseNumber);
            }

            return vehicles.ToString();
        }

        private string msgOfAllByStatus()
        {
            string newLine = Environment.NewLine;
            StringBuilder vehicleInRepair = new StringBuilder(string.Format("== Vehicles in repair =={0}", newLine), 60);
            StringBuilder vehicleDoneRepair = new StringBuilder(string.Format("{0}== Vehicles done repair =={0}", newLine), 60);
            StringBuilder vehiclePaid = new StringBuilder(string.Format("{0}== Vehicles paid =={0}", newLine), 60);

            foreach (VehicleOwner vehicleOwner in r_WorkCards.Values)
            {
                if (vehicleOwner.StatusInGarage == eStatusInGarage.InRepair)
                {
                    vehicleInRepair.AppendLine(vehicleOwner.Vehicle.LicenseNumber);
                }
                else if (vehicleOwner.StatusInGarage == eStatusInGarage.DoneRepair)
                {
                    vehicleDoneRepair.AppendLine(vehicleOwner.Vehicle.LicenseNumber);
                }
                else
                {
                    vehiclePaid.AppendLine(vehicleOwner.Vehicle.LicenseNumber);
                }
            }

            vehicleInRepair.AppendFormat("{0}{1}", vehicleDoneRepair, vehiclePaid);
            return vehicleInRepair.ToString();
        }
    }
}