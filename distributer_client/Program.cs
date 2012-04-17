using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace distributer_client
{
    class Program
    {
        static void Main(string[] args)
        {
           // Manager mngr = new Manager(@".\settings.xml");
           // mngr.Start();

            DistributerClient client = new DistributerClient(@".\settings.xml");
            client.Start();
        }
    }
}
