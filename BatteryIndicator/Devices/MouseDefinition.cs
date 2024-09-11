namespace BatteryTray.Devices;

[Flags]
public enum Strategy
{
    Aerox,
    Prime,
    Rival,
    WirelessV2,
    WirelessV2Flag,
    WirelessV1
}

public record MouseDefinition(string Name, ushort VendorId, ushort ProductId, Strategy Strategy);