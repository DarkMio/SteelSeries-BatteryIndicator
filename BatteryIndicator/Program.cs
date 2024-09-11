

using System.Diagnostics;
using System.Reflection;
using BatteryTray;

AppIcon SelectIcon(int batteryLevel)
{
    return batteryLevel switch
    {
        >= 65 => AppIcon.Good,
        < 65 and >= 45 => AppIcon.Fine,
        < 45 and >= 10 => AppIcon.Okay,
        < 10 => AppIcon.Bad
    };
}

string GetTrayText(int level, bool charging)
{
    var chargingText = charging ? "charging" : "discharging";
    return $"Level: {level}% ({chargingText})";
}



if (Process.GetProcesses().Count(x => x.ProcessName.Contains("Battery Notification")) > 1)
{
    return -1;
}

var appName = "SteelSeries Battery Indicator";
var appLocation = new FileInfo(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "SteelSeries Battery Indicator.exe"));


var tray = new TrayIcon(SystemConfig.HasStartupEntry(appName));
var app = new App();
var toaster = new NotificationBus();

tray.OnChangeAutostartClicked += (_) =>
{
    var hasEntry = SystemConfig.HasStartupEntry(appName);
    return SystemConfig.SetAutostart(appName, appLocation, !hasEntry);
};

tray.OnExitClicked += (_) =>
{
    tray.Hide();
    Environment.Exit(0);
};

app.OnNewBatteryLevels += (_, values) =>
{
    var lowestLevel = values.MinBy(x => x.Level);
    if (values.Count <= 0 || lowestLevel == null)
    {
        tray.UpdateState("No battery information available", AppIcon.Neutral);
        return;
    }
    var iconForLowest = SelectIcon(lowestLevel.Level);
    var batteryStates = values.Select(x =>
        values.Count > 0 ? $"{x.Definition.Name} {GetTrayText(x.Level, x.Charging)}" : GetTrayText(x.Level, x.Charging));
    tray.UpdateState(string.Join('\n', batteryStates), iconForLowest);

    var lowestDischarging = values.Where(x => !x.Charging).MinBy(x => x.Level);
    if (lowestDischarging is { Level: < 1000 })
    {
        var toastText = $"Your {lowestLevel.Definition.Name} reached {lowestLevel.Level}%";
        toaster.Toast(toastText, SelectIcon(lowestDischarging.Level));
    }
};

await app.Loop();
return 0;