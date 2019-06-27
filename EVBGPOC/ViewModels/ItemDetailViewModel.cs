using System;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.Models;

namespace EVBGPOC.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Staff Staff { get; set; }
        public ItemDetailViewModel(Staff staff = null)
        {
            Title = staff?.Name;
            Staff = staff;
        }
    }
}
