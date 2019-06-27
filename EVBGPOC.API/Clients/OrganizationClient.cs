
using System.Collections.Generic;
using System.Threading.Tasks;
using EVBGPOC.API.Models.Organization;
using RestSharp;

namespace EVBGPOC.API.Clients
{
    public class OrganizationClient : BaseClient
    {
        public OrganizationClient(RestClient client) : base(client)
        {
        }

        public static Task<List<Organization>> GetOrganizations()
        {
            return ApiClientHelper.AsyncCall<List<Organization>>("/v1/organization", Method.GET);
        }
    }
}