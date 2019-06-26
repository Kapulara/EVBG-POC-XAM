namespace EVBGPOC.API.Models.Organization
{
    public class Staff
    {
        public string Id { get; set; }
        public long StaffId { get; set; }
        public long OrganizationId { get; set; }
        public long CalendarId { get; set; }
        public object Data { get; set; }
    }
}