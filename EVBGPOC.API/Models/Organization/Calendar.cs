using System;
using System.Collections.Generic;

namespace EVBGPOC.API.Models.Organization
{
    public class Calendar
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OrganizationId { get; set; }
        public string PhoneNumber { get; set; }
        public bool HasPhoneNumber { get; set; }
        public string EvbgId { get; set; }
        public object Data { get; set; }
        public List<Staff> Staff { get; set; }
    }
}