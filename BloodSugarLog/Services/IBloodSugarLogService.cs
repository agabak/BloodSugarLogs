﻿using BloodSugarLog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodSugarLog.Services
{
    public interface IBloodSugarLogService
    {
        Task<bool> Register(RegisterCommandModel model);
        Task<bool> Login(LoginCommandModel model);
        Task<bool> Logout();
        Task<bool> Create(CreateCommandModel model, string email);
        Task<List<BloodSugarHistoryDTO>> GetBloodLogs(string email);
    }
}