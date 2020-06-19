using System;
using System.Collections.Generic;
using System.Text;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseViewModel
    {
        private string _title = String.Empty;

        public string Title
        {
            get => _title;
            set 
            {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute(); //keep the CanExecute function of the SaveComamnd up to date
            }
        }

        private double _latitude = 0.0;
        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        private double _longitude = 0.0;
        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        private string _notes = String.Empty;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        bool CanSave() => !String.IsNullOrWhiteSpace(Title);

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));

        // C# 8.x: public Command SaveCommand => _saveCommand ??= new Command(Save, CanSave));

        public NewEntryViewModel()
        {
            Date = DateTime.Today;
            Rating = 1;
        }

        private void Save()
        {
            var newItem = new TripLogEntry
            {
                Title = Title,
                Latitude = Latitude,
                Longitude = Longitude,
                Date = Date,
                Rating = Rating,
                Notes = Notes
            };
        }

    }
}
