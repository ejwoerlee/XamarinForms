using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TripLog.Models;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TripLogEntry> _logEntries;

        private ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
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