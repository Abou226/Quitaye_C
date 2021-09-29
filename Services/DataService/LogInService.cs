
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BaseVM;
using System.Net.Http.Headers;
using Services;
using Models;

namespace Services
{
    public class LogInService : BaseViewModel, ILogInService
    {
        
        public async Task<bool> IsTokenValid(string token)
        {
            return await Task.FromResult(true);
        }

        public async Task<Secrets> LogInAsync(LogInModel user)
        {
            try
            {
                string jsons = JsonConvert.SerializeObject(user);
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync("api/auth/login", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var all = ConvertSingle<Secrets>.FromJson(await response.Content.ReadAsStringAsync()) ;
                    return all;
                }
                else
                {
                    Message = "Erreur internet";
                    MessageVisibility = true;
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return new Secrets();
        }
    }
}
