using System;

namespace FleetManager.Functions;

public class Functions
{
    private static readonly Random _random = new();
    
    public static int RandomFuelUsage(int CurrentFuelState)
    {
        return _random.Next(1, CurrentFuelState + 1);
    }
}