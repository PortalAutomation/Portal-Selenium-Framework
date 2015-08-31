using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Diagnostics;

namespace CommonUtilities
{
    public class Config : Dictionary<string, string>
    {

        private Dictionary<string, string> configDictionary = new Dictionary<string, string>();

        public string FullPath;
        public string RootName;
        public bool WriteOnUpdate;
        
        public Config()
        {
        }

        public string readAutoConfigKey()
        {
            return string.Empty;
        }

        /// <summary>
        /// Read
        /// </summary>
        public void read(string xmlFile) 
        {
            // read environment variables -- if they exist, use these instead of reading from xml config file
            IDictionary sysVariables = Environment.GetEnvironmentVariables();
            if (sysVariables.Contains("Browser") &&
                sysVariables.Contains("WebServer") &&
                sysVariables.Contains("DebugLogLocation") &&
                sysVariables.Contains("EnableVideoRecording"))
            {
                this["Browser"] = Environment.GetEnvironmentVariable("Browser");
                this["WebServer"] = Environment.GetEnvironmentVariable("WebServer");
                this["DebugLogLocation"] = Environment.GetEnvironmentVariable("DebugLogLocation");
                this["EnableVideoRecording"] = Environment.GetEnvironmentVariable("EnableVideoRecording");
            }
            else
            {
                try
                {
                    using (XmlReader r = XmlReader.Create(xmlFile))
                    {
                        populateDictionary(r);
                    }
                    //break;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine("Encountered exception trying to read the config file '" + xmlFile + "'" + ": " +
                                    ex.Message);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public void setAutConfigKey()
        {
        }

        public void populateDictionary(XmlReader r) 
        {
            bool tempUpdate = WriteOnUpdate;
            try
            {
                WriteOnUpdate = false;
                r.MoveToContent(); // moves to root node, which we don't care about.
                RootName = r.Name;
                r.Read();
                while (!r.EOF)   // moves to next node
                {
                    if (XmlNodeType.Element == r.NodeType)
                    {
                        this[XmlConvert.DecodeName(r.Name)] = r.ReadElementContentAsString();
                    }
                    else
                    {
                        r.Read();
                    }
                }
            }
            finally
            {
                WriteOnUpdate = tempUpdate;
            }
        }

    }
}
