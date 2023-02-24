﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessing
{
    public class TransformData
    {
        private NumberFormatInfo formatInfo;
        private decimal decimalBuff;
        private DateTime dataBuff;
        private long longBuff;
        private string[] splitQuotes;
        private string[] splitCommas;
        private string[] jsonInput;
        private SaveIntoJSON intoJSON;

        public TransformData()
        {
            formatInfo = new NumberFormatInfo()
            {
                NumberDecimalSeparator = ".",
            };
        }

        public bool Transform(string data)
        {
            if (!SplitString(data))
                return false;

            if (isStringCanBeParse(jsonInput[0], jsonInput[1], jsonInput[2], jsonInput[3], jsonInput[4], jsonInput[5], jsonInput[6]))
            {
                intoJSON = new SaveIntoJSON(jsonInput[0], jsonInput[1], jsonInput[2], decimalBuff, dataBuff, longBuff, jsonInput[6]);
                intoJSON.AddNewData();
                return true;
            }
            else
            {
                Console.WriteLine($"{data} not correct");
                return false;
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
                    Console.WriteLine($"{data} not correct");
                    return false;
                }

                splitQuotes[2] = splitQuotes[2].Remove(0, 1);
                splitCommas = splitQuotes[2].Split(',');

                if (splitCommas.Length != 4)
                {
                    Console.WriteLine($"{data} not correct");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{data} not correct");
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
