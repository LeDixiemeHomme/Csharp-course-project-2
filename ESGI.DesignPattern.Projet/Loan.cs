using System;
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
            if (endDate != null)
            {
                return (double) ((endDate.Value.Ticks - _start.Ticks) / Constant.MILLIS_PER_DAY / Constant.DAYS_PER_YEAR);
            }
            return 0;
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
        private const double RiskFactor = Constant.RISK_FACTOR_TERM_NEW_LOAN;
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
                return _commitment * Duration() * RiskFactor;
            return 0.0;
        }
    }

    public class NewRevolver : Loan, INewLoan
    {
        private DateTime? _expiry;
        private const double RiskFactor = Constant.RISK_FACTOR_REVOLVER;
        
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
                return _commitment * Duration() * RiskFactor;
            return 0.0;
        }
    }

    public class NewAdvisedLine : Loan, INewLoan
    {
        private DateTime? _expiry;
        private const double RiskFactor = Constant.RISK_FACTOR_ADVISED_LINE;
        private const double Percentage = 0.1;
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
                return _commitment * Percentage * Duration() * RiskFactor;

            return 0.0;
        }
    }
}