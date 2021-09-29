using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ISignUpService
    {
        Task<Secrets> SignUpAsync(LogInModel user);
    }
}
