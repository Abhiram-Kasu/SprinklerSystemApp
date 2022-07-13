using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SprinklerSystemApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprinklerSystemApp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        IConnectivity connectivity;
        HttpClient httpClient;
        public ObservableCollection<SprinklerTimeSpan> SprinklerOnTimes { get; set; } = new();

        public MainPageViewModel(IConnectivity connectivity, HttpClient httpClient)
        {
            this.connectivity = connectivity;
            this.httpClient = httpClient;
            HasOnTimes = false;
            


        }
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(DoesNotHaveOnTimes))]
        private bool _hasOnTimes;

        public bool DoesNotHaveOnTimes => !HasOnTimes;

        [ObservableProperty]
        private string _sprinklerBackendUrl;
        [ObservableProperty]
        [AlsoNotifyChangeFor(nameof(IsNotBusy))]
        private bool isBusy;

        public bool IsNotBusy => !IsBusy;
        [ObservableProperty]
        private DateOnly _date;

        [ICommand]
        public void RemoveSprinklerOn(Guid id)
        {
            SprinklerOnTimes.Remove(SprinklerOnTimes.Where(x => x.Id == id).First());
            if(SprinklerOnTimes.Count == 0)
            {
                HasOnTimes = false;
            }
        }

        [ICommand]
       public void AddSprinklerOn()
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var addedtime = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(45));


            SprinklerOnTimes.Add(new SprinklerTimeSpan()
            {
                Id = Guid.NewGuid(),
                Day = DateTime.Now,
                BegTime = currentTime,
                EndTime = addedtime,

            });
            HasOnTimes = true;
            
        }
        

        [ICommand]
        public async void CheckUrl()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            var hasInternet = connectivity?.NetworkAccess == NetworkAccess.Internet;

            if (!hasInternet)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Detected no internet connection", "Ok");
            }
            else
            {
                try
                {
                    var res = await httpClient.GetAsync(SprinklerBackendUrl);
                    if (res.IsSuccessStatusCode)
                    {
                        await App.Current.MainPage.DisplayAlert("Server", "Valid Url", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Sever", $"Server not detected on url {SprinklerBackendUrl}", "Ok");
                    }
                }
                catch (Exception)
                {
                    await App.Current.MainPage.DisplayAlert("Sever", $"Server not detected on url {SprinklerBackendUrl}", "Ok");
                }
            }
            IsBusy = false;
            
            
        }

    }
}
