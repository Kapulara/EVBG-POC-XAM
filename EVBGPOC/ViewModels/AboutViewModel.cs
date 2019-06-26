using System;
using System.Windows.Input;
using EVBGPOC.API;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EVBGPOC.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}