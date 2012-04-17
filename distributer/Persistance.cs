using System;  
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace distributer
{
    public class Persistance
    {
        public static List<Job> LoadJobs(String file)
        {
            List<Job> jobs = new List<Job>();
            List<String> sjobs = Persistance.LoadStrings(file);
            foreach (String serialized in sjobs)
            {
                jobs.Add(Job.Deserialize(serialized));
            }
            return jobs;
        }
        public static void StoreJobs(List<Job> jobs, String file)
        {
            List<String> sjobs = new List<String>();
            foreach (Job j in jobs)
                sjobs.Add(Job.Serialize(j));
            Persistance.StoreStrings(sjobs, file);
        }

        public static void StoreStrings(List<String> strings, String file)
        {
            if (File.Exists(file))
                File.Delete(file);
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.ASCII);
            writer.WriteStartDocument();
            writer.WriteStartElement("stringlist");

            foreach (String str in strings)
            {
                writer.WriteElementString("string", str);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
        public static List<String> LoadStrings(String file)
        {
            List<String> strings = new List<String>();
            if (!File.Exists(file)) return strings;
            XmlTextReader reader = new XmlTextReader(file);

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    if (reader.Name == "string")
                    {
                        reader.Read();
                        strings.Add(reader.Value);
                    }
                }
            }
            reader.Close();
            return strings;
        }

        public static void Log(String sessionFile, String message)
        {
            if (!Directory.Exists(@".\logs\"))
                Directory.CreateDirectory(@".\logs\");
            try
            {
                FileStream fs = new FileStream(sessionFile, FileMode.Append);
                ASCIIEncoding asen = new ASCIIEncoding();
                fs.Write(asen.GetBytes(message), 0, asen.GetBytes(message).Length);
                fs.Close();
            }
            catch
            {
                Console.WriteLine("Cannot open log file.");
            }
        }
    }
}
