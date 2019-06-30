using System;
using System.ComponentModel;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EVBGPOC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SettingsPage : ContentPage
    {
        private StackLayout _mainLayout;

        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is SettingsViewModel vm) 
                vm.Receive();
        }

        private void OnPickerSelect(object sender, EventArgs e)
        {
            var picker = (Picker) sender;

            if (!(BindingContext is SettingsViewModel vm)) 
                return;
            
            if(vm.Organizations.Count > 0 && picker.SelectedIndex > -1) {
                vm.SelectOrganization(vm.Organizations[picker.SelectedIndex], picker.SelectedIndex);
            }
        }
    }
}