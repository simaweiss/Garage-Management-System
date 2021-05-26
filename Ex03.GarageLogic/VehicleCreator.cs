using System;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        public static Vehicle CreateVehicle(eTypeOfVehicle i_ChonsenVehicle, string i_LicenceNum, string i_Model)
        {
            Vehicle newVehicle = null;
            switch (i_ChonsenVehicle)
            {
                case eTypeOfVehicle.FuelCar:
                    newVehicle = new FuelCar(i_LicenceNum, i_Model);
                    break;
                case eTypeOfVehicle.ElectricCar:
                    newVehicle = new ElectricCar(i_LicenceNum, i_Model);
                    break;
                case eTypeOfVehicle.FuelMotorCycle:
                    newVehicle = new FuelMotorcycle(i_LicenceNum, i_Model);
                    break;
                case eTypeOfVehicle.ElectricMotorCycle:
                    newVehicle = new ElectricMotorcycle(i_LicenceNum, i_Model);
                    break;
                case eTypeOfVehicle.FuelTruck:
                    newVehicle = new FuelTruck(i_LicenceNum, i_Model);
                    break;
            }

            return newVehicle;
        }

        public static void VehicleWheelsProperty(Vehicle i_Vehicle, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            i_Vehicle.SetWheelsProperty(i_ManufacturerName, i_CurrentAirPressure);
        }

        public static void VehicleEnergyProperty(Vehicle i_Vehicle, float i_CurrentPercentEnergy)
        {
            i_Vehicle.PercentOfEnergy = i_CurrentPercentEnergy;
        }

        public static void CarProperties(Vehicle i_Car, byte i_NumOfDoors, Car.eCarColor i_CarColor)
        {
            Car car = i_Car as Car;

            if (car != null)
            {
                car.DoorsCount = i_NumOfDoors;
                car.CarColor = i_CarColor;
            }
            else
            {
                throw new Exception();
            }
        }

        public static void MotorcyleProperties(Vehicle i_Motorcycle, int i_EngineCapacity, Motorcycle.eTypeOfLicense i_TypeOfLicense)
        {
            Motorcycle motorcycle = i_Motorcycle as Motorcycle;
            if (motorcycle != null)
            {
                motorcycle.EngineCapacity = i_EngineCapacity;
                motorcycle.TypeOfLicense = i_TypeOfLicense;
            }
            else
            {
                throw new Exception();
            }
        }

        public static void TruckProperties(Vehicle i_Truck, float i_TrunkCapacity, bool i_HaveHazardousMaterial)
        {
            FuelTruck truck = i_Truck as FuelTruck;

            if (truck != null)
            {
                truck.HaveHazardousMaterial = i_HaveHazardousMaterial;
                truck.TrunkCapacity = i_TrunkCapacity;
            }
            else
            {
                throw new Exception();
            }
        }
    }
}