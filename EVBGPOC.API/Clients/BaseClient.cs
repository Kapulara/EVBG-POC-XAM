using RestSharp;

namespace EVBGPOC.API.Clients
{
    public abstract class BaseClient
    {
        protected readonly RestClient Client;

        protected BaseClient(RestClient client)
        {
            Client = client;
        }
    }
}