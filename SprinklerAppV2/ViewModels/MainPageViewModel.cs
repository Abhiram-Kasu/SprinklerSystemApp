using SprinklerAppV2.Services;
using SprinklerAppV2.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerAppV2.ViewModels
{
    public partial  class MainPageViewModel: ObservableObject
    {
        IZoneToLocal _zoneToLocal;

        public MainPageViewModel(IZoneToLocal zoneToLocal)
        {
            _zoneToLocal = zoneToLocal;
        }

        [ICommand]
        public async Task ClearZonePrefs()
        {
            _zoneToLocal.ClearPrefs();
            await Shell.Current.GoToAsync("..");
        }
    }
}
