using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApplication5.Helpers
{
    public static class ApiClientExtension
    {

        static ApiClientExtension()
        {

        }



        public static T ExecuteGetAPISync<T>(this BaseHelper controller, string apiRequestURL, Func<string> requestString) where T : new()
        {
            T apiResult = new T();
            try
            {

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(30);

                    var response = httpClient.GetAsync(string.Concat(apiRequestURL, requestString.Invoke())).GetAwaiter().GetResult();
                    apiResult = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
                    // Do stuff...
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiResult;
        }

        public static T ExecuteGetAPIExtendedTime<T>(this BaseHelper controller, string apiRequestURL, Func<string> requestString) where T : new()
        {
            T apiResult = new T();
            try
            {
                HttpClient client = new HttpClient
                {

                    Timeout = TimeSpan.FromMinutes(10)
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                apiResult = client
                                    .GetAsync(string.Concat(apiRequestURL, requestString.Invoke()))
                                    .Result
                                    .Content.ReadAsAsync<T>().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiResult;
        }

        public static T ExecutePostAPI<T, T1>(this BaseHelper controller, string baseApiUrl, string postURL, T1 postData, out int resultCode) where T : new()
        {
            T response = new T();
            resultCode = 0;
            try
            {
                string webApiBaseURL = baseApiUrl;
                HttpClient client = new HttpClient { BaseAddress = new Uri(webApiBaseURL) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.PostAsJsonAsync(postURL, postData).Result
                     .Content.ReadAsAsync<T>().Result;
            }
            catch (AggregateException agex)
            {
                
            }
            catch (Exception ex)
            {
                
            }

            return response;
        }

        public static T ExecutePostAPIExtendedTimeout<T, T1>(this BaseHelper controller, string baseApiUrl, string postURL, T1 postData, out int resultCode) where T : new()
        {
            T response = new T();
            resultCode = 0;
            try
            {
                string webApiBaseURL = baseApiUrl;
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(webApiBaseURL),
                    Timeout = TimeSpan.FromMinutes(20)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                response = client.PostAsJsonAsync(postURL, postData).Result
                     .Content.ReadAsAsync<T>().Result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

    }
    public class BaseHelper
    {
    }
}