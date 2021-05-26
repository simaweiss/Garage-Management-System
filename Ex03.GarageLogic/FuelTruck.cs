using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelTruck : Truck 
    {
        public FuelTruck(string i_LicenseNumber, string i_Model)
            : base(i_LicenseNumber, i_Model, new Fuel(110f, eFuelType.Soler))
        {
        }

        public override string ToString()
        {
            return string.Format(
@"{0}
type of fule is:{1}  , Fuel status is:{2}%",
base.ToString(),
eFuelType.Soler,
PercentOfEnergy);
        }
    }
}