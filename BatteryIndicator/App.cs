using BatteryTray.Devices;
using HidApi;

namespace BatteryTray;



public class App
{
    public record MouseBatteryLevel(MouseDefinition Definition, DeviceInfo device, int Level, bool Charging);
    public delegate void BatteryEvent(App sender, List<MouseBatteryLevel> levels);
    public event BatteryEvent OnNewBatteryLevels;
    
    public App() {}

    
    
    
    
    private async Task<(int level, bool charging)?> GetBatteryLevel(Device device, MouseDefinition definition)
    {
        if ((definition.Strategy & Strategy.WirelessV2) == Strategy.WirelessV2)
        {
            return await Strategies.WirelessBatteryStrategyV2(
                device,
                (definition.Strategy & Strategy.WirelessV2Flag) == Strategy.WirelessV2Flag
            );
        }

        if ((definition.Strategy & Strategy.WirelessV1) == Strategy.WirelessV1)
        {
            return await Strategies.WirelessBatteryStrategyV1(device);
        }

        return null;
    }

    /** Permanent non-returning app loop */
    public async Task Loop()
    {
        while (true)
        {
            var devices = DeviceObserver.GetDevices();
            if (devices.Count <= 0)
            {
                OnNewBatteryLevels?.Invoke(this, []);
                await Task.Delay(new TimeSpan(0, 0, 30));
                continue;
            }

            var batteryLevels = new List<MouseBatteryLevel>();
            foreach (var deviceTuple in devices)
            {
                try
                {
                    var device = deviceTuple.deviceInfo.ConnectToDevice();
                    var batteryLevel = await GetBatteryLevel(device, deviceTuple.definition);
                    if (batteryLevel.HasValue)
                    {
                        batteryLevels.Add(new(deviceTuple.definition, deviceTuple.deviceInfo, batteryLevel.Value.level, batteryLevel.Value.charging));
                    }
                }
                catch (HidException)
                {
                    // silently catch not being able to connect to the device
                }
            }

            if (batteryLevels.Count <= 0)
            {
                OnNewBatteryLevels?.Invoke(this, []);
                await Task.Delay(new TimeSpan(0, 0, 30));
                continue;
            }
            
            OnNewBatteryLevels?.Invoke(this, batteryLevels);
            await Task.Delay(new TimeSpan(0, 0, 30));
        }
    }
}