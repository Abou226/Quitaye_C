﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Quitaye;
using Models;
using Services;
using Newtonsoft.Json;
using BaseVM;

namespace Services
{
    public class InitialService : BaseViewModel, IInitialService
    {
        public async Task<Secrets> Get(LogInModel token)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token.Token);
                
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string jsons = JsonConvert.SerializeObject(token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await Client.PostAsync("api/Auth/RefreshToken", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var all = ConvertSingle<Secrets>.FromJson(result);
                    return all;
                }
                else
                {
                    return null;
                }
                //var response = await Client.PostAsync("api/Tests", httpContent);
                //if (response.StatusCode.Equals(401))
                //{
                //    await RefreshToken(token);
                //    return "Success";
                //}else
                //{
                //    return "Failed";
                //}
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Secrets> Get(string token, string url)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await Client.GetStringAsync(url);
            return ConvertSingle<Secrets>.FromJson(response);
        }

        private async Task<LogInModel> RefreshToken(LogInModel logIn)
        {
            if (!string.IsNullOrWhiteSpace(logIn.Token))
            {
                var authHeader = new AuthenticationHeaderValue("bearer", logIn.Token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Client.BaseAddress = new Uri(baseurl);
                string jsons = JsonConvert.SerializeObject(logIn);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await Client.PostAsync("api/Auth/RefreshToken", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var all = ConvertSingle<LogInModel>.FromJson(result);
                    return all;
                }
                else
                {
                    return null;
                }
            }
            else return null;
            
        }
    }
}
