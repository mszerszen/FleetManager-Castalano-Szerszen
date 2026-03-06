using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using FleetManager.Models;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Vehicle> Vehicles { get; } = [];

    private const string FilePath = "Data/vehicles.json";
    private static readonly JsonSerializerOptions JsonOptions = new() {WriteIndented = true};

    public MainWindowViewModel()
    {
        LoadVehicles();
    }

    private void LoadVehicles()
    {
        if (!File.Exists(FilePath)) return;
        try
        {
            var jsonData = File.ReadAllText(FilePath);
            var list = JsonSerializer.Deserialize<List<Vehicle>>(jsonData);
            Vehicles.Clear();
            if (list == null) return;
            foreach (var vehicle in list)
            {
                Vehicles.Add(vehicle);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void SaveToJSON()
    {
        try
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(Vehicles, JsonOptions));
        }
    }
}