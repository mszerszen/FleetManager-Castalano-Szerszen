using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text.Json;
using FleetManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<Vehicle> Vehicles { get; } = [];

    private const string FilePath = "Data/vehicles.json";
    private static readonly JsonSerializerOptions JsonOptions = new() {WriteIndented = true};
    
    [Reactive] public string NewName { get; set; } = string.Empty;
    [Reactive] public string NewRegistrationNumber { get; set; } = string.Empty;
    
    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Vehicle> SetAvailableCommand { get; }
    public ReactiveCommand<Unit, Vehicle> SetInRouteCommand { get; }
    public ReactiveCommand<Unit, Vehicle> SetServiceCommand { get; }

    public MainWindowViewModel()
    {
        LoadVehicles();
        
        AddCommand = ReactiveCommand.Create(AddVehicle);
        SetAvailableCommand = ReactiveCommand.Create(() =>
        {
            Vehicle.Status = "Available";
            SaveToJSON();
        });

        SetInRouteCommand = ReactiveCommand.Create(() =>
        {
            Vehicle.Status = "InRoute";
            SaveToJSON();
        });

        SetServiceCommand = ReactiveCommand.Create((vehicle) =>
        {
            Vehicle.Status = "Service";
            SaveToJSON();
        });
    }

    private void AddVehicle()
    {
        if (!new List<string> { NewName, NewRegistrationNumber }.Any(string.IsNullOrWhiteSpace))
        {
            Vehicles.Add(new Vehicle
            {
                Name = NewName,
                RegistrationNumber = NewRegistrationNumber
            });
            SaveToJSON();
            
            NewName = NewRegistrationNumber = string.Empty;
        }
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
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}