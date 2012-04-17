using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace distributer
{
    public class Compressor
    {
        public static void Compress(Settings settings, String dir, String file)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = settings.getString("zip_application");
            p.Arguments = settings.getString("zip_compress_flags")  + " " + file + " " + dir;
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();
        }

        public static void Decompress(Settings settings, String file, String dir)
        {
            ProcessStartInfo p = new ProcessStartInfo();
            p.FileName = settings.getString("zip_application");
            p.Arguments = settings.getString("zip_decompress_flags") + " " + file + " -o" + dir;
            p.WindowStyle = ProcessWindowStyle.Hidden;
            Process x = Process.Start(p);
            x.WaitForExit();
        }
    }
}
