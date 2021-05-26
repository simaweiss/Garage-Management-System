using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Electricity : Energy
    {
        private const string k_Electric = "Electric";

        public Electricity(float i_MaxEnergyCapacity) : base(i_MaxEnergyCapacity)
        {
        }

        public override string EnergyType
        {
            get
            {
                return k_Electric;
            }
        }

        public override void FillEnergy(float i_AmonutOfEnergy, string i_EnergyType)
        {
            const string Electric = "Electric"; 

            if (Electric == i_EnergyType)
            {
                CheckingAmoutAndFillingEnergy(i_AmonutOfEnergy);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
