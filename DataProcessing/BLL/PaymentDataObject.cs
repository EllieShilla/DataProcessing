using System;
using System.Collections.Generic;

namespace DataProcessing.BLL
{
    public static class PaymentDataObject
    {
        public static PaymentData CreatePaymentData(string firstNamem, string lastName, string city, decimal payment, DateTime date, long account, string title)
        {
            return new PaymentData
            {
                city = city,
                services = new List<Service>()
                {
                    new Service
                    {
                        name=title,
                        payers=new List<Payer>()
                        {
                            new Payer
                            {
                                name=string.Concat(firstNamem, " ", lastName),
                                payment=payment,
                                date=date,
                                account_number=account
                            }
                        },
                        total=payment,
                    }
                },
                total = payment
            };
        }
    }
}
