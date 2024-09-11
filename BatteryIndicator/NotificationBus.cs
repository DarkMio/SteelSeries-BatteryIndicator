using Microsoft.Toolkit.Uwp.Notifications;

namespace BatteryTray;

public class NotificationBus
{
    private DateTimeOffset lastToastTime;
    
    public NotificationBus()
    {
        lastToastTime = DateTimeOffset.MinValue;
    }


    public void Toast(string text, AppIcon icon)
    {
        var now = DateTimeOffset.Now;
        var timeDelta = now - lastToastTime;
        if (timeDelta.TotalHours < 8)
        {
            return;
        }

        lastToastTime = now;
        new ToastContentBuilder()
            .AddText(text)
            .AddAppLogoOverride(icon.Path)
            .Show();
    }
}