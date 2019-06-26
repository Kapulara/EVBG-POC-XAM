using System;
using System.Collections;
using System.Collections.Generic;

namespace EVBGPOC.API.Models.Organization
{
    public class Organization
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public long EvbgOrganizationId { get; set; }
        public object EvbgOrganizationData { get; set; }
        public string EvbgApiUsername { get; set; }
        public List<Calendar> Calendars { get; set; }
    }
}