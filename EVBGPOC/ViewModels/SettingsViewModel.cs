using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EVBGPOC.API;
using EVBGPOC.API.Clients;
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
        
        private Organization _selectedOrganization;
        public int SelectedOrganizationIndex
        {
            get => _selectedOrganizationIndex;
            set { _selectedOrganizationIndex  = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Organization> _organizations = new ObservableCollection<Organization>();
        public ObservableCollection<Organization> Organizations
        {
            get => _organizations;
            set { _organizations  = value; OnPropertyChanged(); }
        }
        private int _selectedOrganizationIndex;

        public SettingsViewModel()
        {
            Title = "Settings";
            RefreshCommand = new Command(Receive);
        }

        public ICommand RefreshCommand { get; }


        public async void Receive()
        {
            IsBusy = true;
            Organizations.Clear();
            var organizations = await OrganizationClient.GetOrganizations();

            var selectOrganizationId = AppSettings.GetValueOrDefault(SettingsKeys.SelectedOrganizationId, string.Empty);

            if (organizations.Count > 0)
            {
                var organization = organizations.FirstOrDefault(it => it.Id == selectOrganizationId) ?? organizations[0];

                if (organization != null)
                {
                    SelectOrganization(organization, organizations.IndexOf(organization));
                }
            }
            
            foreach (var organization in organizations)
            {
                Organizations.Add(organization);
            }
            
            Console.WriteLine(JsonConvert.SerializeObject(Organizations));
            IsBusy = false;
        }

        public void SelectOrganization(Organization organizationToBeSelected, int index)
        {
            if (organizationToBeSelected == null) 
                return;
            SelectedOrganization = organizationToBeSelected;
            SelectedOrganizationIndex = index;
            AppSettings.AddOrUpdateValue(SettingsKeys.SelectedOrganizationId, $"{SelectedOrganization.EvbgOrganizationId}");
        }
    }
}