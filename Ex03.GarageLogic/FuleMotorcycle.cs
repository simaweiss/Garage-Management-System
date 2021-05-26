using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelTank = 8f;

        public FuelMotorcycle(string i_LicenseNumber, string i_Model)
            : base(i_LicenseNumber, i_Model, new Fuel(k_MaxFuelTank, eFuelType.Octan95))
        {
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
type of fule is:{1}  , Fuel status is:{2}%",
base.ToString(),
eFuelType.Octan95,
m_PercentOfEnergy);
        }
    }
}
