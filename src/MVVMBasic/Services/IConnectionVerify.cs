using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMBasic.Services
{
    public interface IConnectionVerify
    {
        bool HasInternetConnection();
        Task ShowNoConnectionMessage();
        Task<bool> VerifyConnectionException(Exception ex);
    }
}
