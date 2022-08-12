using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer
{
    public class RadioStation
    {
        public Guid ChangeUUID { get; set; }
        public Guid StationUUID { get; set; }
        //public string ServerUUID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string URL_Resolved { get; set; }
        public string Homepage { get; set; }
        public string Favicon { get; set; }
        public string Tags { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        //public string ISO_3166_2 { get; set; }
        //public string State { get; set; }
        //public string Language { get; set; }
        //public string LanguageCodes { get; set; }
        public int Votes { get; set; }
        //public string LastChangeTime { get; set; }
        //public string LastChangeTime_ISO8601 { get; set; }
        public string Codec { get; set; }
        public int Bitrate { get; set; }
        public bool HLS { get; set; }
        //public string LastCheckOK { get; set; }
        //public string LastCheckTime { get; set; }
        //public string LastCheckTime_ISO8601 { get; set; }
        //public string LastCheckOKTime { get; set; }
        //public string LastCheckOKTime_ISO8601 { get; set; }
        //public string ClickTimeStamp { get; set; }
        //public string ClickTimeStamp_ISO8601 { get; set; }
        public int ClickCount { get; set; }
        public int ClickTrend { get; set; }
        //public int SSL_Error { get; set; }
        //public string Geo_Lat { get; set; }
        //public string Geo_Long { get; set; }
        //public bool Has_Extended_Info { get; set; }

        public bool Equals(RadioStation other)
        {
            if (other is null) return false;

            if (Equals(StationUUID, other.StationUUID))
                return true;

            return false;
        }
    }
}
