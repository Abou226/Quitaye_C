﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDataService<T>
    {
         string ProjectId { get; set; }

        Task<string> GetProjectSourcesAsync(string url = null);
        Task<IEnumerable<T>> GetItemsAsync(string token, string url = null);
        Task<IEnumerable<T>> GetIListtemsAsync(string token, string url = null);
        Task<List<T[]>> GetItemsGroupedAsync(string token, string url = null);
        Task<T> GetItemAsync(string token, string url = null);
        Task<T> AddAsync(T value, string token, string url = null);
        Task<T> PostAsync(object value, string token, string url = null);
        Task<object> SimplePostAsync(List<T> value, string token, string url = null);
        Task<T> AddFormDataAsync(T value, string token, string url = null);
        Task<object> UploadFileFormDataAsync(T value, string token, string url = null);
        Task<IEnumerable<T>> AddFormDataAsync(List<T> value, string token, string url = null);
        Task<IEnumerable<T>> AddListAsync(List<T> values, string token, string url = null);
        Task<T> UpdateAsync(T value, string url, string token);
        Task<IEnumerable<T>> UpdateListAsync(List<T> values, string token, string url = null);
        Task<T> DeleteAsync(string token, T value, string url = null);
    }
    public interface IDataService<T, D>
    {
        string ProjectId { get; set; }
        Task<string> GetProjectSourcesAsync(string url = null);
        Task<IEnumerable<T>> GetItemsAsync(string token, string url = null);
        Task<IEnumerable<T>> GetIListtemsAsync(string token, string url = null);
        Task<IEnumerable<D>> GetAsync(string token, string url = null);
        Task<List<T[]>> GetItemsGroupedAsync(string token, string url = null);
        Task<T> GetItemAsync(string token, string url = null);
        Task<T> AddAsync(T value, string token, string url = null);
        Task<object> PostAsync(object value, string token, string url = null);
        Task<object> SimplePostAsync(List<T> value, string token, string url = null);
        Task<T> AddFormDataAsync(T value, string token, string url = null);
        Task<object> UploadFileFormDataAsync(T value, string token, string url = null);
        Task<IEnumerable<T>> AddFormDataAsync(List<T> value, string token, string url = null);
        Task<IEnumerable<T>> AddListAsync(List<T> values, string token, string url = null);
        Task<T> UpdateAsync(T value, string url, string token);
        Task<IEnumerable<T>> UpdateListAsync(List<T> values, string token, string url = null);
        Task<T> DeleteAsync(string token, T value, string url = null);
        Task<IEnumerable<D>> EditAsync(List<D> values, string token, Guid id, string url = null);
    }
}
