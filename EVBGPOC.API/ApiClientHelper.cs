using System;
using System.Collections.Generic;
using EVBGPOC.API.Clients;
using EVBGPOC.API.Models.Organization;
using RestSharp;

namespace EVBGPOC.API
{
    public static class ApiClientHelper
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
        }
    }
}