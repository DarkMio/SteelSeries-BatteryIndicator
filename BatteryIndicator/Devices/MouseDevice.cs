using System.Diagnostics.CodeAnalysis;
using HidApi;

namespace BatteryTray.Devices;

public abstract class MouseDevice<T> where T : MouseDevice<T>
{
    public readonly Device device;
    public readonly DeviceInfo DeviceInfo;

    protected MouseDevice(HidApi.Device device)
    {
        this.device = device;
        DeviceInfo = device.GetDeviceInfo();
    }

    public abstract Task<(int batteryLevel, bool charging)> GetBatteryLevel();

    protected static bool TryGetDevice(ushort vendorId, ushort deviceId, [NotNullWhen(true)] out HidApi.Device? device)
    {
        try
        {
            device = new HidApi.Device(vendorId, deviceId);
            return true;
        }
        catch
        {
            device = null;
            return false;
        }
    }
}