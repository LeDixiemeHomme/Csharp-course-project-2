﻿using System;
using System.Collections.Generic;

namespace ESGI.DesignPattern.Projet
{
    public class Loan
    {
        protected double _commitment;
        IList<Payment> _payments = new List<Payment>();
        private DateTime _start = DateTime.Now;

        public Loan(
            double commitment,
            DateTime start
            )
        {
            this._commitment = commitment;
            this._start = start;
        }

        public void Payment(
            double amount,
            DateTime paymentDate)
        {
            _payments.Add(new Payment(amount, paymentDate));
        }

        protected double WeightedAverageDuration()
        {
            double weightedAverage = 0.0;
            double sumOfPayments = 0.0;

            if (_commitment == 0.0) return 0.0;

            foreach (var payment in _payments)
            {
                sumOfPayments += payment.Amount;
                weightedAverage += YearsTo(payment.Date) * payment.Amount;
            }

            return weightedAverage / sumOfPayments;
        }

        protected double YearsTo(DateTime? endDate)
        {
            return (double) ((endDate?.Ticks - _start.Ticks) / Constant.MILLIS_PER_DAY / Constant.DAYS_PER_YEAR);
        }
    }

    public interface INewLoan
    {
        double Duration();
        double Capital();
        void Payment(double amount, DateTime paymentDate);
    }

    public class NewTermNewLoan : Loan, INewLoan
    {
        private const double RISK_FACTOR = 0.03;
        private DateTime? _maturity;
        public NewTermNewLoan(double commitment, DateTime start, DateTime end) : base(commitment, start)
        {
            this._maturity = end;
        }
        
        public double Duration()
        {
            return _maturity != null ? WeightedAverageDuration() : 0.0;
        }

        public double Capital()
        {
            if (_maturity != null)
                return _commitment * Duration() * RISK_FACTOR;
            return 0.0;
        }
    }

    public class NewRevolver : Loan, INewLoan
    {
        private DateTime? _expiry;
        private const double RISK_FACTOR = 0.01;
        
        public NewRevolver(double commitment, DateTime start, DateTime end) : base(commitment, start)
        {
            this._expiry = end;
        }
        
        public double Duration()
        {
            return _expiry != null ? YearsTo(_expiry) : 0.0;
        }

        public double Capital()
        {
            if (_expiry != null)
                return _commitment * Duration() * RISK_FACTOR;
            return 0.0;
        }
    }

    public class NewAdvisedLine : Loan, INewLoan
    {
        private DateTime? _expiry;
        private const double RISK_FACTOR = 0.03;
        private const double PERCENTAGE = 0.1;
        public NewAdvisedLine(double commitment, DateTime start, DateTime end) : base(commitment, start)
        {
            this._expiry = end;
        }
        
        public double Duration()
        {
            return _expiry != null ? YearsTo(_expiry) : 0.0;
        }

        public double Capital()
        {
            if (_expiry != null)
                return _commitment * PERCENTAGE * Duration() * RISK_FACTOR;

            return 0.0;
        }
    }
}