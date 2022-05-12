using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ButterflySystems.EntityDataContracts.ResponseDataModels;
using ButterflySystems.ViewDataContracts.RequestDataModels;
using Microsoft.Extensions.Options;

namespace ButterflySystems.Api.Core.Services
{
    public class CalculationService
    {
        public readonly IHttpContextAccessor HttpContextAccessor;
        public readonly ILogger<CalculationService> Logger;
        public readonly IConfiguration Configuration;
        public readonly IServiceScopeFactory ServiceScopeFactory;

        public CalculationService(ILogger<CalculationService> logger, IHttpContextAccessor httpContextAccessor,
            IServiceScopeFactory serviceScopeFactory,
            IConfiguration configuration)
        {
            Logger = logger;
            HttpContextAccessor = httpContextAccessor;
            ServiceScopeFactory = serviceScopeFactory;
            Configuration = configuration;
        }

        public async Task<CalculationResponse> CalculateResultAsync(CalculationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstOperator.ToString()) ||
                string.IsNullOrWhiteSpace(request.LastOperator.ToString()) ||
                string.IsNullOrWhiteSpace(request.CalculationOperator) || request.CalculationOperator.Length > 1)
            {
                throw new Exception("Invalid input parameters used, please try again with different mathematical values and operators");
            }
            CalculationResponse response = new CalculationResponse();
            string toCalculate = 
                request.FirstOperator.ToString() +
                request.CalculationOperator +
                request.LastOperator.ToString();
            DataTable dt = new DataTable();
            response.Result = dt.Compute(toCalculate, "").ToString();
            
            return (response);

        }
    }
}
