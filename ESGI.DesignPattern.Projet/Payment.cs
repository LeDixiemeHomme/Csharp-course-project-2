using System;
using System.Collections.Generic;
using System.Text;

namespace ESGI.DesignPattern.Projet
{
    public class Payment
    {
        private double _amount;
        private DateTime _date;

        public Payment()
        {
            _amount = 0.0;
            _date = new DateTime();
        }

        public Payment(double amount, DateTime date)
        {
            Amount = amount;
            Date = date;
        }

        public double Amount
        {
            get { return _amount; }

            private set { _amount = value; }
        }

        public DateTime Date
        {
            get { return _date; }

            private set { _date = value; }
        }
    }
}
