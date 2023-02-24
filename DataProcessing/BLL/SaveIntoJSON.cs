using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataProcessing
{
    public class SaveIntoJSON
    {
        private List<PaymentData> paymentDataList;
        private readonly string fileName = $"{System.Configuration.ConfigurationManager.AppSettings["folderPathB"]}/output.json";
        private string firstNamem;
        private string lastName;
        private string city;
        private decimal payment;
        private DateTime date;
        private long account;
        private string title;

        public SaveIntoJSON(string firstNamem, string lastName, string city, decimal payment, DateTime date, long account, string title)
        {
            paymentDataList = new List<PaymentData>();
            this.firstNamem = firstNamem;
            this.lastName = lastName;
            this.city = city;
            this.payment = payment;
            this.date = date;
            this.account = account;
            this.title = title;
        }

        public void AddNewData()
        {
            GetDataFromJson();

            if (paymentDataList.Exists(i => i.city.Equals(city)) == false)
            {
                PaymentData paymentData = new PaymentData();

                paymentData.city = city;
                paymentData.services = new List<Service>()
                {
                    CreateService()
                };
                paymentData.total = payment;
                paymentDataList.Add(paymentData);
            }
            else
            {
                if (paymentDataList.FirstOrDefault(i => i.city.Equals(city)).services.Exists(i => i.name.Equals(title)) == false)
                {
                    Service service = CreateService();

                    paymentDataList.FirstOrDefault(i => i.city.Equals(city)).services.Add(service);
                    paymentDataList.FirstOrDefault(i => i.city.Equals(city)).total += payment;
                }
                else
                {
                    Payer payer = CreatePayer();
                    paymentDataList.FirstOrDefault(i => i.city.Equals(city)).services.FirstOrDefault(i => i.name.Equals(title)).payers.Add(payer);

                    decimal total = paymentDataList.FirstOrDefault(i => i.city.Equals(city)).services
                                                   .FirstOrDefault(x => x.name.Equals(title)).total += payment;

                    paymentDataList.FirstOrDefault(i => i.city.Equals(city)).services.FirstOrDefault(i => i.name.Equals(title)).total = total;
                    paymentDataList.FirstOrDefault(i => i.city.Equals(city)).total += payment;
                }
            }

            JsonSerializerAndSave();
        }

        private void GetDataFromJson()
        {
            if (File.Exists(fileName))
                paymentDataList.AddRange(JsonConvert.DeserializeObject<List<PaymentData>>(File.ReadAllText(fileName)));
        }
        private void JsonSerializerAndSave()
        {
            var updatedJsonString = JsonConvert.SerializeObject(paymentDataList);
            File.WriteAllText(fileName, updatedJsonString);
        }

        private Payer CreatePayer()
        {
            return new Payer()
            {
                name = string.Concat(firstNamem, " ", lastName),
                payment = payment,
                date = date,
                account_number = account
            };
        }

        private Service CreateService()
        {
            return new Service()
            {
                name = title,
                payers = new List<Payer>()
                        {
                            CreatePayer()
                        },
                total = payment
            };
        }
    }
}
