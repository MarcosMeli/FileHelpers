using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileHelpers.WizardApp
{

    public class RegConfig
    {
        private static string mBranch = @"Software\FileHelpers\";

        public static string GetStringValue(string keyName, string def)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            string res;
            res = (string)key.GetValue(keyName, def);
            key.Close();
            return res;
        }

        public static bool HasValue(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            if (key == null)
                key = Registry.CurrentUser.CreateSubKey(mBranch, RegistryKeyPermissionCheck.ReadSubTree);
            object res = (string)key.GetValue(keyName, null);
            key.Close();

            return res != null;
        }

        public static void SetStringValue(string keyName, string value)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(mBranch, true);
            key.SetValue(keyName, value, RegistryValueKind.String);
            key.Close();
        }
    }
}
