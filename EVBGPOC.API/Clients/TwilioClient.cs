using System.Collections.Generic;
using System.Threading.Tasks;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.API.Models.PhoneNumber;
using RestSharp;

namespace EVBGPOC.API.Clients
{
    public class TwilioClient : BaseClient
    {
        public TwilioClient(RestClient client) : base(client)
        {
        }

        public static Task<List<TwilioPhoneNumber>> GetNumbers()
        {
            return ApiClientHelper.AsyncCall<List<TwilioPhoneNumber>>("/v1/twilio/numbers", Method.GET);
        }
    }
}