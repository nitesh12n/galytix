using System;
using System.Collections.Generic;
using System.Text;

namespace Galytix.WebApi.Models
{
    public class GwpModel
    {
        public GwpModel()
        {
            YearGwpValue = new List<string>();
        }
        public string Country
        {
            get;set;
        }
        public string variableId

        {
            get;set;
        }
        public string variableName
        {
            get;set;
        }
        public string lineOfBusiness
        {
            get;set;
        }
        public List<string> YearGwpValue
        {
            get;set;
        }


    }
}
