using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EVBGPOC.API.Models.Organization;
using EVBGPOC.API.Models.PhoneNumber;
using Newtonsoft.Json;
using RestSharp;

namespace EVBGPOC.API.Clients
{
    public class CalendarClient : BaseClient
    {
        public CalendarClient(RestClient client) : base(client)
        {
        }

        public static Task<List<Calendar>> GetAllByOrganizationId(string organizationId)
        {
            return ApiClientHelper.AsyncCall<List<Calendar>>($"/v1/calendar/all/{organizationId}", Method.GET);
        }

        public static Task<dynamic> SavePhoneLink(PhoneLink phoneLink)
        {
            var taskCompletionSource = new TaskCompletionSource<dynamic>();
            var request = new RestRequest("/v1/calendar/phone-link", Method.POST, DataFormat.Json);
            request.AddParameter("application/json", JsonConvert.SerializeObject(new
            {
                phoneNumber = phoneLink.PhoneNumber == "none" ? null : phoneLink.PhoneNumber,
                calendarId =  phoneLink.CalendarId
            }), ParameterType.RequestBody);

            ApiClientHelper.Check();
            ApiClientHelper.Client.ExecuteAsync<dynamic>(request,
                response =>
                {
                    ApiClientHelper.HandleException(response);
                    taskCompletionSource.SetResult(null);
                });
            
            return taskCompletionSource.Task;
        }
    }
}