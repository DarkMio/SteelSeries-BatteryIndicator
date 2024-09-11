using HidApi;

namespace BatteryTray.Devices;

public static class Strategies
{
    private const byte AeroxWirelessFlag = 0b01000000;
    private const byte AeroxChargingFlag = 0b10000000;
    public static async Task<(int level, bool charging)> WirelessBatteryStrategyV2(Device device, bool isWireless)
    {
        byte requestByte = (byte) (0x92 | (isWireless ? AeroxWirelessFlag : 0b0));
        var report = await GetRawReport(device, [0x00, requestByte], 2);
        var level = ((report[1] & ~AeroxChargingFlag) - 1) * 5;
        var charging = (report[1] & AeroxChargingFlag) > 0;
        return (level, charging);
    }

    public static async Task<(int level, bool charging)> WirelessBatteryStrategyV1(Device device)
    {
        var report = await GetRawReport(device, [0x00, 0xAA, 0x01], 3);
        return (report[0], report[2] > 1);
    }
    
    public static Task<byte[]> GetRawReport(Device device, byte[] command, int maxLength, int milliseconds = 200)
    {
        return Task.FromResult(GetRawReportImpl(device, command, maxLength, milliseconds).ToArray());
    }
    private static ReadOnlySpan<byte> GetRawReportImpl(Device device, byte[] command, int maxLength, int milliseconds)
    {
        device.Write(command);
        return device.ReadTimeout(maxLength, milliseconds);
    }
}