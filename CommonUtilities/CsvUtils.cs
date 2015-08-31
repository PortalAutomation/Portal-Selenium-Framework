using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics;

namespace CommonUtilities
{
    public class CsvUtils
    {
        public static List<String> GetCsvStrings(string szCsvFilePath)
        {
            List<String> csvStrings = new List<string>();
            using (TextReader tr = new StreamReader(szCsvFilePath))
            {
                //string szCsvContents = tr.ReadToEnd();

                while (tr.Peek() != -1)
                {
                    csvStrings.Add(tr.ReadLine());
                }

                tr.Close();

            }
            return csvStrings;
        }

        public static DataTable ParseCSV(string PathToFile, string Delimiter)
        {
            DataTable dt = new DataTable();

            using (StreamReader streamReader = new StreamReader(PathToFile))
            {

                string line = "";
                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] theLine = line.Split(new string[] { Delimiter }, StringSplitOptions.None);

                    //The first time through we need to create columns
                    if (dt.Columns.Count == 0)
                    {
                        for (int i = 0; i < theLine.Length; i++)
                        {
                            dt.Columns.Add(i.ToString());
                        }
                    }

                    if (dt.Columns.Count == theLine.Length)
                    {
                        dt.Rows.Add(theLine);
                    }
                    else
                    {
                        Trace.WriteLine(String.Format("Failed to add row {0} because the number of columns in the row {1} did not match the columns length {2}", line, theLine.Length, dt.Columns.Count));
                    }
                }
            }

            return dt;
        }
    }
}
