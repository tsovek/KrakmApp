using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KrakmApp.Core.Common;
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
                    .AllIncludingAsync(e => e.Localization);
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
        public IActionResult Get(
            int id)
        {
            MonumentViewModel monumentVM = null;

            try
            {
                Monument monuments = _monumentsRepository
                    .GetSingle(e => e.Id == id, e => e.Localization);
                monumentVM = Mapper.Map<Monument, MonumentViewModel>(monuments);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(monumentVM);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody]MonumentViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result monumentCreationResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

                var localization = new Localization
                {
                    Latitude = value.Latitude,
                    Longitude = value.Longitude,
                    Default = true
                };
                _localization.Add(localization);

                var monument = new Monument
                {
                    Name = value.Name,
                    Description = value.Description,
                    Payable = value.Payable,
                    Localization = localization,
                    ImageUrl = value.ImageUrl,
                    Adress = value.Adress
                };
                _monumentsRepository.Add(monument);
                _monumentsRepository.Commit();

                monumentCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch(Exception ex)
            {
                monumentCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(monumentCreationResult);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody]MonumentViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result monumentEditionResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before editing");
                }

                Monument monument = _monumentsRepository
                    .GetSingle(e => e.Id == id, e => e.Localization);

                if (monument == null)
                {
                    return HttpBadRequest();
                }

                Localization loc = _localization
                    .GetSingle(monument.LocalizationId);
                loc.Latitude = value.Latitude;
                loc.Longitude = value.Longitude;
                _localization.Edit(loc);

                monument.Name = value.Name;
                monument.Description = value.Description;
                monument.Payable = value.Payable;
                monument.ImageUrl = value.ImageUrl;
                _monumentsRepository.Edit(monument);

                _monumentsRepository.Commit();

                monumentEditionResult = new Result()
                {
                    Succeeded = true,
                    Message = "Editing succeeded"
                };
            }
            catch (Exception ex)
            {
                monumentEditionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(monumentEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result monumentDeletionResult = null;

            try
            {
                Monument monument = _monumentsRepository
                    .GetSingle(id);
                if (monument != null)
                {
                    _monumentsRepository.Delete(monument);
                    _monumentsRepository.Commit();

                    monumentDeletionResult = new Result()
                    {
                        Succeeded = true,
                        Message = "Deletion succeeded"
                    };
                }
                else
                {
                    return HttpBadRequest();
                }
            }
            catch (Exception ex)
            {
                monumentDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(monumentDeletionResult);
            return result;
        }
    }
}
