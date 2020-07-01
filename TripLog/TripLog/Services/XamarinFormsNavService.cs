using System;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TripLog.Services
{
    public class XamarinFormsNavService: INavService
    {
        private readonly IDictionary<Type, Type> _map = new Dictionary<Type, Type>();
        public INavigation XamarinFormsNav { get; set; }

        public void RegisterViewMapping(Type viewModel, Type view)
        {
            _map.Add(viewModel, view);
        }
        
    }
}