using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace distributer
{
    public class Job
    {
        public String Title;
        public String Parameters;
        public String IP;
        public String Added;
        public String Started;
        public String Finished;
        public String ResultsFile;
        public int Generation;
        public double Best;
        public double Average;
        public double Valid;
        public bool Successful;

        public Job()
        {
            Title = "";
            Parameters = "";
            IP = "";
            Added = "";
            Started = "";
            Finished = "";
            ResultsFile = "";
            Generation = 0;
            Best = 0;
            Average = 0;
            Valid = 0;
            Successful = false;
        }
        public override string ToString()
        {
            if (IP.Length > 0)
                return Title + " : " + IP;
            return Title;
        }
        public override bool Equals(object obj)
        {
            Job other = (Job)obj;
            if(IP.Length > 0)
                return (Title == other.Title && Parameters == other.Parameters && IP == other.IP && Added == other.Added);
            return (Title == other.Title && Parameters == other.Parameters && Added == other.Added);
        }


        #region Serializer
        public static String Serialize(Job job)
        {
            String results = "{";

            results += "title:" + job.Title + ";";
            results += "parameters:" + job.Parameters + ";";
            results += "ip:" + job.IP + ";";
            results += "added:" + job.Added + ";";
            results += "started:" + job.Started + ";";
            results += "finished:" + job.Finished + ";";
            results += "file:" + job.ResultsFile + ";";
            results += "generation:" + job.Generation.ToString() + ";";
            results += "best:" + job.Best.ToString() + ";";
            results += "average:" + job.Average.ToString() + ";";
            results += "valid:" + job.Valid.ToString() + ";";
            results += "successful:" + job.Successful.ToString();

            return results + "}";
        }
        public static Job Deserialize(String serialized)
        {
            Job j = new Job();
            serialized = serialized.Substring(1, serialized.Length - 2);
            String[] settings = serialized.Split(';');
            for (int i = 0; i < settings.Length; i++)
            {
                String[] kvp = settings[i].Split(':');
                if (kvp.Length >= 2)
                {
                    switch (kvp[0])
                    {
                        case "title":
                            j.Title = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "parameters":
                            j.Parameters = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "ip":
                            j.IP = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "added":
                            j.Added = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "started":
                            j.Started = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "finished":
                            j.Finished = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "file":
                            j.ResultsFile = settings[i].Substring(kvp[0].Length + 1);
                            break;
                        case "generation":
                            j.Generation = Convert.ToInt32(settings[i].Substring(kvp[0].Length + 1));
                            break;
                        case "best":
                            j.Best = Convert.ToDouble(settings[i].Substring(kvp[0].Length + 1));
                            break;
                        case "average":
                            j.Average = Convert.ToDouble(settings[i].Substring(kvp[0].Length + 1));
                            break;
                        case "valid":
                            j.Valid = Convert.ToDouble(settings[i].Substring(kvp[0].Length + 1));
                            break;
                        case "successful":
                            j.Successful = Boolean.Parse(settings[i].Substring(kvp[0].Length + 1));
                            break;
                    }
                }
            }
            return j;
        }
        #endregion

        public void Save(String file)
        {
            if (File.Exists(file))
                File.Delete(file);
            XmlTextWriter writer = new XmlTextWriter(file, Encoding.ASCII);
            writer.WriteStartDocument();
            writer.WriteStartElement("job");

            writer.WriteElementString("title", Title);
            writer.WriteElementString("parameters", Parameters);
            writer.WriteElementString("ip", IP);
            writer.WriteElementString("added", Added);
            writer.WriteElementString("started", Started);
            writer.WriteElementString("finished", Finished);
            writer.WriteElementString("generation", Generation.ToString());
            writer.WriteElementString("best", Best.ToString());
            writer.WriteElementString("average", Average.ToString());
            writer.WriteElementString("valid", Valid.ToString());
            writer.WriteElementString("successful", Successful.ToString());
            writer.WriteElementString("results",ResultsFile);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void Start()
        {
            Started = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public void Finish(bool success)
        {
            Finished = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Successful = success;
        }
    }
}
