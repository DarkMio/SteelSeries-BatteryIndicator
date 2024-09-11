using BatteryTray.Devices;
using HidApi;

namespace BatteryTray;

public static class DeviceObserver
{
    public static List<(DeviceInfo deviceInfo, MouseDefinition definition)> GetDevices()
    {
        var list = new List<(DeviceInfo device, MouseDefinition definition)>();
        foreach (var device in Hid.Enumerate(0x1038))
        {
            var definition = MiceDefinitions.Definitions.SingleOrDefault(x => x.ProductId == device.ProductId);
            if (definition == null)
            {
                continue;
            }
            
            list.Add((device, definition));
        }

        return list;
    }
}