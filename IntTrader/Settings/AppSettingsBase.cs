using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using Zicore.Settings.Json;

namespace IntTrader.Settings
{
    public class AppSettingsBase : JsonSettings
    {
        protected AppSettingsBase()
        {

        }

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static string ApplicationName = "IntTrader";
        public static string FileName = "AppSettings.json";

        public void Load()
        {
            try
            {
                Load(ApplicationName, FileName);
            }
            catch (FileNotFoundException)
            {
                Save();
            }
            catch (Exception ex)
            {
                Log.Debug(ex);
            }
        }
    }
}
