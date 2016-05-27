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
    [Authorize(Policy = "All")]
    public class EntertainmentController : BaseController
    {
        IEntertainmentRepository _entertainmentRepository;
        ILoggingRepository _loggingRepository;
        ILocalizationRepository _localization;
        IMembershipService _membership;

        public EntertainmentController(
            IEntertainmentRepository entertainmentRepository,
            ILoggingRepository loggingRepository,
            ILocalizationRepository localization,
            IMembershipService membership)
            : base(membership, loggingRepository)
        {
            _entertainmentRepository = entertainmentRepository;
            _loggingRepository = loggingRepository;
            _localization = localization;
            _membership = membership;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var monumentsVM = Enumerable.Empty<EntertainmentViewModel>();

            try
            {
                IEnumerable<Entertainment> entertainments = 
                    await _entertainmentRepository
                    .AllIncludingAsync(e => e.Localization);
                monumentsVM = Mapper.Map<
                    IEnumerable<Entertainment>,
                    IEnumerable<EntertainmentViewModel>>(entertainments);
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
            EntertainmentViewModel monumentVM = null;

            try
            {
                Entertainment entertainment = _entertainmentRepository
                    .GetSingle(e => e.Id == id, e => e.Localization);
                monumentVM = Mapper.Map<Entertainment, EntertainmentViewModel>(entertainment);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(monumentVM);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody]EntertainmentViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result entertainmentCreationResult = null;

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

                var entertainment = new Entertainment
                {
                    Name = value.Name,
                    Description = value.Description,
                    Payable = value.Payable,
                    Localization = localization,
                    ImageUrl = value.ImageUrl,
                    Adress = value.Adress
                };
                _entertainmentRepository.Add(entertainment);
                _entertainmentRepository.Commit();

                entertainmentCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch (Exception ex)
            {
                entertainmentCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(entertainmentCreationResult);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody]EntertainmentViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result entertainmentEditionResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before editing");
                }

                Entertainment entertainment = _entertainmentRepository
                    .GetSingle(e => e.Id == id, e => e.Localization);

                if (entertainment == null)
                {
                    return HttpBadRequest();
                }

                Localization loc = _localization
                    .GetSingle(entertainment.LocalizationId);
                loc.Latitude = value.Latitude;
                loc.Longitude = value.Longitude;
                _localization.Edit(loc);

                entertainment.Name = value.Name;
                entertainment.Description = value.Description;
                entertainment.Payable = value.Payable;
                entertainment.ImageUrl = value.ImageUrl;
                entertainment.Adress = value.Adress;
                _entertainmentRepository.Edit(entertainment);

                _entertainmentRepository.Commit();

                entertainmentEditionResult = new Result()
                {
                    Succeeded = true,
                    Message = "Editing succeeded"
                };
            }
            catch (Exception ex)
            {
                entertainmentEditionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(entertainmentEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result entertainmentDeletionResult = null;

            try
            {
                Entertainment entertainment = _entertainmentRepository
                    .GetSingle(id);
                if (entertainment != null)
                {
                    _entertainmentRepository.Delete(entertainment);
                    _entertainmentRepository.Commit();

                    entertainmentDeletionResult = new Result()
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
                entertainmentDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(entertainmentDeletionResult);
            return result;
        }
    }
}
