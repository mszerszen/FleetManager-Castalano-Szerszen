using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace FleetManager.Services;

public class VehicleService : IVehicleService
{
    public void CloseWindow(object viewModel)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Windows.FirstOrDefault(w => w.DataContext == viewModel)?.Close();
        }
    }
}