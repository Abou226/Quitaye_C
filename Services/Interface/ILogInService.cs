using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILogInService
    {
        Task<Secrets> LogInAsync(LogInModel user);
        Task<bool> IsTokenValid(string token);
    }
}
