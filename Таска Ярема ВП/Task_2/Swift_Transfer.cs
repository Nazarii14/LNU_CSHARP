using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Swift_Transfer
    {
        private string _id;
        private string _iban;
        private string _account_holder;
        private double _amount;
        private double _fee_amount;
        private string _currency;
        private string _date;

        public string ID
        {
            get { return _id; }
            set { _id = Validation.ValidateNumber(value); }
        }
        public string IBAN
        {
            get { return _iban; }
            set { _iban = Validation.ValidateIBAN(value); }
        }
        public string AccountHolder
        {
            get { return _account_holder; }
            set { _account_holder = Validation.ValidateStr(value); }
        }
        public double Amount
        {
            get { return _amount; }
            set { _amount = Validation.ValidateAmount(value); }
        }
        public double FeeAmount
        {
            get { return _fee_amount; }
            set { _fee_amount = Validation.ValidateAmount(value); }
        }
        public string Currency
        {
            get { return _currency; }
            set { _currency = Validation.ValidateCurrency(value); }
        }
        public string Date
        {
            get { return _date; }
            set { _date = Validation.ValidateDate(value); }
        }
        public string print()
        {
            return ($"ID: {ID} \nIBAN: {IBAN} \nAccountHolder: {AccountHolder} \nAmount: {Amount} \nFeeAmount: {Currency} " +
                $"\nCurrency: {FeeAmount} \nDate: {Date}\n");
        }
        public void input()
        {
            Console.Write("ID: ");
            ID = Console.ReadLine();
            Console.Write("IBAN: ");
            IBAN = Console.ReadLine();
            Console.Write("AccountHolder: ");
            AccountHolder = Console.ReadLine();
            Console.Write("Amount: ");
            Amount = double.Parse(Console.ReadLine());
            Console.Write("FeeAmount: ");
            FeeAmount = double.Parse(Console.ReadLine());
            Console.Write("Currency: ");
            Currency = Console.ReadLine();
            Console.Write("Date: ");
            Date = Console.ReadLine();
        }
    }
}
