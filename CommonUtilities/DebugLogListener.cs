using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace CommonUtilities
{
    public class DebugLogListener : TextWriterTraceListener
    {

        private string fileNameWithoutExtension;
        /// <summary>
        /// Base name of log files; defaults to "TaskDebug".
        /// <seealso cref="LogExtension"/>
        /// </summary>
        public string FileNameWithoutExtension
        {
            get
            {
                //Confirm if file name is valid.. other set to default
                if (String.IsNullOrEmpty(fileNameWithoutExtension))
                {
                    fileNameWithoutExtension = "SeleniumDebugLog";
                }

                return fileNameWithoutExtension;
            }
            set
            {
                fileNameWithoutExtension = value;
            }
        }

        private string logLocation;
        /// <summary>
        /// The directory in which the log file will be stored.
        /// </summary>
        public string LogLocation
        {
            get
            {
                //Confirm if file name is valid.. other set to default
                if (String.IsNullOrEmpty(logLocation))
                {
                    logLocation = String.Format("{0}\\Logs", Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location));
                }

                return logLocation;
            }
            set
            {
                logLocation = value;
            }
        }

        /// <summary>
        /// The full path to the debug log file
        /// </summary>
        public string FullLogPath
        {
            get
            {
                return Path.Combine(logLocation, FileNameWithoutExtension + LogExtension);
            }
        }

        /// <summary>
        /// Causes a preexisting log file with the same name to be deleted.
        /// If false, the log file is appended to.
        /// </summary>
        public bool DeleteDirContents = false;
        /// <summary>
        /// If true, append the date to <see cref="FileNameWithoutExtension"/>
        /// to generate daily logs.
        /// </summary>
        public bool RollingLogs = false;
        /// <summary>
        /// Used in the construction of the log file name.
        /// </summary>
        public string LogExtension = ".log";


        /// <summary>
        /// Sets up DebugLogListener
        /// </summary>
        /// <param name="logFileName"></param>
        /// <param name="logFileLocation"></param>
        public DebugLogListener(string logFileLocation, string fileName , bool rollingLogs)
        {
            LogLocation = logFileLocation;
            RollingLogs = rollingLogs;
            fileNameWithoutExtension = fileName;
            SetupDebugLogListener();
        }

        /// <summary>
        /// All constructors go here.  This method sets all required properties.
        /// </summary>
        public void SetupDebugLogListener()
        {
            if (RollingLogs)
            {
                // Create or append to log file.
                string shortDate = DateTime.Now.ToString("yyyyMMdd");
                FileNameWithoutExtension = FileNameWithoutExtension + "_" + shortDate;
            }

            LogListenerHelpers.ConfigureTraceLogLocation(LogLocation, FileNameWithoutExtension + LogExtension, DeleteDirContents);
        }

        /// <summary>
        /// Log a message to the log file.
        /// </summary>
        /// <param name="message">content to log</param>
        public override void Write(string message)
        {
            WriteLine(message);
        }

        /// <summary>
        /// Log a message to the log file.
        /// </summary>
        /// <param name="message">content to log</param>
        /// <param name="category">the priority of the message</param>
        public override void WriteLine(string message, string category)
        {
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            if (RollingLogs)
            {
                // Create or append to log file.
                string shortDate = DateTime.Now.ToString("yyyyMMdd");
                FileNameWithoutExtension = FileNameWithoutExtension.Substring(0, FileNameWithoutExtension.LastIndexOf('_')) + "_" + shortDate;
            }

            string fullPath = Path.Combine(logLocation, FileNameWithoutExtension, LogExtension);
            string dateTime = DateTime.Now.ToString("MM-dd-yyyy h:mm:ss tt");

            //Get calling method name
            StackTrace stackTrace = new StackTrace();

            string methodName = "";
            int methodNum = 1;

            MethodBase mb = stackTrace.GetFrame(methodNum).GetMethod();
            string className = "";
            while (mb.Name.ToLowerInvariant() == "writeline" || mb.Name.ToLowerInvariant() == "handleexception")
            {
                methodNum++;
                mb = stackTrace.GetFrame(methodNum).GetMethod();
                string[] fullName = mb.ReflectedType.FullName.Split('.');
                className = fullName[fullName.Length - 1];
            }

            if (mb != null)
            {
                if (!String.IsNullOrEmpty(className))
                {
                    methodName = className + ">";
                }

                methodName += mb.Name;
            }

            // Create or append to log file.
            fullPath = Path.Combine(LogLocation, FileNameWithoutExtension + LogExtension);

            //remove extra line breaks
            if (message.EndsWith(Environment.NewLine))
            {
                message = message.Substring(0, message.LastIndexOf(Environment.NewLine));
            }

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    File.AppendAllText(fullPath, String.Format("{0} [{1}] [{2}] [{3}] {4}", dateTime, category.ToUpperInvariant(), Environment.MachineName, methodName, message + Environment.NewLine));
                    break;
                }
                catch (System.IO.IOException)
                {
                    System.Threading.Thread.Sleep(100);
                    
                }
            }
        }

        /// <summary>
        /// Log a message to the log file.
        /// </summary>
        /// <param name="message">content to log</param>
        public override void WriteLine(string message)
        {
            WriteLine(message, "INFO");
        }
    }
}
