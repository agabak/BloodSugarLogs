using BloodSugarLog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodSugarLog.Services
{
    public interface IBloodSugarLogService
    {
        Task<bool> Register(RegisterCommandModel model);
        Task<bool> Login(LoginCommandModel model);
        Task<bool> Logout();
        Task<bool> Create(CreateCommandModel model);
        Task<List<BloodSugarHistoryDTO>> GetBloodLogs(string email);
    }
}
