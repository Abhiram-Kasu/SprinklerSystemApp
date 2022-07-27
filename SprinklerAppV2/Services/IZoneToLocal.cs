using SprinklerAppV2.Models;
using System.Collections.ObjectModel;

namespace SprinklerAppV2.Services
{
    public interface IZoneToLocal
    {
        Task<bool> DoesZonePrefsExist();
        Task Update(ObservableCollection<Zone> collection);

        void ClearPrefs();


    }
}