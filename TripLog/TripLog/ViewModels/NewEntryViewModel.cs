﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        private readonly ILocationService _locService;
        
        private string _title = String.Empty;

        public string Title
        {
            get => _title;
            set 
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title), "Title must be provided.");
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
                Validate(() => _rating >= 1 && _rating <= 5, "Rating must be between 1 and 5.");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute(); //keep the CanExecute function of the SaveComamnd up to date
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

        bool CanSave() => !String.IsNullOrWhiteSpace(Title) && !HasErrors;

        private Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(async () => await Save(), CanSave));

        // C# 8.x: public Command SaveCommand => _saveCommand ??= new Command(Save, CanSave));

        public NewEntryViewModel(INavService navService, ILocationService locService): base(navService)
        {
            _locService = locService;
            
            Date = DateTime.Today;
            Rating = 1;
        }

        public override async void Init()
        {
            try
            {
                var coords = await _locService.GetGeoCoordinatesAsync();
                Latitude = coords.Latitude;
                Longitude = coords.Longitude;
            }
            catch (Exception )
            {
                //TODO: handle exceptions from location service
            }
        }

        private async Task Save()
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
            //TODO: Persist Entry somehwere
            await NavService.GoBack();
        }

    }
}
