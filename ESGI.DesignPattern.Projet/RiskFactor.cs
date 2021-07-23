using System.Collections.Generic;

namespace ESGI.DesignPattern.Projet
{
    public class RiskFactor
    {
        private RiskFactor()
        {

        }

        public static RiskFactor GetFactors()
        {
            return new RiskFactor();
        }

        public double ForRating(double riskRating)
        {
            return 0.03;
        }
    }
}
