using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace CommonUtilities
{
    public static class LogListenerHelpers
    {
        /// <summary>
        /// Prepare target log location by ensuring that it exists and
        /// optionally deleting preexisting a log file.
        /// </summary>
        /// <param name="logLocation">directory to validate</param>
        /// <param name="fileName">log to delete</param>
        /// <param name="deleteContents">causes @filename to be deleted</param>
        /// <returns>whether the call was successful</returns>
        public static bool ConfigureTraceLogLocation(string logLocation, string fileName, bool deleteContents)
        {
            bool success = true;

            try
            {
                if (String.IsNullOrEmpty(logLocation))
                {
                    logLocation = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Logs");
                }

                if (!Directory.Exists(logLocation))
                {
                    Directory.CreateDirectory(logLocation);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(String.Format("Problem creating {0}", logLocation), ex);
            }

            if (deleteContents)
            {
                string fullpath = Path.Combine(logLocation, fileName);

                try
                {
                    File.Delete(fullpath);
                }
                catch (IOException ex)
                {
                    Trace.WriteLine(String.Format("Problem deleting {0}{1}{2}", fullpath, Environment.NewLine, ex.ToString()));
                    success = false;
                }
            }

            Trace.WriteLine(String.Format("LogLocation: {0}, FileName: {1}, Delete Contents: {2}", logLocation, fileName, deleteContents));

            return success;
        }
 
    }
}
