using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVBGPOC.API.Models.Organization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EVBGPOC.Models;
using EVBGPOC.Views;
using EVBGPOC.ViewModels;

namespace EVBGPOC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Staff;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void Settings_Clicked(object sender, EventArgs e)
        {
            if (CalendarPicker.SelectedItem != null)
            {
                await Navigation.PushModalAsync(new NavigationPage(new CalendarSettingsPage((Calendar) CalendarPicker.SelectedItem)));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.IsFirstTime = true;
            if (viewModel.Staff.Count == 0)
                viewModel.LoadCommand.Execute(null);
        }

        private void OnPickerSelect(object sender, EventArgs e)
        {
            var picker = (Picker) sender;

            if (!(BindingContext is ItemsViewModel vm)) 
                return;
            
            if(vm.Calendars.Count > 0 && picker.SelectedIndex > -1) {
                vm.SelectCalendar(vm.Calendars[picker.SelectedIndex]);
            }
        }
    }
}