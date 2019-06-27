using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EVBGPOC.API.Clients;
using EVBGPOC.API.Models.Organization;
using Newtonsoft.Json;
using RestSharp;

namespace EVBGPOC.API
{
    public class ApiClientHelper
    {
        public static RestClient Client;

        public static void Init()
        {
            Client = new RestClient("https://bvvliet-dev.eu.ngrok.io");
        }

        public static void HandleException<T>(IRestResponse<T> result)
        {
            if (result.ErrorException != null)
            {
                throw new Exception(result.ErrorException.Message);
            }
            
            if (result.ErrorMessage != null)
            {
                throw new Exception(result.ErrorMessage);
            }
            
            if (!result.IsSuccessful)
            {
                throw new Exception(result.StatusDescription);
            }
        }
        
        public static Task<T> AsyncCall<T>(string url, Method method) where T : new()
        {
            Check();
            var taskCompletionSource = new TaskCompletionSource<T>();
            var request = new RestRequest(url, method, DataFormat.Json);
            Client.ExecuteAsync<T>(request,
                response =>
                {
                    HandleException(response);
                    taskCompletionSource.SetResult(response.Data);
                });
            return taskCompletionSource.Task;
        }

        public static void Check()
        {
            if (Client == null)
            {
                Init();
            }
        }
    }
}