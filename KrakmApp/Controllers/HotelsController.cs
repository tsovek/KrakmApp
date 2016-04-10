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
    [Authorize(Policy = "OwnerOnly")]
    public class HotelsController : BaseController
    {
        IHotelRepository _hotelsRepository;
        ILoggingRepository _loggingRepository;
        IAuthorizationService _authorization;
        ILocalizationRepository _localization;
        IMembershipService _membership;

        public HotelsController(
            IHotelRepository hotelsRepository, 
            ILoggingRepository loggingRepository, 
            IAuthorizationService authorization,
            ILocalizationRepository localization,
            IMembershipService membership)
            : base(membership, loggingRepository)
        {
            _hotelsRepository = hotelsRepository;
            _loggingRepository = loggingRepository;
            _authorization = authorization;
            _localization = localization;
            _membership = membership;
        }

        [HttpGet]
        public IEnumerable<HotelViewModel> Get()
        {
            var returnedHotels = Enumerable.Empty<HotelViewModel>();

            try
            {
                IEnumerable<Hotel> hotels = _hotelsRepository
                    .GetAllByUsername(GetUsername())
                    .OrderBy(p => p.Id);

                returnedHotels = Mapper.Map<
                    IEnumerable<Hotel>, 
                    IEnumerable<HotelViewModel>>(hotels);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return returnedHotels;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            HotelViewModel hotelVM = null;
            try
            {
                Hotel hotel = _hotelsRepository
                    .GetSingleByUsername(GetUsername(), id);

                if (hotel == null)
                {
                    return HttpBadRequest();
                }

                hotelVM = Mapper.Map<Hotel, HotelViewModel>(hotel);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(hotelVM);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody]HotelViewModel value)
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
                _localization.Add(localization);

                User user = GetUser();
                var hotel = new Hotel
                {
                    User = user,
                    Name = value.Name,
                    Adress = value.Adress,
                    Phone = value.Phone,
                    Email = value.Email,
                    Localization = localization
                };
                _hotelsRepository.Add(hotel);
                _hotelsRepository.Commit();

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

        [HttpPut("{id}")]
        public IActionResult Put(
            int id, 
            [FromBody]HotelViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelEditionResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before editing");
                }

                Hotel hotel = _hotelsRepository
                    .GetSingleByUsername(GetUsername(), id);

                Localization loc =
                    _localization.GetSingle(hotel.LocalizationId);
                loc.Latitude = value.Latitude;
                loc.Longitude = value.Longitude;
                _localization.Edit(loc);

                hotel.Adress = value.Adress;
                hotel.Email = value.Email;
                hotel.Phone = value.Phone;
                hotel.Name = value.Name;
                _hotelsRepository.Edit(hotel);

                _hotelsRepository.Commit();

                hotelEditionResult = new Result()
                {
                    Succeeded = true,
                    Message = "Editing succeeded"
                };
            }
            catch (Exception ex)
            {
                hotelEditionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(hotelEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelDeletionResult = null;

            try
            {
                Hotel hotel = _hotelsRepository
                    .GetSingleByUsername(GetUsername(), id);
                if (hotel != null)
                {
                    _hotelsRepository.Delete(hotel);
                    _hotelsRepository.Commit();

                    hotelDeletionResult = new Result()
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
                hotelDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(hotelDeletionResult);
            return result;
        }
    }
}
