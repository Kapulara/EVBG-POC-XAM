namespace EVBGPOC.API.Models.Organization
{
    public class Staff
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StaffId { get; set; }
        public string OrganizationId { get; set; }
        public string CalendarId { get; set; }
        public object Data { get; set; }
    }
}