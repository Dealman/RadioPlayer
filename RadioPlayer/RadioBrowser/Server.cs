using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace RadioPlayer.RadioBrowser
{
    public class Server
    {
        IPAddress IP { get; set; }
        string Host { get; set; }

        public Server(IPAddress ip, string host)
        {
            IP = ip;
            Host = host;
        }

        public override string ToString()
        {
            return Host ?? "N/A";
        }
    }
}
