using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.ViewEngines.Razor;

namespace IntTrader.Web
{
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "IntTrader.API";
            yield return "IntTrader.WebService";
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "Nancy.Validation";
            yield return "System.Globalization";
            yield return "System.Collections.Generic";
            yield return "System.Linq";
            yield return "IntTrader.API";
            yield return "IntTrader.API.Currency";
            yield return "IntTrader.WebService.Base";
        }

        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
