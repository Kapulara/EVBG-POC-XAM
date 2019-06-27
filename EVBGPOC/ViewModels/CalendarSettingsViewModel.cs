using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using EVBGPOC.API;
using EVBGPOC.API.Clients;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.API.Models.PhoneNumber;
using EVBGPOC.Models;

namespace EVBGPOC.ViewModels
{
    public class CalendarSettingsViewModel : BaseViewModel
    {
        private Calendar _calendar;
        private TwilioPhoneNumber _selectedPhoneNumber;

        public Calendar Calendar
        {
            get => _calendar;
            set => SetProperty(ref _calendar, value);
        }
        public TwilioPhoneNumber SelectedPhoneNumber
        {
            get => _selectedPhoneNumber;
            set => SetProperty(ref _selectedPhoneNumber, value);
        }

        public ObservableCollection<TwilioPhoneNumber> PhoneNumbers { get; set; }
        

        public CalendarSettingsViewModel()
        {
            PhoneNumbers = new ObservableCollection<TwilioPhoneNumber>();
        }

        public async void Receive()
        {
            var phoneNumbers = await TwilioClient.GetNumbers();
            
            PhoneNumbers.Clear();
            PhoneNumbers.Add(new TwilioPhoneNumber
            {
                Sid = null,
                AccountSid = null,
                PhoneNumber = "none"
            });
            foreach (var phoneNumber in phoneNumbers)
            {
                if (phoneNumber.PhoneNumber == Calendar.PhoneNumber)
                {
                    SelectedPhoneNumber = phoneNumber;
                }
                PhoneNumbers.Add(phoneNumber);
            }

            if (SelectedPhoneNumber == null)
            {
                SelectedPhoneNumber = PhoneNumbers.First();
            }
        }
    }
}
