using SprinklerAppV2.Models;
using SprinklerAppV2.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerAppV2.ViewModels
{
    public partial class ZoneSetupViewModel : ObservableObject
    {
        private IZoneToLocal _zoneToLocal;
        
        public ObservableCollection<Zone> Zones { get; set; } = new();
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        private bool _isBusy;
        

        public bool IsNotBusy => !IsBusy;
        
        public ZoneSetupViewModel(IZoneToLocal zoneToLocal)
        {
            _zoneToLocal = zoneToLocal;
            
           
        }



        [ICommand]
        public async Task OnLoaded()
        {
            if (await _zoneToLocal.DoesZonePrefsExist())
            {
                GoToNextPage();
            }
        }

        [ICommand]
        public void GoToNextPage()
        {
            Shell.Current.GoToAsync(nameof(MainPage));
        }

        [ICommand]
        public async Task AddZone()
        {
            if (IsBusy)
            {
                return;
            }
            IsBusy = true;
            var zone = new Zone()
            {
                Name = $"Zone{Zones.Count + 1}",
                ZoneId = Zones.Count + 1
            };
            try
            {
                Zones.Add(zone);
            }
            catch (Exception ex)
            {

            }
            
            await _zoneToLocal.Update(Zones);
            IsBusy = false;
            
        }
        [ICommand]
        public async Task RemoveZone(Guid id)
        {
            
            Zones.Remove(Zones.Where(x => x.Id == id).First());
            await _zoneToLocal.Update(Zones);
            
            
        }

    }
}
