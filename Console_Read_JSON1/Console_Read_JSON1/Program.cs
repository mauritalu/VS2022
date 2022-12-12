using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Collections;
using static Console_Read_JSON1.Program;
using System.Management.Instrumentation;

namespace Console_Read_JSON1
{
    internal class Program
    {

        static string fileName = "c:\\Temp\\cat.json";

        static void Main(string[] args)
        {
            string jsonFile     = File.ReadAllText(fileName);
            dynamic jsonFileRes = JObject.Parse(jsonFile);

            //Console.WriteLine(jsonFileRes.response["getCatData"]["getCatData"]);

            var catDataString = jsonFileRes["response"]["getCatData"]["getCatData"].ToString();
            var catDataList   = JsonConvert.DeserializeObject<List<CatDataDetails>>(catDataString);
            var success       = bool.Parse(jsonFileRes["response"]["success"].ToString());
            var successMsg    = jsonFileRes["response"]["successmessage"].ToString();

            var model = new Response
            {
                Success        = success,
                SuccessMessage = successMsg,
                CatData        = catDataList
            };

            Console.WriteLine(model);

            Console.WriteLine(model.Success + ": " + model.SuccessMessage + " " + model.CatData.Count);


            for (int i = 0; i < model.CatData.Count; i++)
            {
                
                Console.WriteLine(model.CatData[i].BankName);

                Console.WriteLine(model.Success.ToString());
            }


            //foreach (var uus in model.CatData) 
            //{ 
            //    //Console.WriteLine(uus.kundnr);

            //    Console.WriteLine(string.Join(",", uus.BankName.ToList()));
            //}



           

        }



        public class CatDataDetails
        {
            public string BankName { get; set; }
            public string Persnr { get; set; }
            public int kundnr { get; set; }
            public double ExpectedAmount { get; set; }
        }

        public class Response
        {
            public bool Success { get; set; }
            public string SuccessMessage { get; set; }
            public List<CatDataDetails> CatData { get; set; }
        }
    }
}
