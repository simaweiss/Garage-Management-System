using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public enum eTypeOfVehicle : byte
    {
        FuelCar,
        ElectricCar,
        FuelMotorCycle,
        ElectricMotorCycle,
        FuelTruck
    }

    public abstract class Vehicle
    {
        protected readonly string r_LicenseNumber, r_Model;
        protected readonly List<Wheel> r_ListOfWheels;
        protected readonly Energy r_Energy;
        protected float m_PercentOfEnergy = 0;

        public Vehicle(string i_LicenseNumber, string i_Model, byte i_NumOfWheels, Energy i_EnergyToVehicle)
        {
            r_Model = i_Model;
            r_LicenseNumber = i_LicenseNumber;
            r_ListOfWheels = new List<Wheel>(i_NumOfWheels);
            r_Energy = i_EnergyToVehicle;
        }

        public Energy EnergySystem
        {
            get
            {
                return r_Energy;
            }
        }

        public string Model
        {
            get
            {
                return r_Model;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public float PercentOfEnergy
        {
            get
            {
                return m_PercentOfEnergy;
            }

            internal set
            {
                EnergySystem.CurrentEnergyStatus = (value / 100) * EnergySystem.MaxEnergyCapacity;
                m_PercentOfEnergy = value;
            }
        }

        public void FillEnergy(float i_AmonutOfEnergy, string i_EnergyType)
        {
            EnergySystem.FillEnergy(i_AmonutOfEnergy, i_EnergyType);
            m_PercentOfEnergy = (EnergySystem.CurrentEnergyStatus / EnergySystem.MaxEnergyCapacity) * 100;
        }

        public abstract void SetWheelsProperty(string i_ManufacturerName, float i_CurrentAirPressure);

        protected void initWheelsList(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure, byte i_NumOfWheels)
        {
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                r_ListOfWheels.Add(new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure));
            }
        }

        public void FillMaxWheelsAir()
        {
            foreach (Wheel wheel in r_ListOfWheels)
            {
                wheel.AirInflation(wheel.MaxAirPressure - wheel.CurrentAirPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder wheels = new StringBuilder();
            foreach (Wheel wheel in r_ListOfWheels)
            {
                wheels.Append(Environment.NewLine + wheel);
            }

            return string.Format(
            @"License Number:{0}  ,Model is:{1} ,the wheels are:{2}", r_LicenseNumber, r_Model, wheels);
        }
    }
}