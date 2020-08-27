using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// Mark navigation class as a dependency, so that it
// can be resolved by the Xamarin.Forms DependencyService
// This is accomplished by adding the assembly attribute to the class
// For XamarinForms Dependency service only -> [assembly:Dependency(typeof(XamarinFormsNavService))]
// But now swapped for Ninject
namespace TripLog.Services
{
    public class XamarinFormsNavService: INavService
    {
        public event PropertyChangedEventHandler CanGoBackChanged;
        
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        public INavigation XamarinFormsNav { get; set; }

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }

        public bool CanGoBack => XamarinFormsNav.NavigationStack != null && XamarinFormsNav.NavigationStack.Count > 0;

        private void OnCanGoBackChanged()
        {
            CanGoBackChanged?.Invoke(this, new PropertyChangedEventArgs("CanGoBack"));
        }

        private async Task NavigateToView(Type viewModelType)
        {
            if (!_map.TryGetValue(viewModelType, out Type viewType))
            {
                throw new ArgumentException("No view found in view mapping for " + viewModelType.FullName + ".");
            }

            var constructor = viewType.GetTypeInfo()
                .DeclaredConstructors
                .FirstOrDefault(dc => !dc.GetParameters().Any());
            
            var view = constructor.Invoke(null) as Page;
            var vm = ((App) Application.Current)
                .Kernel
                .GetService(viewModelType);
            
            view.BindingContext = vm;
            
            await XamarinFormsNav.PushAsync(view, true);
        }
        
        public async Task GoBack()
        {
            if (CanGoBack)
            {
                await XamarinFormsNav.PopAsync((true));
                OnCanGoBackChanged();
            }
        }

        public async Task NavigateTo<TVM>() where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
            var vm = XamarinFormsNav.NavigationStack.Last().BindingContext;
            (vm as BaseViewModel)?.Init();
        }

        public async Task NavigateTo<TVM, TParameter>(TParameter parameter) where TVM : BaseViewModel
        {
            await NavigateToView(typeof(TVM));
            var vm = XamarinFormsNav.NavigationStack.Last().BindingContext;
            (vm as BaseViewModel<TParameter>)?.Init();
        }

        public void RemoveLastView()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }

            var lastView = XamarinFormsNav.NavigationStack[XamarinFormsNav.NavigationStack.Count - 2];
            XamarinFormsNav.RemovePage(lastView);
        }

        public void ClearBackStack()
        {
            if (XamarinFormsNav.NavigationStack.Count < 2)
            {
                return;
            }

            for (var i = 0; i < XamarinFormsNav.NavigationStack.Count - 1; i++)
            {
                XamarinFormsNav.RemovePage(XamarinFormsNav.NavigationStack[i]);
            }
        }

        public void NavigateToUri(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentException("Invalid URI");
            }

            Device.OpenUri(uri);
            
        }

    }
}