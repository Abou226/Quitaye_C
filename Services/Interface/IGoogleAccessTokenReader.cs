using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IGoogleAccessTokenReader
    {
        Task<TokenResponse> GetOrNullAsync();
    }
}
