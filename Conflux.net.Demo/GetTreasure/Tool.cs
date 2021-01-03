using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GetTreasure
{
    class Tool
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static void Log(string content)
        {
            content = DateTime.Now.ToString() + " " + content;
            var logName = "log.txt";
            if (!File.Exists(logName)) // If file does not exists
            {
                File.Create(logName).Close(); // Create file
                using (StreamWriter sw = File.AppendText(logName))
                {
                    sw.WriteLine(content); // Write text to .txt file
                }
            }
            else // If file already exists
            {
                // File.WriteAllText("FILENAME.txt", String.Empty); // Clear file
                using (StreamWriter sw = File.AppendText(logName))
                {
                    sw.WriteLine(content); // Write text to .txt file
                }
            }
        }
    }
}
