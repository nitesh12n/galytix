using System;
using System.Collections.Generic;
using System.Text;

namespace Galytix.WebApi
{
    public interface ICountryGwp
    {
        Dictionary<string,string> GWP(string country, string[] lob);
    }
}
