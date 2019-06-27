using System;
using System.ComponentModel;
using EVBGPOC.API.Models.Organization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using EVBGPOC.Models;
using EVBGPOC.ViewModels;

namespace EVBGPOC.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Staff
            {
                Id = null,
                Name = "Unknown",
                StaffId = null,
                CalendarId = null,
                OrganizationId = null,
                Data = null
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}