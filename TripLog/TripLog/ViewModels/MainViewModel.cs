using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TripLogEntry> _logEntries;

        /// <summary>
        /// Public member, anders niet zichtbaar in de view/page
        /// </summary>
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>( async(entry) => await ExecuteViewCommand(entry));
        public Command NewCommand => new Command(async() => await ExecuteNewCommand());
        
        private async Task ExecuteViewCommand(TripLogEntry entry)
        {
            if (NavService == null)
            {
                return;
            }
            await NavService.NavigateTo<DetailViewModel, TripLogEntry>(entry);
        }

        private async Task ExecuteNewCommand()
        {
            await NavService.NavigateTo<NewEntryViewModel>();
        }

        public MainViewModel(INavService navService): base(navService)
        {
            LogEntries = new ObservableCollection<TripLogEntry>();
        }
        
        public override void Init()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            LogEntries.Clear();
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Washington monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2019, 2, 5),
                    Latitude = 38.8895,
                    Longitude = -77.0373655
                });
            LogEntries.Add(
                new TripLogEntry
                {
                    Title = "Statue of liberty",
                    Notes = "INspinring!",
                    Rating = 1,
                    Date = new DateTime(2019, 4, 13),
                    Latitude = 40.8895,
                    Longitude = -74.0466944
                });
            LogEntries.Add(new TripLogEntry
                {
                    Title = "Golden gate bridge",
                    Notes = "Cool!",
                    Rating = 2,
                    Date = new DateTime(2019, 4, 26),
                    Latitude = 37.8199286,
                    Longitude = -122.4804491
                });
        }
    }
}