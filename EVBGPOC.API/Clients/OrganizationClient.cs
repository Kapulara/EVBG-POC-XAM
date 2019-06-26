
using System.Collections.Generic;
using EVBGPOC.API.Models.Organization;
using RestSharp;

namespace EVBGPOC.API.Clients
{
    public class OrganizationClient
    {
        private readonly RestClient _client;
        
        public OrganizationClient(RestClient client)
        {
            _client = client;
        }

        public List<Organization> GetOrganizations()
        {
            var result = _client.Execute<List<Organization>>(new RestRequest("/v1/organization", Method.GET, DataFormat.Json));
            ApiClientHelper.HandleException(result);

            return result.Data;
        }
    }
}