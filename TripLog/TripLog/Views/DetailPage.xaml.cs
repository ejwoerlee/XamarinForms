using System;
using System.Collections.Generic;
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

            map.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude), Distance.FromMiles(.5)));
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });

            //title.Text = entry.Title;
            //date.Text = entry.Date.ToString("M");
            //rating.Text = $"{entry.Rating} star rating";
            //notes.Text = entry.Notes;
		}
	}
}