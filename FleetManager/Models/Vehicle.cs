namespace FleetManager.Models;

public class Vehicle
{
    public string Name { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public int FuelPercentage { get; set; } = 100;
    public string Status { get; set; } = "Available";
}