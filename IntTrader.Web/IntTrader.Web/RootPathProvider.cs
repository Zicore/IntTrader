using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nancy;

namespace IntTrader.Web
{
    public class RootPathProvider : IRootPathProvider
    {
        private static readonly string RootPath;
        static RootPathProvider()
        {
            RootPath = @"C:\dev\cs\IntTrader.Web\IntTrader.Web";
        }
        public string GetRootPath()
        {
            return RootPath;
        }
    }
}
