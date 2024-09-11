using System.Drawing;
using System.Runtime.InteropServices;
using H;

namespace BatteryTray;

public record AppIcon
{
    
    public static readonly AppIcon Neutral = new(Resources.AppIcon_ico);
    public static readonly AppIcon Good = new(Resources.AppIcon_good_ico);
    public static readonly AppIcon Fine = new(Resources.AppIcon_fine_ico);
    public static readonly AppIcon Okay = new(Resources.AppIcon_okay_ico);
    public static readonly AppIcon Bad = new(Resources.AppIcon_bad_ico);
    
    public Icon Icon { init; get; }
    public Uri Path { init; get; }

    private AppIcon(Resource resource)
    {
        Icon = new Icon(resource.AsStream());
        var path = System.IO.Path.GetTempFileName();
        File.WriteAllBytes(path, resource.AsBytes());
        Path = new Uri(path);
    }
    public AppIcon(Stream stream, string path)
    {
        Icon = new Icon(stream);
        Path = new Uri(path);
    }
    public AppIcon(Bitmap icon)
    {
        Icon = BitmapToIcon(icon);
        var path = System.IO.Path.GetTempFileName();
        icon.Save(path);
        Path = new Uri(path);
    }
    
    
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    extern static bool DestroyIcon(IntPtr handle);

    private static Icon BitmapToIcon(Bitmap bitmap)
    {
        IntPtr hIcon = bitmap.GetHicon();
        Icon newIcon = Icon.FromHandle(hIcon);
        return newIcon;
    }
}