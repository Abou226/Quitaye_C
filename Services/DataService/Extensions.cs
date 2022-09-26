using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Services
{
    public static class Extensions
    {
        public static async Task<HttpResponseMessage> PatchAsync(this HttpClient client, string requestUri, HttpContent httpContent)
        {
            // TODO add some error handling
            var method = new HttpMethod("PATCH");

            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = httpContent
            };

            return await client.SendAsync(request);
        }

        public static async Task<HttpResponseMessage> PostFormDataAsync<T>(this HttpClient httpClient, string url, string token, T data)
        {
            using (var content = new MultipartFormDataContent())
            {
                //foreach (var item in data)
                {
                    foreach (var prop in data.GetType().GetProperties())
                    {
                        var value = prop.GetValue(data);
                        if (value is Guid || value is DateTime)
                        {
                            if (value is Guid)
                            {
                                var d = (Guid)value;
                                if (value == null || d == Guid.Empty)
                                {

                                }
                                else
                                {
                                    content.Add(new StringContent(value.ToString()), prop.Name);
                                }
                            }
                            else if (value is DateTime)
                            {
                                var d = (DateTime)value;
                                if (value != null && d != Convert.ToDateTime("01/01/0001 00:00:00"))
                                    content.Add(new StringContent(value.ToString()), prop.Name);
                            }
                        }
                        else
                        {
                            if (value is FormFile)
                            {
                                var file = value as FormFile;
                                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = prop.Name, FileName = file.FileName };
                            }
                            else
                            {
                                if (prop.Name.Equals("Image") || prop.Name.Equals("File"))
                                {
                                    var v = prop.GetValue(data);
                                    if (v is FormFile)
                                    {
                                        var file = v as FormFile;
                                        content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
                                        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") { Name = prop.Name, FileName = file.FileName };
                                    }
                                }
                                else
                                {
                                    if (value is null)
                                    {

                                    }
                                    else
                                    {
                                        var type = value.GetType().ToString();
                                        //var t = type.BaseType;
                                        if(type.Contains("System.Collections.Generic.List"))
                                        {
                                            var da = JsonConvert.SerializeObject(value);
                                            content.Add(new StringContent(da.ToString()), prop.Name);
                                        }
                                        else 
                                            content.Add(new StringContent(value.ToString()), prop.Name);
                                    }
                                }
                            }
                        }
                    }
                }


                if (!string.IsNullOrWhiteSpace(token))
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return await httpClient.PostAsync(url, content);
            }
        }
    }
}
