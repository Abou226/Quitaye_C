﻿using BaseVM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClientService<T, D> : BaseViewModel, IDataService<T, D> where T : class
    {
        public string ProjectId { get; set; }

        public virtual async Task<T> AddAsync(T value, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(value);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync("api/client" + url, httpContent);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                    return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> AddListAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = new HttpResponseMessage();
                var type = values.First().GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                response = await Client.PostAsync("api/client"+ type +"s"+ url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return (List<T>)values;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<IEnumerable<D>> EditAsync(List<D> values, string token, Guid id, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Add("id", id.ToString());
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PatchAsync("api/client" + url + "/" + ProjectId, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                    return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<T> DeleteAsync(string token, T value = null, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await Client.DeleteAsync("api/client" + url + "/" + ProjectId);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                    return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url+"/"+ProjectId);
            var all = Convert<T>.FromJson(json);
            return all;
        }

        public virtual async Task<List<T[]>> GetItemsGroupedAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = ConvertGrouped<T>.FromJson(json);
            return all;
        }
        public virtual async Task<T> GetItemAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = ConvertSingle<T>.FromJson(json);
            return all;
        }

        public virtual async Task<T> UpdateAsync(T value, string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string jsons = JsonConvert.SerializeObject(value);

            HttpContent httpContent = new StringContent(jsons);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PutAsync("api/client"+value.GetType().ToString() + "s" + url + "/" + ProjectId, httpContent);
            if (response.IsSuccessStatusCode)
                return value;
            else
                return null;
        }

        public virtual async Task<IEnumerable<T>> UpdateListAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);

                var type = values.First().GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PatchAsync("api/client" + type + "s" + url + "/" + ProjectId, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                    return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<object> PostAsync(object value, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(value);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = new HttpResponseMessage();
                response = await Client.PostAsync("api/client" + url, httpContent);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<D>> GetAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<D>.FromJson(json);
            return all;
        }

        public async Task<T> AddFormDataAsync(T value, string token, string url = null)
        {
            try
            {
                var response = new HttpResponseMessage();

                var type = value.GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                
                response = await Client.PostFormDataAsync("api/client" + type + "s" + url, token, value);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<List<T>>> GetIListstemsAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<List<T>>.FromJson(json);
            return all;
        }

        public async Task<IEnumerable<T>> GetIListtemsAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<T>.FromJson(json);
            return all;
        }

        public async Task<IEnumerable<T>> AddFormDataAsync(List<T> value, string token, string url = null)
        {
            var response = new HttpResponseMessage();

            var type = value.First().GetType().ToString();
            var a = type.Split('.');
            foreach (var item in a)
            {
                type = item;
            }
            response = await Client.PostFormDataAsync("api/client" + type + "s" + url, token, value);
            if (response.IsSuccessStatusCode)
                return value;
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                var mes = response.ReasonPhrase.ToString();
                return null;
            }
        }

        public async Task<string> GetProjectSourcesAsync(string url = null)
        {
            var json = await Client.GetStringAsync("api/" + url);
            return json;
        }

        public async Task<object> SimplePostAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);
                
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync("api/" + url, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var phrase = response.ReasonPhrase;
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<object> UploadFileFormDataAsync(T value, string token, string url = null)
        {
            var response = new HttpResponseMessage();
            var type = value.GetType().ToString();
            var a = type.Split('.');
            foreach (var item in a)
            {
                type = item;
            }

            response = await Client.PostFormDataAsync("api/" + type + "s/" + url, token, value);
            if (response.IsSuccessStatusCode)
            {
                if (value is Models.File)
                {
                    var data = ConvertSingle<Models.FileUrl>.FromJson(await response.Content.ReadAsStringAsync());
                    var d = new Models.File() { Url = data.Url };

                    return d;
                }
                else
                {
                    var data = ConvertSingle<T>.FromJson(await response.Content.ReadAsStringAsync());
                    return data;
                }

            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                var mes = response.ReasonPhrase.ToString();
                return null;
            }
        }
    }

    public class ClientService<T> : BaseVM.BaseViewModel, IDataService<T> where T : class
    {
        public string ProjectId { get; set; }
        public virtual async Task<T> AddAsync(T value, string token, string url = null)
        {
            try
            {
                var response = new HttpResponseMessage();

                var type = value.GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                
                response = await Client.PostFormDataAsync("api/client" + type + "s" + url, token, value);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> AddListAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);
                var type = values.First().GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync("api/client" + type + "s" + url, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var phrase = response.ReasonPhrase;
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<T> DeleteAsync(string token, T value = null, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await Client.DeleteAsync("api/client" + url + "/" + ProjectId);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                    return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> GetItemsAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<T>.FromJson(json);
            return all;
        }

        public virtual async Task<List<T[]>> GetItemsGroupedAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = ConvertGrouped<T>.FromJson(json);
            return all;
        }

        public virtual async Task<T> GetItemAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = ConvertSingle<T>.FromJson(json);
            return all;
        }

        public virtual async Task<T> UpdateAsync(T value, string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string jsons = JsonConvert.SerializeObject(value);

            HttpContent httpContent = new StringContent(jsons);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await Client.PutAsync("api/client"+value.GetType().ToString() + "s" + url + "/" + ProjectId, httpContent);
            if (response.IsSuccessStatusCode)
                return value;
            else
                return null;
        }

        public virtual async Task<IEnumerable<T>> UpdateListAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PatchAsync("api/client" + url+"/"+ProjectId, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mese = response.ReasonPhrase;
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<T> PostAsync(object value, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(value);

                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = new HttpResponseMessage();

                response = await Client.PostAsync("api/client" + url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var all = ConvertSingle<T>.FromJson(await response.Content.ReadAsStringAsync());
                    return all;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<object>> GetAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<object>.FromJson(json);
            return all;
        }

        public async Task<T> AddFormDataAsync(T value, string token, string url = null)
        {
            try
            {
                var response = new HttpResponseMessage();

                var type = value.GetType().ToString();
                var a = type.Split('.');
                foreach (var item in a)
                {
                    type = item;
                }
                
                response = await Client.PostFormDataAsync("api/client" + type + "s" + url, token, value);
                if (response.IsSuccessStatusCode)
                    return value;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var mes = response.ReasonPhrase.ToString();
                    return null;
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<IEnumerable<List<T>>> GetIListtemsAsync(string token, string url = null)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<List<T>>.FromJson(json);
            return all;
        }

        async Task<IEnumerable<T>> IDataService<T>.GetIListtemsAsync(string token, string url)
        {
            var authHeader = new AuthenticationHeaderValue("bearer", token);
            Client.DefaultRequestHeaders.Authorization = authHeader;
            var json = await Client.GetStringAsync("api/client" + url + "/" + ProjectId);
            var all = Convert<T>.FromJson(json);
            return all;
        }

        public async Task<IEnumerable<T>> AddFormDataAsync(List<T> value, string token, string url = null)
        {
            var response = new HttpResponseMessage();

            var type = value.First().GetType().ToString();
            var a = type.Split('.');
            foreach (var item in a)
            {
                type = item;
            }
            response = await Client.PostFormDataAsync("api/client" + type + "s" + url, token, value);
            if (response.IsSuccessStatusCode)
                return value;
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                var mes = response.ReasonPhrase.ToString();
                return null;
            }
        }

        public async Task<string> GetProjectSourcesAsync(string url = null)
        {
            var json = await Client.GetStringAsync("api/" + url);
            return json;
        }

        public async Task<object> SimplePostAsync(List<T> values, string token, string url = null)
        {
            try
            {
                var authHeader = new AuthenticationHeaderValue("bearer", token);
                Client.DefaultRequestHeaders.Authorization = authHeader;
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsons = JsonConvert.SerializeObject(values);
                
                HttpContent httpContent = new StringContent(jsons);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync("api/" + url, httpContent);
                if (response.IsSuccessStatusCode)
                    return values;
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    var phrase = response.ReasonPhrase;
                    return null;
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var phrase = ex.InnerException;
                return null;
            }
        }

        public async Task<object> UploadFileFormDataAsync(T value, string token, string url = null)
        {
            var response = new HttpResponseMessage();
            var type = value.GetType().ToString();
            var a = type.Split('.');
            foreach (var item in a)
            {
                type = item;
            }

            response = await Client.PostFormDataAsync("api/" + type + "s/" + url, token, value);
            if (response.IsSuccessStatusCode)
            {
                if (value is Models.File)
                {
                    var data = ConvertSingle<Models.FileUrl>.FromJson(await response.Content.ReadAsStringAsync());
                    var d = new Models.File() { Url = data.Url };

                    return d;
                }
                else
                {
                    var data = ConvertSingle<T>.FromJson(await response.Content.ReadAsStringAsync());
                    return data;
                }
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                var mes = response.ReasonPhrase.ToString();
                return null;
            }
        }
    }
}
