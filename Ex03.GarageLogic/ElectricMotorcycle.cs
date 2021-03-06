using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxHoursBattery = 1.4f;

        public ElectricMotorcycle(string i_LicenseNumber, string i_Model)
            : base(i_LicenseNumber, i_Model, new Electricity(k_MaxHoursBattery))
        {
        }

        public override string ToString()
        {
            return string.Format(
 @"{0}
Battery status:{1}%",
 base.ToString(),
 m_PercentOfEnergy);
        }
    }
}
