using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing.BLL
{
    internal class PaymentDataList
    {
        private List<PaymentData> dataList;
        private List<PaymentData> paymentsDataList;

        public PaymentDataList(List<PaymentData> data)
        {
            this.dataList = data;
            paymentsDataList = new List<PaymentData>();
        }

        public void CreatePaymentDataList()
        {
            foreach (var item in dataList)
            {
                AddNewData(item);
            }
        }

        public List<PaymentData> ReturnPaymentDataList()
        {
            return paymentsDataList;
        }

        private void AddNewData(PaymentData data)
        {
            if (paymentsDataList.Exists(i => i.city.Equals(data.city)) == false)
            {
                PaymentData paymentData = new PaymentData();

                paymentData.city = data.city;
                paymentData.services = new List<Service>()
                {
                    data.services[0]
                };
                paymentData.total = data.services[0].payers[0].payment;
                paymentsDataList.Add(paymentData);
            }
            else
            {
                if (paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).services.Exists(i => i.name.Equals(data.services[0].name)) == false)
                {
                    Service service = data.services[0];

                    paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).services.Add(service);
                    paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).total += data.services[0].payers[0].payment;
                }
                else
                {
                    Payer payer = data.services[0].payers[0];
                    paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).services.FirstOrDefault(i => i.name.Equals(data.services[0].name)).payers.Add(payer);

                    decimal total = paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).services
                                                   .FirstOrDefault(x => x.name.Equals(data.services[0].name)).total += data.services[0].payers[0].payment;

                    paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).services.FirstOrDefault(i => i.name.Equals(data.services[0].name)).total = total;
                    paymentsDataList.FirstOrDefault(i => i.city.Equals(data.city)).total += data.services[0].payers[0].payment;
                }
            }
        }
    }
}
