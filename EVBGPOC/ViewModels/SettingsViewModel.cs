using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EVBGPOC.API;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.Keys;
using EVBGPOC.Models;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace EVBGPOC.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private static ISettings AppSettings =>
            CrossSettings.Current;

        public Organization SelectedOrganization
        {
            get => _selectedOrganization;
            set => SetProperty(ref _selectedOrganization, value);
        }

        private bool _isLoading;
        private Organization _selectedOrganization;
        private ObservableCollection<string> _organizationNames = new ObservableCollection<string>();
        public ObservableCollection<string> OrganizationNames
        {
            get => _organizationNames;
            set { _organizationNames  = value; OnPropertyChanged(); }
        }
        public int SelectedOrganizationIndex
        {
            get => _selectedOrganizationIndex;
            set { _selectedOrganizationIndex  = value; OnPropertyChanged(); }
        }

        public List<Organization> Organizations = new List<Organization>();
        private int _selectedOrganizationIndex;

        public SettingsViewModel()
        {
            Title = "Settings";
            RefreshCommand = new Command(Receive);
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set => SetProperty(ref _isLoading, value);
        }

        public ICommand RefreshCommand { get; }


        public void Receive()
        {
            OrganizationNames.Clear();
            IsBusy = true;
            Organizations = ApiClient.Instance.OrganizationClient.GetOrganizations();

            if (Organizations.Count > 0 && SelectedOrganization == null)
            {
                SelectedOrganization = Organizations[0];
                SelectedOrganizationIndex = 0;
            }
            
            foreach (var organization in Organizations)
            {
                OrganizationNames.Add(organization.Name);
            }
            
            Console.WriteLine(JsonConvert.SerializeObject(OrganizationNames));
            IsBusy = false;
        }
    }
}