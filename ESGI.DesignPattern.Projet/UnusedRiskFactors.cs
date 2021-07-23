using System;
using System.Collections.Generic;
using System.Text;

namespace ESGI.DesignPattern.Projet
{
    public class UnusedRiskFactors
    {
        private UnusedRiskFactors()
        {

        }

        public static UnusedRiskFactors GetFactors()
        {
            return new UnusedRiskFactors();
        }

        public double ForRating(double riskRating)
        {
            return 0.01;
        }
    }
}
