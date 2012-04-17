using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace distributer
{
    public class Settings
    {
        String mFile;
        List<KeyValuePair<String, String>> mList;

        public Settings(String file)
        {
            mFile = file;
            mList = new List<KeyValuePair<String, String>>();

            XmlTextReader reader = new XmlTextReader(mFile);
            String tmpKey = "";
            String tmpValue = "";
            try {

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "settings") continue;
                        if (tmpKey != "")
                        {
                            mList.Add(new KeyValuePair<string, string>(tmpKey, tmpValue));
                            tmpKey = "";
                            tmpValue = "";
                        }
                        tmpKey = reader.Name;
                    }
                    else if (reader.HasValue)
                    {
                        tmpValue = reader.Value;
                    }
                }
                if (tmpKey != "")
                {
                    mList.Add(new KeyValuePair<string, string>(tmpKey, tmpValue));
                }
                reader.Close();
                System.Console.WriteLine("Settings: Loaded file '" + mFile + "'");
            }
            catch (FileNotFoundException e)
            {
                System.Console.WriteLine("Settings: Unable to load file.");
            }
        }

        public void Save()
        {
            if (File.Exists(mFile))
                File.Delete(mFile);
            XmlTextWriter writer = new XmlTextWriter(mFile, Encoding.ASCII);
            writer.WriteStartDocument();
            writer.WriteStartElement("settings");

            foreach (KeyValuePair<String,String> value in mList)
            {
                writer.WriteElementString(value.Key, value.Value);
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public String getFile() { return mFile; }
        public String getString(String key)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                if (mList[i].Key == key)
                {
                    return mList[i].Value;
                }
            }
            return "";
        }
        public int getInteger(String key)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                if (mList[i].Key == key)
                {
                    return Int32.Parse(mList[i].Value);
                }
            }
            return -1;
        }
        public void setValue(String key, String value)
        {
            for(int i = 0; i < mList.Count; i++)
                if (mList[i].Key == key)
                {
                    mList.Add(new KeyValuePair<string, string>(key, value));
                    mList.RemoveAt(i);
                    return;
                }
            mList.Add(new KeyValuePair<string, string>(key, value));
        }
    }
}