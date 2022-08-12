using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace RadioPlayer.RadioBrowser
{
    public static class API
    {
        public static class URLManager
        {
            static readonly Dictionary<string, string> Countries = new Dictionary<string, string>() {
            { "Afghanistan", "AF" },
            { "Albania", "AL" },
            { "Algeria", "DZ" },
            { "American_Samoa", "AS" },
            { "Andorra", "AD" },
            { "Angola", "AO" },
            { "Antarctica", "AQ" },
            { "Antigua_And_Barbuda", "AG" },
            { "Argentina", "AR" },
            { "Armenia", "AM" },
            { "Aruba", "AW" },
            { "Ascension_And_Tristan_Da_Cunha_Saint_Helena", "SH" },
            { "Australia", "AU" },
            { "Austria", "AT" },
            { "Azerbaijan", "AZ" },
            { "Bahrain", "BH" },
            { "Bangladesh", "BD" },
            { "Barbados", "BB" },
            { "Belarus", "BY" },
            { "Belgium", "BE" },
            { "Belize", "BZ" },
            { "Benin", "BJ" },
            { "Bermuda", "BM" },
            { "Bolivarian_Republic_Of_Venezuela", "VE" },
            { "Bolivia", "BO" },
            { "Bonaire", "BQ" },
            { "Bosnia_And_Herzegovina", "BA" },
            { "Botswana", "BW" },
            { "Brazil", "BR" },
            { "British_Indian_Ocean_Territory", "IO" },
            { "Brunei_Darussalam", "BN" },
            { "Bulgaria", "BG" },
            { "Burkina_Faso", "BF" },
            { "Burundi", "BI" },
            { "Cabo_Verde", "CV" },
            { "Cambodia", "KH" },
            { "Cameroon", "CM" },
            { "Canada", "CA" },
            { "Chile", "CL" },
            { "China", "CN" },
            { "Colombia", "CO" },
            { "Costa_Rica", "CR" },
            { "Coted_Ivoire", "CI" },
            { "Croatia", "HR" },
            { "Cuba", "CU" },
            { "Curacao", "CW" },
            { "Cyprus", "CY" },
            { "Czechia", "CZ" },
            { "Denmark", "DK" },
            { "Dominica", "DM" },
            { "Ecuador", "EC" },
            { "Egypt", "EG" },
            { "El_Salvador", "SV" },
            { "Estonia", "EE" },
            { "Eswatini", "SZ" },
            { "Ethiopia", "ET" },
            { "Fiji", "FJ" },
            { "Finland", "FI" },
            { "France", "FR" },
            { "French_Guiana", "GF" },
            { "French_Polynesia", "PF" },
            { "Georgia", "GE" },
            { "Germany", "DE" },
            { "Ghana", "GH" },
            { "Gibraltar", "GI" },
            { "Greece", "GR" },
            { "Greenland", "GL" },
            { "Grenada", "GD" },
            { "Guadeloupe", "GP" },
            { "Guam", "GU" },
            { "Guatemala", "GT" },
            { "Guernsey", "GG" },
            { "Guinea", "GN" },
            { "Guinea_Bissau", "GW" },
            { "Guyana", "GY" },
            { "Haiti", "HT" },
            { "Honduras", "HN" },
            { "Hong_Kong", "HK" },
            { "Hungary", "HU" },
            { "Iceland", "IS" },
            { "India", "IN" },
            { "Indonesia", "ID" },
            { "Iraq", "IQ" },
            { "Ireland", "IE" },
            { "Islamic_Republic_Of_Iran", "IR" },
            { "Isle_Of_Man", "IM" },
            { "Israel", "IL" },
            { "Italy", "IT" },
            { "Jamaica", "JM" },
            { "Japan", "JP" },
            { "Jersey", "JE" },
            { "Jordan", "JO" },
            { "Kazakhstan", "KZ" },
            { "Kenya", "KE" },
            { "Kosovo", "XK" },
            { "Kuwait", "KW" },
            { "Kyrgyzstan", "KG" },
            { "Latvia", "LV" },
            { "Lebanon", "LB" },
            { "Lesotho", "LS" },
            { "Libya", "LY" },
            { "Liechtenstein", "LI" },
            { "Lithuania", "LT" },
            { "Luxembourg", "LU" },
            { "Macao", "MO" },
            { "Madagascar", "MG" },
            { "Malawi", "MW" },
            { "Malaysia", "MY" },
            { "Mali", "ML" },
            { "Malta", "MT" },
            { "Martinique", "MQ" },
            { "Mauritius", "MU" },
            { "Mayotte", "YT" },
            { "Mexico", "MX" },
            { "Monaco", "MC" },
            { "Mongolia", "MN" },
            { "Montenegro", "ME" },
            { "Morocco", "MA" },
            { "Mozambique", "MZ" },
            { "Myanmar", "MM" },
            { "Namibia", "NA" },
            { "Nepal", "NP" },
            { "New_Caledonia", "NC" },
            { "New_Zealand", "NZ" },
            { "Nicaragua", "NI" },
            { "Nigeria", "NG" },
            { "Norway", "NO" },
            { "Oman", "OM" },
            { "Pakistan", "PK" },
            { "Panama", "PA" },
            { "Papua_New_Guinea", "PG" },
            { "Paraguay", "PY" },
            { "Peru", "PE" },
            { "Poland", "PL" },
            { "Portugal", "PT" },
            { "Puerto_Rico", "PR" },
            { "Qatar", "QA" },
            { "Republic_Of_North_Macedonia", "MK" },
            { "Reunion", "RE" },
            { "Romania", "RO" },
            { "Rwanda", "RW" },
            { "Saint_Kitts_And_Nevis", "KN" },
            { "Saint_Lucia", "LC" },
            { "Saint_Pierre_And_Miquelon", "PM" },
            { "Saint_Vincent_And_The_Grenadines", "VC" },
            { "San_Marino", "SM" },
            { "Saudi_Arabia", "SA" },
            { "Senegal", "SN" },
            { "Serbia", "RS" },
            { "Seychelles", "SC" },
            { "Sierra_Leone", "SL" },
            { "Singapore", "SG" },
            { "Slovakia", "SK" },
            { "Slovenia", "SI" },
            { "Somalia", "SO" },
            { "South_Africa", "ZA" },
            { "South_Sudan", "SS" },
            { "Spain", "ES" },
            { "Sri_Lanka", "LK" },
            { "State_Of_Palestine", "PS" },
            { "Suriname", "SR" },
            { "Sweden", "SE" },
            { "Switzerland", "CH" },
            { "Syrian_Arab_Republic", "SY" },
            { "Taiwan_Province_Of_China", "TW" },
            { "Tajikistan", "TJ" },
            { "Thailand", "TH" },
            { "The_Bahamas", "BS" },
            { "The_Central_African_Republic", "CF" },
            { "The_Comoros", "KM" },
            { "The_Congo", "CG" },
            { "The_Cook_Islands", "CK" },
            { "The_Democratic_Peoples_Republic_Of_Korea", "KP" },
            { "The_Democratic_Republic_Of_The_Congo", "CD" },
            { "The_Dominican_Republic", "DO" },
            { "The_Falkland_Islands_Malvinas", "FK" },
            { "The_Faroe_Islands", "FO" },
            { "The_Holy_See", "VA" },
            { "The_Netherlands", "NL" },
            { "The_Philippines", "PH" },
            { "The_Republic_Of_Korea", "KR" },
            { "The_Republic_Of_Moldova", "MD" },
            { "The_Russian_Federation", "RU" },
            { "The_Sudan", "SD" },
            { "The_Turks_And_Caicos_Islands", "TC" },
            { "The_United_Arab_Emirates", "AE" },
            { "The_United_Kingdom_Of_Great_Britain_And_Northern_Ireland", "GB" },
            { "The_United_States_Minor_Outlying_Islands", "UM" },
            { "The_United_States_Of_America", "US" },
            { "Togo", "TG" },
            { "Tonga", "TO" },
            { "Trinidad_And_Tobago", "TT" },
            { "Tunisia", "TN" },
            { "Turkey", "TR" },
            { "Turkmenistan", "TM" },
            { "US_Virgin_Islands", "VI" },
            { "Uganda", "UG" },
            { "Ukraine", "UA" },
            { "United_Republic_Of_Tanzania", "TZ" },
            { "Uruguay", "UY" },
            { "Uzbekistan", "UZ" },
            { "Vanuatu", "VU" },
            { "Vietnam", "VN" },
            { "Wallis_And_Futuna", "WF" },
            { "Yemen", "YE" },
            { "Zambia", "ZM" },
            { "Zimbabwe", "ZW" }
        };
            static readonly HttpClient httpClient = new HttpClient();

            static URLManager()
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Radio Player/0.1");
            }

            public static Server GetFirstAvailableServer()
            {
                if (HostManager.ServerList.Count > 0)
                {
                    return HostManager.ServerList.First();
                } else {
                    HostManager.UpdateServers();
                    return HostManager.ServerList.First();
                }
            }

            public static async Task<ObservableCollection<RadioStation>> AdvancedSearch(string country, string tags)
            {
                Server server = GetFirstAvailableServer();

                if (server != null)
                {
                    string url = $"https://{server}/json/stations/search?country={country}&tagList={tags}&hidebroken=true";

                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            ObservableCollection<RadioStation> stations = new ObservableCollection<RadioStation>();
                            stations = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<RadioStation>>(responseBody);

                            return stations;
                        }
                    } catch(SocketException e) {

                    }
                }

                return null;
            }

            public static async Task<ObservableCollection<RadioStation>> CountrySearch(string country)
            {
                Server server = GetFirstAvailableServer();

                if (server != null)
                {
                    string url = $"https://{server}/json/stations/search?country={country}&hidebroken=true";

                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            ObservableCollection<RadioStation> stations = new ObservableCollection<RadioStation>();
                            stations = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<RadioStation>>(responseBody);

                            return stations;
                        }
                    } catch (SocketException e) {

                    }
                }

                return null;
            }

            public static async Task<ObservableCollection<RadioStation>> TagSearch(string tags)
            {
                Server server = GetFirstAvailableServer();

                if (server != null)
                {
                    string url = $"https://{server}/json/stations/search?tagList={tags}&hidebroken=true";

                    try
                    {
                        HttpResponseMessage response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();

                        if (!String.IsNullOrWhiteSpace(responseBody))
                        {
                            ObservableCollection<RadioStation> stations = new ObservableCollection<RadioStation>();
                            stations = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<RadioStation>>(responseBody);

                            return stations;
                        }
                    } catch (SocketException e) {

                    }
                }

                return null;
            }
        }

        // TODO: Refactor this
        public static class HostManager
        {
            const string DNS_LOOKUP_ADDRESS = "all.api.radio-browser.info";
            public static List<Server> ServerList = new();

            public static void UpdateServers()
            {
                ServerList.Clear();
                GetAvailableHosts();
            }

            static void GetAvailableHosts()
            {
                try
                {
                    var ipArray = Dns.GetHostAddresses(DNS_LOOKUP_ADDRESS);

                    if (ipArray.Length > 0)
                    {
                        foreach (IPAddress ip in ipArray)
                        {
                            var hostName = ReverseLookup(ip);
                            if (!String.IsNullOrWhiteSpace(hostName))
                            {
                                Server server = new Server(ip, hostName);

                                if (!ServerList.Contains(server))
                                    ServerList.Add(server);
                            }
                        }
                    }
                } catch (Exception e) {
                    MessageBox.Show($"An error occurred while trying to fetch available hosts!\n\nError Message:\n{e.Message}");
                }
                
            }

            static string ReverseLookup(IPAddress ip)
            {
                if (String.IsNullOrWhiteSpace(ip.ToString())) return "";
                string host = null;

                try
                {
                    var hostEntry = Dns.GetHostEntry(ip);
                    host = hostEntry.HostName;
                } catch (Exception e) {
                    MessageBox.Show($"An error has occurred while doing stuff.\n\nError Message:\n{e.Message}");
                }

                return host;
            }
        }
    }
}
