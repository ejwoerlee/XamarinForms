using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		// Initializing Viewmodels are done automaticaly when they are navigated by
		// calling Init() in the BaseViewModel. Since main page is launched by default,
		// and not via naviagtion => add Init manually
		MainViewModel ViewModel => BindingContext as MainViewModel;

		/// <summary>
		/// Main page
		/// DependencyService.Get<INavService>() -> public class XamarinFormsNavService: INavService
		/// </summary>
        public MainPage ()
        {
            InitializeComponent();
            BindingContext = new MainViewModel(DependencyService.Get<INavService>());            
		}

		// async void Trips_SelectionChanged(object s, SelectionChangedEventArgs e)

		// {
		//     TripLogEntry trip = (TripLogEntry)e.CurrentSelection.FirstOrDefault();
        //     if (trip != null)
        //     {
        //         await Navigation.PushAsync(new DetailPage(trip));
        //     }
        //     // clear selection
        //     trips.SelectedItem = null;
        // }

        protected override void OnAppearing()
        {
	        ViewModel?.Init();
        }
	}
}