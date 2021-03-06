using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Energy
    {
        protected readonly float r_MaxEnergyCapacity;
        protected float m_CurrentEnergyStatus = 0;

        protected Energy(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
        }

        public float MaxEnergyCapacity
        {
            get
            {
                return r_MaxEnergyCapacity;
            }
        }

        public float CurrentEnergyStatus
        {
            get
            {
                return m_CurrentEnergyStatus;
            }

            set
            {
                if (value >= 0 && value <= r_MaxEnergyCapacity)
                {
                    m_CurrentEnergyStatus = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(null, r_MaxEnergyCapacity, 0);
                }
            }
        }

        public abstract void FillEnergy(float i_AmonutOfEnergy, string i_EnergyType);

        public abstract string EnergyType
        {
            get;
        }

        protected void CheckingAmoutAndFillingEnergy(float i_AmonutOfEnergy)
        {
            if (m_CurrentEnergyStatus + i_AmonutOfEnergy <= r_MaxEnergyCapacity)
            {
                m_CurrentEnergyStatus += i_AmonutOfEnergy;
            }
            else
            {
                throw new ValueOutOfRangeException(null, r_MaxEnergyCapacity - m_CurrentEnergyStatus, 0);
            }
        }
    }
}