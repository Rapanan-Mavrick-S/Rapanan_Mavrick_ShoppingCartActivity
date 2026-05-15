using System;
using System.Collections.Generic;
using System.Text;

namespace Program
{
    class Order
    {
        private string? receiptNo;
        private DateTime date;
        private double finalTotal;
        private double payment;
        private double change;

        public string? ReceiptNo
        {
            get { return receiptNo; }
            set { receiptNo = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public double FinalTotal
        {
            get { return finalTotal; }
            set { finalTotal = value; }
        }

        public double Payment
        {
            get { return payment; }
            set { payment = value; }
        }

        public double Change
        {
            get { return change; }
            set { change = value; }
        }
    }
}