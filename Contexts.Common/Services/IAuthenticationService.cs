using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tlm.Fed.Contexts.Common.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetAccessToken(string scope);
    }
}
