using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class PaymentData
    {
        public string city { get; set; }
        public List<Service> services { get; set; }
        public decimal total { get; set; }
    }
    public class Payer
    {
        public string name { get; set; }
        public decimal payment { get; set; }
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime date { get; set; }
        public long account_number { get; set; }
    }
    public class Service
    {
        public string name { get; set; }
        public List<Payer> payers { get; set; }
        public decimal total { get; set; }
    }

    public class OnlyDateConverter : IsoDateTimeConverter
    {
        public OnlyDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
