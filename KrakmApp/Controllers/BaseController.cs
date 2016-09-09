using System;
using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KrakmApp.Controllers
{
    public class BaseController : Controller
    {
        IMembershipService _membership;
        ILoggingRepository _loggingRepository;

        public BaseController(
            IMembershipService membership,
            ILoggingRepository loggingRepository)
        {
            _membership = membership;
            _loggingRepository = loggingRepository;
        }

        protected string GetUsername()
        {
            return _membership.GetUserByPrinciples(User).Name;
        }

        protected User GetUser()
        {
            return _membership.GetUserByPrinciples(User);
        }

        protected Result GetFailedResult(Exception ex)
        {
            var result = new Result()
            {
                Succeeded = false,
                Message = ex.Message
            };

            LogFail(ex);

            return result;
        }

        protected void LogFail(Exception ex)
        {
            _loggingRepository.Add(new Error()
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                DateCreated = DateTime.Now
            });
            _loggingRepository.Commit();
        }
    }
}
