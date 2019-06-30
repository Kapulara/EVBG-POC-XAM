using System;
using System.Collections.Generic;
using System.ComponentModel;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.API.Models.PhoneNumber;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EVBGPOC.Models;
using EVBGPOC.ViewModels;

namespace EVBGPOC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CalendarSettingsPage : ContentPage
    {
        public CalendarSettingsPage(Calendar calendar)
        {
            InitializeComponent();
            
            if (!(BindingContext is CalendarSettingsViewModel vm)) 
                return;

            vm.Calendar = calendar;
            Title = calendar.Name;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            if (!(BindingContext is CalendarSettingsViewModel vm)) 
                return;

            MessagingCenter.Send(this, "SaveCalendar", new PhoneLink
            {
                CalendarId = vm.Calendar.Id,
                PhoneNumber = vm.SelectedPhoneNumber.PhoneNumber
            });
            await Navigation.PopModalAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (!(BindingContext is CalendarSettingsViewModel vm)) 
                return;
            
            vm.Receive();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}