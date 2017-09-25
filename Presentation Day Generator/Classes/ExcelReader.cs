using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;
using System.Diagnostics;

namespace Presentation_Day_Generator
{
    class ExcelReader
    {

        public static string GetFileExtension(string filepath)
        {
            if (filepath != null && filepath.Length > 0)
                return filepath.Substring(filepath.LastIndexOf('.') + 1);
            else
                return "";
        }


        public static string GetFiletype(string filepath)
        {

            string extension = GetFileExtension(filepath).ToUpper();
            string result = "Invalid file";

            var excel = new ExcelQueryFactory(filepath);

            try
            {
                string[] headers;

                if (extension == "CSV")
                {
                    headers = System.IO.File.ReadLines(filepath).First().Split(',');
                }
                else 
                {
                    headers = excel.GetColumnNames(excel.GetWorksheetNames().First()).ToArray();
                }

                //This is the see if its a sentral file
                if (headers.Contains("First Name") && headers.Contains("Surname"))
                {

                    result = "Sentral";

                    if (headers.Contains("Award"))
                        result += " Awards";
                    else if (headers.Contains("Activity Name"))
                        result += " Activities";

                }
                //or a vivo file
                else if (headers.Contains("fname") && headers.Contains("sname") && headers.Contains("miles_total"))
                {
                    result = "VIVO points";
                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return result;

        }

    }
}
