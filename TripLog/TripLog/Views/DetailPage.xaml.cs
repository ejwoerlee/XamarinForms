using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
        DetailViewModel ViewModel => BindingContext as DetailViewModel;

		public DetailPage ()
		{
			InitializeComponent ();
            BindingContext = new DetailViewModel(DependencyService.Get<INavService>());
		}

		private void UpdateMap()
		{
			if (ViewModel.Entry == null)
			{
				return;
			}
			
			map.MoveToRegion(MapSpan.FromCenterAndRadius(
				new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude), Distance.FromMiles(.5)));
			map.Pins.Add(new Pin
			{
				Type = PinType.Place,
				Label = ViewModel.Entry.Title,
				Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
			});
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged += OnViewModelOnPropertyChanged; 
			}
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			if (ViewModel != null)
			{
				ViewModel.PropertyChanged -= OnViewModelOnPropertyChanged;
			}
		}

		private void OnViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(DetailViewModel.Entry))
			{
				UpdateMap();
			}
		}
	}
}