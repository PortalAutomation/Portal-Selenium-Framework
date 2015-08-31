using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using System.Collections;
using System.Runtime.InteropServices;

namespace CommonUtilities
{
    public static class ExceptionHandler
    {
        private static TraceListener m_Listener = null;

        /// <summary>
        /// Attaches a text file based trace listener.
        /// </summary>
        /// <param name="szFile">Name of the trace file.</param>
        static public void AttachTraceListener(string szFile)
        {
            DetachTraceListener();

            m_Listener = new TextWriterTraceListener(szFile);
            m_Listener.TraceOutputOptions = TraceOptions.DateTime;
            Debug.Listeners.Add(m_Listener);
        }

        /// <summary>
        /// Detaches the trace listener.
        /// </summary>
        static public void DetachTraceListener()
        {
            if (m_Listener != null)
            {
                Debug.Listeners.Remove(m_Listener);
                m_Listener.Close();
            }
        }

        /// <summary>
        /// This is the main METHOD - all other handle exceptions end up going here.  Logs exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        /// <param name="szCaughtInMethod">Name of the method in which the exception was caught.</param>
        /// <param name="bRethrow">Flag that specifies whether the exception should be rethrown.</param>
        public static string HandleException(Exception ex, string caughtInMethod, bool bRethrow)
        {

            string msg = "";
            if (!string.IsNullOrEmpty(caughtInMethod))
            {
                msg += string.Format("An exception of type {0} occured in method: {1}", ex.GetType(), caughtInMethod);
            }

            msg = GetExceptionDetails(ex);

            Trace.WriteLine(msg, "ERROR");

            if (bRethrow)
                throw ex;

            return msg;
        }

        /// <summary>
        /// Logs exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        public static string HandleException(Exception ex)
        {
            return HandleException(ex, string.Empty, false);
        }

        /// <summary>
        /// Logs exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        /// <param name="szCaughtInMethod">Name of the method in which the exception was caught.</param>
        public static string HandleException(Exception ex, string caughtInMethod)
        {
            return HandleException(ex, caughtInMethod, false);
        }

        /// <summary>
        /// Logs exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        /// <param name="bRethrow">Flag that specifies whether the exception should be rethrown.</param>
        public static string HandleException(Exception ex, bool bRethrow)
        {
            return HandleException(ex, string.Empty, bRethrow);
        }

        /// <summary>
        /// Displays a message box with the exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        public static void ShowMessageBox(Exception ex)
        {
            string CRLF = Environment.NewLine;
            System.String szDemarker = new string('-', 20);

            string szMsg = ("Exception Message: " + ex.Message + CRLF);
            szMsg += ("Stack Trace: " + ex.StackTrace + CRLF + szDemarker + CRLF);

            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                szMsg += ("\tInner Exception Message: " + innerException.Message + CRLF);
                szMsg += ("\tStack Trace: " + innerException.StackTrace + CRLF + szDemarker + CRLF);

                innerException = innerException.InnerException;
            }

            System.Windows.Forms.MessageBox.Show(szMsg);
        }

        /// <summary>
        /// Logs exception details.
        /// </summary>
        /// <param name="ex">Exception object.</param>
        /// <param name="bDemarcate">Use demarcation.</param>
        private static string GetExceptionDetails(Exception ex, bool bDemarcate = false)
        {
            string msg = "";
            string lb = Environment.NewLine;

            msg += "Exception Message: " + ex.Message + lb;
            //msg += "Stack Trace: " + ex.StackTrace + lb;
            msg += "Stack Trace: " + ex.ToString() + lb;

            if (ex.Data.Count > 0)
            {
                msg += "--- Additional Data ---" + lb;
                foreach (DictionaryEntry de in ex.Data)
                {
                    msg += string.Format("Key: {0}, Value: {1}", de.Key, de.Value) + lb;
                }
            }

            msg += DumpInnerExceptions(ex);

            if (bDemarcate)
                Demarcate();

            //Trace.WriteLine(msg, "ERROR");

            return msg;
        }

        /// <summary>
        /// Generates a demarcation string.
        /// </summary>
        private static void Demarcate()
        {
            System.String szDemarker = new string('-', 20);
            Log(szDemarker);
        }

        /// <summary>
        /// Logs message to the trace listeners.
        /// </summary>
        /// <param name="szMsg">Message</param>
        private static void Log(string szMsg)
        {
            string szMessage = DateTime.Now.ToString() + ": " + szMsg;
            Trace.WriteLine(szMessage, "ERROR");
        }

        private static string indentException(int numIndents)
        {
            string indents = "";
            for (int i = 0; i <= numIndents; i++)
            {
                indents += "\t";
            }

            return indents;
        }

        public static string DumpInnerExceptions(Exception ex)
        {
            string msg = "";
            string lb = Environment.NewLine;

            Exception innerException = ex.InnerException;

            if (innerException != null)
            {
                msg += "--- INNER EXCEPTIONS ---" + lb;

                int iter = 0;
                while (innerException != null)
                {
                    msg += string.Format("{0}Inner Exception Message: {1}{2}", indentException(iter), innerException.Message, lb);
                    msg += string.Format("{0}Stack Trace: ", indentException(iter), innerException.StackTrace, lb);

                    if (innerException.Data.Count > 0)
                    {
                        msg += "--- Additional Data ---" + lb;
                        foreach (DictionaryEntry de in innerException.Data)
                        {
                            msg += string.Format("{2}Key: {0}, Value: {1}", de.Key, de.Value, indentException(iter)) + lb;
                        }
                    }

                    innerException = innerException.InnerException;

                    iter++;
                }
            }

            return msg;
        }
    }
}
