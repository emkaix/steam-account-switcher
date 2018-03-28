using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccountSwitcherV3
{
    class SteamInstanceHelper
    {
        public static void KillCurrentSession()
        {
            var si = new ProcessStartInfo();

            si.Arguments = @"/c taskkill /F /im Steam.exe";
            si.FileName = "cmd.exe";
            si.WindowStyle = ProcessWindowStyle.Hidden;

            var proc = new Process() { StartInfo = si };
            proc.Start();

            Thread.Sleep(500);
        }

        public static void LoginAccount(Account acc)
        {
            var si = new ProcessStartInfo();
            var steampath = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Valve\Steam", @"InstallPath", null) + @"\Steam.exe";

            si.Arguments = $"/c {steampath} -login {acc.Username} {acc.Password}";
            si.FileName = "cmd.exe";
            si.WindowStyle = ProcessWindowStyle.Hidden;

            var proc = new Process() { StartInfo = si };
            proc.Start();
        }
    }
}
