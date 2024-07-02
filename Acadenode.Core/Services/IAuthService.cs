using Acadenode.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadenode.Core.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model);
        Task<(int, string)> Login(LoginModel model);
    }
}
