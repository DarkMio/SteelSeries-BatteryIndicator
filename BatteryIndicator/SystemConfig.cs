using Microsoft.Win32;

namespace BatteryTray;

public static class SystemConfig
{
    public static bool HasStartupEntry(string appName)
    {
        RegistryKey? rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        if (rk == null)
        {
            return false;
        }

        if (rk.GetValue(appName) is not string value)
        {
            return false;
        }
        var currentLocation = new FileInfo(value);
        return currentLocation.Exists;
    }

    public static bool SetAutostart(string appName, FileInfo appLocation, bool enableAutostart)
    {
        try
        {
            RegistryKey? rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk == null)
            {
                return false;
            }
            
            if (enableAutostart && appLocation.Exists)
            {
                rk.SetValue(appName, appLocation.FullName);
                return true;
            }

            rk.DeleteValue(appName, false);
            return false;
        }
        catch (Exception any)
        {
            Console.WriteLine(any);
            return false;
        }
    }
}