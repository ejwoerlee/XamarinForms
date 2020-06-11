using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();

            var items = new List<TripLogEntry>()
            {
                new TripLogEntry
                {
                    Title = "Washington monument",
                    Notes = "Amazing!",
                    Rating = 3,
                    Date = new DateTime(2019,2,5),
                    Latitude = 38.8895,
                    Longitude = -77.0373655
                },
                new TripLogEntry
                {
                    Title = "Statue of liberty",
                    Notes = "INspinring!",
                    Rating = 1,
                    Date = new DateTime(2019,4,13),
                    Latitude = 40.8895,
                    Longitude = -74.0466944
                },
                new TripLogEntry
                {
                    Title = "Golden gate bridge",
                    Notes = "Cool!",
                    Rating = 2,
                    Date = new DateTime(2019,4,26),
                    Latitude = 37.8199286,
                    Longitude = -122.4804491
                }
            };
            trips.ItemsSource = items;
		}

        void New_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NewEntryPage());
        }

        async void Trips_SelectionChanged(object s, SelectionChangedEventArgs e)
        {
            TripLogEntry trip = (TripLogEntry)e.CurrentSelection.FirstOrDefault();
            if (trip != null)
            {
                await Navigation.PushAsync(new DetailPage(trip));
            }
            // clear selection
            trips.SelectedItem = null;
        }
    }
}