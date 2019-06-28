using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.Keys;
using EVBGPOC.ViewModels;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xunit;

namespace EVBGPOC.UnitTests
{
    public class ItemsUnitTests
    {
        public ItemsViewModel ItemsViewModel = new ItemsViewModel();
        
        [Fact]
        public void UpdateSelectedCalendar()
        {
            var calendar = new Calendar
            {
                Id = null,
                Data = null,
                Name = "Test",
                OrganizationId = "99999",
                PhoneNumber = "+31600000000",
                EvbgId = "",
                HasPhoneNumber = true,
                Staff = new List<Staff>()
            };
            ItemsViewModel.SelectedCalendar = calendar;
            Assert.Equal(calendar, ItemsViewModel.SelectedCalendar);
        }
        
        [Fact]
        public void UpdateCalendars()
        {
            var calendars = new ObservableCollection<Calendar>();
            ItemsViewModel.Calendars = calendars;
            Assert.Equal(calendars, ItemsViewModel.Calendars);
        }
        
        [Fact]
        public void UseSelectCalendar()
        {
            var calendarWithOneStaff = new Calendar
            {
                Id = null,
                Data = null,
                Name = "Test",
                OrganizationId = "99999",
                PhoneNumber = "+31600000000",
                EvbgId = "",
                HasPhoneNumber = true,
                Staff = new List<Staff>()
                {
                    new Staff()
                    {
                        Id = "1"
                    }
                }
            };
            var calendarWithTwoStaff = new Calendar
            {
                Id = null,
                Data = null,
                Name = "Test",
                OrganizationId = "99999",
                PhoneNumber = "+31600000000",
                EvbgId = "",
                HasPhoneNumber = true,
                Staff = new List<Staff>()
                {
                    new Staff()
                    {
                        Id = "1"
                    },
                    new Staff()
                    {
                        Id = "2"
                    }
                }
            };
            Assert.Null(ItemsViewModel.SelectedCalendar);
            ItemsViewModel.SelectCalendar(calendarWithOneStaff);
            Assert.Equal(calendarWithOneStaff, ItemsViewModel.SelectedCalendar);
            Assert.Equal(1, ItemsViewModel.Staff.Count);
            ItemsViewModel.SelectCalendar(calendarWithTwoStaff);
            Assert.Equal(calendarWithTwoStaff, ItemsViewModel.SelectedCalendar);
            Assert.Equal(2, ItemsViewModel.Staff.Count);
        }
        
        [Fact]
        public async void TestLoadCommand()
        {
            Assert.True(ItemsViewModel.IsFirstTime);
            await ItemsViewModel.Load();
            Assert.False(ItemsViewModel.IsFirstTime);
            Assert.Equal(0, ItemsViewModel.Calendars.Count);
            Assert.Equal(0, ItemsViewModel.Staff.Count);
        }
        
        [Fact]
        public async void TestLoadWithOrganizationCommand()
        {
            Assert.True(ItemsViewModel.IsFirstTime);
            await ItemsViewModel.Load("453003085618525");
            Assert.False(ItemsViewModel.IsFirstTime);
            Assert.Equal(4, ItemsViewModel.Calendars.Count);
            Assert.Equal(3, ItemsViewModel.Staff.Count);
        }
    }
}