using System;

namespace ESGI.DesignPattern.Projet
{
    public static class Constant
    {
        public static long MILLIS_PER_DAY = 86400000;
        public static long DAYS_PER_YEAR = DaysPerYear();

        private static long DaysPerYear()
        {
            var date = DateTime.Now;
            var year = date.Year;
            
            if (year % 4 == 0 && year % 100 != 0 )
            {
                return 366;
            }
            return 365;
        }
    }
}