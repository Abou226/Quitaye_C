using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInitialService
    {
        Task<Secrets> Get(LogInModel token);
        Task<Secrets> Get(string token, string url);
    }
}
