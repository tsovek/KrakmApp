using System;
using System.Collections.Generic;
using System.Linq;

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
    [Authorize(Policy ="OwnerOnly")]
    public class PartnersController : BaseController
    {
        IAuthorizationService _authorizationService;
        IPartnersRepository _partnersRepository;
        ILocalizationRepository _localizationRepository;
        ILoggingRepository _loggingRepository;
        IMembershipService _membership;

        public PartnersController(
            IAuthorizationService authorizationService,
            IPartnersRepository partnersRepository,
            ILoggingRepository loggingRepository,
            IMembershipService membershipService,
            ILocalizationRepository localizationRepository)
            : base(membershipService, loggingRepository)
        {
            _authorizationService = authorizationService;
            _partnersRepository = partnersRepository;
            _loggingRepository = loggingRepository;
            _membership = membershipService;
            _localizationRepository = localizationRepository;
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

        [HttpPost]
        public IActionResult Post(
            [FromBody]PartnerViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelCreationResult = null;

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
                    UniqueKey = Guid.NewGuid()
                };
                _partnersRepository.Add(partner);
                _partnersRepository.Commit();

                hotelCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch (Exception ex)
            {
                hotelCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(hotelCreationResult);
            return result;
        }
    }
}
