using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EVBGPOC.API;
using EVBGPOC.API.Clients;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.API.Models.PhoneNumber;
using EVBGPOC.Keys;
using EVBGPOC.Models;
using EVBGPOC.Views;
using Newtonsoft.Json;
using Plugin.Messaging;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace EVBGPOC.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Calendar _selectedCalendar;
        public bool IsFirstTime = true;

        public ItemsViewModel()
        {
            Title = "Browse";
            Staff = new ObservableCollection<Staff>();
            Calendars = new ObservableCollection<Calendar>();
            LoadCommand = new Command(() => Load());
            CallSelectedCalenderCommand = new Command(async () =>
            {
                Console.WriteLine("Yes " + SelectedCalendar.PhoneNumber);
//                Device.OpenUri(new Uri($"tel:{SelectedCalendar.PhoneNumber}"));
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall(SelectedCalendar.PhoneNumber);
            });
            
            MessagingCenter.Subscribe<CalendarSettingsPage, PhoneLink>(this, "SaveCalendar", async (obj, item) =>
            {
                await CalendarClient.SavePhoneLink(item);
                await ExecuteLoadCommand(true);
            });
        }

        public async Task Load(string overrideOrganization = null)
        {
            await ExecuteLoadCommand(!IsFirstTime, overrideOrganization);
            IsFirstTime = false;
        }

        private static ISettings AppSettings =>
            CrossSettings.Current;

        public ObservableCollection<Calendar> Calendars { get; set; }

        public Calendar SelectedCalendar
        {
            get => _selectedCalendar;
            set => SetProperty(ref _selectedCalendar, value);
        }

        public ObservableCollection<Staff> Staff { get; set; }
        public Command LoadCommand { get; set; }
        public ICommand CallSelectedCalenderCommand { get; }

        private async Task ExecuteLoadCommand(bool isRefresh = false, string organizationId = null)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var previousSelected = SelectedCalendar?.Id;

            if (string.IsNullOrEmpty(organizationId))
            {
                try
                {
                    organizationId = $"{AppSettings.GetValueOrDefault(SettingsKeys.SelectedOrganizationId, string.Empty)}";
                }
                catch (Exception exception)
                {
                    organizationId = "";
                    Console.WriteLine(exception.Message);
                }
            }
            
            try
            {
                Calendars.Clear();
                
                if (string.IsNullOrEmpty(organizationId))
                {
                    IsBusy = false;
                    return;
                }
                
                var calendars = await CalendarClient.GetAllByOrganizationId(organizationId);
                foreach (var calendar in calendars)
                {
                    if (isRefresh)
                    {
                        if (calendar.Id == previousSelected && SelectedCalendar == null)
                        {
                            SelectCalendar(calendar);
                        }
                    }
                    else
                    {
                        if (SelectedCalendar == null)
                        {
                            SelectCalendar(calendar);
                        }
                    }

                    Calendars.Add(calendar);
                }

                if (SelectedCalendar == null)
                {
                    SelectCalendar(Calendars.First());
                }

                Debug.WriteLine("Calendars " + JsonConvert.SerializeObject(calendars));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void SelectCalendar(Calendar calendarToBeSelected)
        {
            SelectedCalendar = calendarToBeSelected;
            Staff.Clear();
            foreach (var staff in SelectedCalendar.Staff)
            {
                Staff.Add(staff);
            }
        }
    }
}