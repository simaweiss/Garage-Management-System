using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public enum eFuelType : byte
    {
        Octan98,
        Octan96,
        Octan95,
        Soler
    }

    // $G$ CSS-999 (-2) Each enum\struct\class which is non nested should be in seperate file
    public class Fuel : Energy
    {
        private readonly eFuelType r_FuelType;

        public Fuel(float i_MaxEnergyCapacity, eFuelType i_FuelType) : base(i_MaxEnergyCapacity)
        {
            r_FuelType = i_FuelType;
        }

        public override void FillEnergy(float i_AmonutOfEnergy, string i_EnergyType)
        {
            eFuelType currentFuel = (eFuelType)eFuelType.Parse(typeof(eFuelType), i_EnergyType);

            if (currentFuel == r_FuelType)
            {
                CheckingAmoutAndFillingEnergy(i_AmonutOfEnergy);
            }
            else
            {
                throw new ArgumentException("Wrong Fuel type.");
            }
        }

        public string FuelType
        {
            get
            {
                return r_FuelType.ToString();
            }
        }

        public override string EnergyType
        {
            get
            {
                return r_FuelType.ToString();
            }
        }      
    }
}