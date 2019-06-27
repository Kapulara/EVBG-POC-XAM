using EVBGPOC.API.Clients;

namespace EVBGPOC.API
{
    public sealed class ApiClient
    {
        private static ApiClient _instance;
        private static readonly object Padlock = new object();

        public static ApiClient Instance
        {
            get
            {
                lock (Padlock)
                {
                    var instance = _instance;
                    return instance ?? (_instance = new ApiClient());
                }
            }
        }

        private ApiClient()
        {
            ApiClientHelper.Init();
            OrganizationClient = new OrganizationClient(ApiClientHelper.Client);
            CalendarClient = new CalendarClient(ApiClientHelper.Client);
            TwilioClient = new TwilioClient(ApiClientHelper.Client);
        }

        public OrganizationClient OrganizationClient;
        public CalendarClient CalendarClient;
        public TwilioClient TwilioClient;
    }
}