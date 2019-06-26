using System;
using System.Collections.Generic;

namespace EVBGPOC.API.Models.Organization
{
    public class Calendar
    {
        public string Id { get; set; }
        public long OrganizationId { get; set; }
        public long EvbgId { get; set; }
        public object Data { get; set; }
        public List<Staff> Staff { get; set; }
    }
}