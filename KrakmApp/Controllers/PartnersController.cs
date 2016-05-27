using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using AutoMapper;

using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy ="All")]
    public class PartnersController : BaseController
    {
        IAuthorizationService _authorizationService;
        IPartnersRepository _partnersRepository;
        ILocalizationRepository _localizationRepository;
        ILoggingRepository _loggingRepository;
        IMembershipService _membership;
        IHostingEnvironment _host;

        public PartnersController(
            IAuthorizationService authorizationService,
            IPartnersRepository partnersRepository,
            ILoggingRepository loggingRepository,
            IMembershipService membershipService,
            ILocalizationRepository localizationRepository,
            IHostingEnvironment host)
            : base(membershipService, loggingRepository)
        {
            _authorizationService = authorizationService;
            _partnersRepository = partnersRepository;
            _loggingRepository = loggingRepository;
            _membership = membershipService;
            _localizationRepository = localizationRepository;
            _host = host;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var partnersVM = Enumerable.Empty<PartnerViewModel>();

            try
            {
                IEnumerable<Partner> partners = _partnersRepository
                    .GetAllByUsername(GetUsername());

                partnersVM = Mapper.Map<
                    IEnumerable<Partner>,
                    IEnumerable<PartnerViewModel>>(partners);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(partnersVM);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            PartnerViewModel partnerVM = null;
            try
            {
                Partner partner = _partnersRepository
                    .GetSingleByUsername(GetUsername(), id);

                if (partner == null)
                {
                    return HttpBadRequest();
                }

                partnerVM = Mapper.Map<Partner, PartnerViewModel>(partner);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(partnerVM);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody]PartnerViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result partnersCreationResult = null;

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
                _localizationRepository.Add(localization);

                User user = GetUser();
                var partner = new Partner
                {
                    User = user,
                    Name = value.Name,
                    Adress = value.Adress,
                    Phone = value.Phone,
                    Localization = localization,
                    ImageUrl = value.ImageUrl,
                    Commission = value.Commission,
                    UniqueKey = Guid.NewGuid(),
                    Description = value.Description,
                    StartPromotion = value.StartPromotion,
                    EndPromotion = value.EndPromotion
                };
                _partnersRepository.Add(partner);
                _partnersRepository.Commit();

                partnersCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch (Exception ex)
            {
                partnersCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(partnersCreationResult);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody]PartnerViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result partnerEditionResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before editing");
                }

                Partner partner = _partnersRepository
                    .GetSingleByUsername(GetUsername(), id);

                Localization loc =
                    _localizationRepository.GetSingle(partner.LocalizationId);
                loc.Latitude = value.Latitude;
                loc.Longitude = value.Longitude;
                _localizationRepository.Edit(loc);

                partner.Adress = value.Adress;
                partner.Phone = value.Phone;
                partner.Name = value.Name;
                partner.Description = value.Description;
                partner.Commission = value.Commission;
                partner.ImageUrl = value.ImageUrl;
                _partnersRepository.Edit(partner);

                _partnersRepository.Commit();

                partnerEditionResult = new Result()
                {
                    Succeeded = true,
                    Message = "Editing succeeded"
                };
            }
            catch (Exception ex)
            {
                partnerEditionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(partnerEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result partnerDeletionResult = null;

            try
            {
                Partner partner = _partnersRepository
                    .GetSingleByUsername(GetUsername(), id);
                if (partner != null)
                {
                    _partnersRepository.Delete(partner);
                    _partnersRepository.Commit();

                    partnerDeletionResult = new Result()
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
                partnerDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(partnerDeletionResult);
            return result;
        }
    }
}
