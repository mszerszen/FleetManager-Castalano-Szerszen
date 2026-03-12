using ReactiveUI;
using System.Reactive;
using FleetManager.Models;  // wymagane dla klasy Vehicle

namespace FleetManager.ViewModels
{
    public class VehicleItemViewModel : ViewModelBase
    {
        public Vehicle Vehicle { get; }

        // Komendy dla zmiany statusu
        

        // Konstruktor przyjmuje pojazd i referencję do MainWindowViewModel
        public VehicleItemViewModel(Vehicle vehicle, MainWindowViewModel parent)
        {
            Vehicle = vehicle;

            
        }
    }
}