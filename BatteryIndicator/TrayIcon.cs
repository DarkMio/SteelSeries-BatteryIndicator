using System.Reflection;
using BatteryTray.Devices;
using H.NotifyIcon.Core;
using HidApi;
using Microsoft.Win32;

namespace BatteryTray;

public class TrayIcon
{
    private readonly TrayIconWithContextMenu _trayIcon;
    private readonly PopupMenuItem _autostartEntry;

    public delegate void ClickEvent(TrayIcon ctx);

    public delegate bool AutostartChangeEvent(TrayIcon ctx);

    public event ClickEvent? OnExitClicked;
    public event AutostartChangeEvent? OnChangeAutostartClicked;

    public TrayIcon (bool isAutostarting)
    {
        _autostartEntry = new PopupMenuItem("Autostart", (_, __) =>
        {
            var value = OnChangeAutostartClicked?.Invoke(this);
            if (value.HasValue)
            {
                _autostartEntry!.Checked = value.Value;
            }
        })
        {
            Checked = isAutostarting
        };
        _trayIcon = new TrayIconWithContextMenu()
        {
            
            Icon = AppIcon.Neutral.Icon.Handle,
            ToolTip = "Initializing",
            ContextMenu = new PopupMenu
            {
                Items =
                {
                    _autostartEntry,
                    new PopupMenuSeparator(),
                    new PopupMenuItem("Exit", (_, __) => OnExitClicked?.Invoke(this))
                }
            }
        };
        
        _trayIcon.Create();
    }
    
    public void UpdateState(string text, AppIcon icon)
    {
        _trayIcon.UpdateToolTip(text);
        _trayIcon.UpdateIcon(icon.Icon.Handle);
    }

    public void Hide() => _trayIcon.Hide();
}