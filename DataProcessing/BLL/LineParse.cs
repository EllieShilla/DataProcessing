using DataProcessing.BLL;
using DataProcessing.BLL.Log;
using Serilog;
using System;
using System.Globalization;

namespace DataProcessing
{
    public class LineParse
    {
        private NumberFormatInfo formatInfo;
        private decimal decimalBuff;
        private DateTime dataBuff;
        private long longBuff;
        private string[] splitQuotes;
        private string[] splitCommas;
        private string[] jsonInput;

        public LineParse()
        {
            formatInfo = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ".",
            };
        }

        public PaymentData ParsingData(string data)
        {
            if (!SplitString(data))
                return null;

            if (isStringCanBeParse(jsonInput[0], jsonInput[1], jsonInput[2], jsonInput[3], jsonInput[4], jsonInput[5], jsonInput[6]))
            {
                return PaymentDataObject.CreatePaymentData(jsonInput[0], jsonInput[1], jsonInput[2], decimalBuff, dataBuff, longBuff, jsonInput[6]);
            }
            else
            {
                return null;
            }
        }

        private bool SplitString(string data)
        {
            splitQuotes = new string[3];
            splitCommas = new string[4];

            try
            {
                splitQuotes = data.Split('“', '”');

                if (splitQuotes.Length != 3)
                {
                    return false;
                }

                splitQuotes[2] = splitQuotes[2].Remove(0, 1);
                splitCommas = splitQuotes[2].Split(',');

                if (splitCommas.Length != 4)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MetaLog.FoundErrors();
                Log.Error($"Exception: {ex.Message} \nPlace of occurrence: {ex.TargetSite}");
            }

            jsonInput = new string[] {
                splitQuotes[0].Split(',')[0],
                splitQuotes[0].Split(',')[1],
                splitQuotes[1].Split(',')[0],
                splitCommas[0],
                splitCommas[1].Trim(),
                splitCommas[2],
                splitCommas[3]
            };

            return true;
        }

        private bool isStringCanBeParse(string firstName, string lastname, string city, string payment, string data, string code, string title)
        {
            if (String.IsNullOrEmpty(firstName))
                return false;

            if (String.IsNullOrEmpty(lastname))
                return false;

            if (String.IsNullOrEmpty(city))
                return false;

            if (decimal.TryParse(payment, NumberStyles.Number, formatInfo, out decimalBuff) == false)
                return false;

            if (DateTime.TryParseExact(data, "yyyy-dd-MM", null,
                                      DateTimeStyles.None, out dataBuff) == false)
                return false;

            if (long.TryParse(code, out longBuff) == false)
                return false;

            if (String.IsNullOrEmpty(title))
                return false;

            return true;
        }
    }
}
