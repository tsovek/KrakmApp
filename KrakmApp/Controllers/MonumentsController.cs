using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy ="All")]
    public class MonumentsController : BaseController
    {
        IMonumentRepository _monumentsRepository;
        ILoggingRepository _loggingRepository;
        ILocalizationRepository _localization;
        IMembershipService _membership;

        public MonumentsController(
            IMonumentRepository monumentsRepository,
            ILoggingRepository loggingRepository,
            ILocalizationRepository localization,
            IMembershipService membership)
            : base(membership, loggingRepository)
        {
            _monumentsRepository = monumentsRepository;
            _loggingRepository = loggingRepository;
            _localization = localization;
            _membership = membership;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var monumentsVM = Enumerable.Empty<MonumentViewModel>();

            try
            {
                IEnumerable<Monument> monuments = await _monumentsRepository
                    .GetAllAsync();
                monumentsVM = Mapper.Map<
                    IEnumerable<Monument>, 
                    IEnumerable<MonumentViewModel>>(monuments);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(monumentsVM);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            int id)
        {
            MonumentViewModel monumentVM = null;

            try
            {
                Monument monuments = await _monumentsRepository
                    .GetSingleAsync(id);
                monumentVM = Mapper.Map<Monument, MonumentViewModel>(monuments);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(monumentVM);
        }

        [HttpPost]
        public void Post(
            [FromBody]string value)
        {

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
