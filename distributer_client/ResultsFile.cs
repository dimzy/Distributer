using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace distributer_client
{
    struct Results
    {
        public int generation;
        public double best;
        public double avg;
        public double valid;
    }

    class ResultsFile
    {
        public static Results GetResults(string file)
        {
            Results res = new Results();
            StreamReader sr = File.OpenText(file);
            String tmp = "";
            while ((tmp = sr.ReadLine()) != null)
            {
                String[] spl = tmp.Split(',');
                for (int i = 0; i < spl.Length; i++)
                {
                    switch (i)
                    {
                        case 0:
                            res.generation = Convert.ToInt32(spl[0]);
                            break;
                        case 1:
                            res.best = Convert.ToDouble(spl[1]);
                            break;
                        case 2:
                            res.avg = Convert.ToDouble(spl[2]);
                            break;
                        case 3:
                            res.valid = Convert.ToDouble(spl[2]);
                            break;
                    }
                }
            }
            sr.Close();
            return res;
        }
    }
}
