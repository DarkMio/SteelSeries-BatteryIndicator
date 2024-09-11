namespace BatteryTray.Devices;

public static class MiceDefinitions
{
    public static IEnumerable<MouseDefinition> Definitions = new MouseDefinition[]
    {
        new Aerox3WirelessWired(),
        new Aerox3Wireless(),
        
        new Aerox5WirelessWired(),
        new Aerox5WirelessWiredDestiny2(),
        new Aerox5WirelessWiredDiablo4(),
        new Aerox5Wireless(),
        new Aerox5WirelessDestiny2(),
        new Aerox5WirelessDiablo4(),
        
        new Aerox9WirelessWired(),
        new Aerox9Wireless(),
        
        new PrimeMiniWirelessWired(),
        new PrimeMiniWireless(),
        
        new PrimeWirelessWired(),
        new PrimeWireless(),
        
        new Rival3Wireless()
    };
}



// Aerox 3
public record Aerox3WirelessWired() : MouseDefinition("Aerox 3 Wireless (wired)", 0x1038, 0x183A, Strategy.Aerox | Strategy.WirelessV2);
public record Aerox3Wireless() : MouseDefinition("Aerox 3 Wireless", 0x1038, 0x1838, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2Flag);


// Aerox 5
public record Aerox5WirelessWired() : MouseDefinition("Aerox 5 Wireless (wired)", 0x1038, 0x1854, Strategy.Aerox | Strategy.WirelessV2);
public record Aerox5Wireless() : MouseDefinition("Aerox 5 Wireless", 0x1038, 0x1852, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2Flag);

// Aerox 5 (Destiny 2)
public record Aerox5WirelessWiredDestiny2() : MouseDefinition("Destiny 2 Aerox 5 Wireless (wired)", 0x1038, 0x185E, Strategy.Aerox | Strategy.WirelessV2);
public record Aerox5WirelessDestiny2() : MouseDefinition("Destiny 2 Aerox 5 Wireless", 0x1038, 0x185C, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2Flag);

// Aerox 5 (Diablo 4)
public record Aerox5WirelessWiredDiablo4() : MouseDefinition("Diablo IV Aerox 5 Wireless (wired)", 0x1038, 0x1862, Strategy.Aerox | Strategy.WirelessV2);
public record Aerox5WirelessDiablo4() : MouseDefinition("Diablo IV Aerox 5 Wireless", 0x1038, 0x1860, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2Flag);


// Aerox 9
public record Aerox9WirelessWired() : MouseDefinition("Aerox 9 Wireless (wired)", 0x1038, 0x185A, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2);
public record Aerox9Wireless() : MouseDefinition("Aerox 9 Wireless", 0x1038, 0x1858, Strategy.Aerox | Strategy.WirelessV2 | Strategy.WirelessV2Flag);


// Prime Mini
public record PrimeMiniWirelessWired() : MouseDefinition("Prime Mini Wireless (wired)", 0x1038, 0x1842, Strategy.Prime | Strategy.WirelessV2);
public record PrimeMiniWireless() : MouseDefinition("Prime Mini Wireless", 0x1038, 0x1840, Strategy.Prime | Strategy.WirelessV2 | Strategy.WirelessV2Flag);

// Prime
public record PrimeWirelessWired() : MouseDefinition("Prime Wireless (wired)", 0x1038, 0x184A, Strategy.Prime | Strategy.WirelessV2);
public record PrimeWireless() : MouseDefinition("Prime Wireless", 0x1038, 0x1848, Strategy.Prime | Strategy.WirelessV2 | Strategy.WirelessV2Flag);

// Rival 3 
public record Rival3Wireless() : MouseDefinition("Rival 3 Wireless", 0x1038, 0x1830, Strategy.Rival | Strategy.WirelessV1);