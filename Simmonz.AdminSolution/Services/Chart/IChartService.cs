using Simmonz.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simmonz.AdminSolution.Services
{
    public  interface IChartService
    {
        Task<ApiResult<bool>> GetData();
    }
}
