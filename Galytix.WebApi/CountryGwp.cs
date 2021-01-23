using Galytix.WebApi.Models;
using System;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Galytix.WebApi
{
    public class CountryGwp : ICountryGwp
    {
        public Dictionary<string, string> GWP(string country, string[] lob)
        {
            decimal value;
            var gwpValues = new Dictionary<string, string>();
            var results = GetCountriesData(country, lob);
            results = results.Where(x => x.Country == country && lob.Contains(x.lineOfBusiness)).ToList();
            foreach (var result in results)
            {
                value = 0;
                result.YearGwpValue.Skip(8).ToList().ForEach(gwp =>
                {
                    decimal.TryParse(gwp, out decimal parsedValue);
                    value += parsedValue;
                });
                gwpValues.Add(result.lineOfBusiness, (value/8).ToString());
            }

            return gwpValues;
        }


        public virtual List<GwpModel> GetCountriesData(string country, string[] lob)
        {
            var contents = File.ReadAllText(".././../../Data/gwpByCountry.csv").Split('\n').ToList();
            var models = contents.Select(line => line.Split(','));
            var results = new List<GwpModel>();
            models = models.Skip(1).ToList();
            foreach (var row in models)
            {
                if (row.Length > 10)
                {

                    results.Add(new GwpModel
                    {
                        Country = row[0],
                        variableId = row[1],
                        variableName = row[2],
                        lineOfBusiness = row[3],
                        YearGwpValue = row.Length > 4 ? row.Skip(4).TakeWhile(x => !x.Equals("\r")).ToList() : new List<string>(),
                    });
                }
            }
            return results;
        }
    }
}
