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
    public class HotelsController : Controller
    {
        IHotelRepository _hotelsRepository;
        ILoggingRepository _loggingRepository;
        IAuthorizationService _authorization;
        ILocalizationRepository _localization;
        IMembershipService _membershipService;

        public HotelsController(
            IHotelRepository hotelsRepository, 
            ILoggingRepository loggingRepository, 
            IAuthorizationService authorization,
            ILocalizationRepository localization,
            IMembershipService membershipService)
        {
            _hotelsRepository = hotelsRepository;
            _loggingRepository = loggingRepository;
            _authorization = authorization;
            _localization = localization;
            _membershipService = membershipService;
        }

        [HttpGet]
        public IEnumerable<HotelViewModel> Get()
        {
            var returnedHotels = Enumerable.Empty<HotelViewModel>();

            try
            {
                IEnumerable<Hotel> hotels = _hotelsRepository
                    .GetAll()
                    .OrderBy(p => p.Id);

                returnedHotels = 
                    Mapper.Map<IEnumerable<Hotel>, IEnumerable<HotelViewModel>>(hotels);
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(
                    new Error()
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        DateCreated = DateTime.Now
                    });
                _loggingRepository.Commit();
            }

            return returnedHotels;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            HotelViewModel hotelVM = null;

            try
            {
                if (await _authorization.AuthorizeAsync(User, "OwnerOnly"))
                {
                    User user = _membershipService.GetUserByPrinciples(User);
                    if (!user.Hotels.Any(e => e.Id == id))
                    {
                        var codeResult = new CodeResult(403);
                        return new ObjectResult(codeResult);
                    }

                    Hotel hotel = await _hotelsRepository.GetSingleAsync(id);
                    hotelVM = Mapper.Map<Hotel, HotelViewModel>(hotel);
                }
                else
                {
                    var codeResult = new CodeResult(401);
                    return new ObjectResult(codeResult);
                }
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(
                    new Error()
                    {
                        Message = ex.Message,
                        StackTrace = ex.StackTrace,
                        DateCreated = DateTime.Now
                    });
                _loggingRepository.Commit();
            }

            return new ObjectResult(hotelVM);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody]HotelViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelCreationResult = null;

            try
            {
                if (await _authorization.AuthorizeAsync(User, "OwnerOnly"))
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

                    User user = _membershipService.GetUserByPrinciples(User);
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
                else
                {
                    var codeResult = new CodeResult(401);
                    return new ObjectResult(codeResult);
                }
            }
            catch (Exception ex)
            {
                hotelCreationResult = new Result()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            result = new ObjectResult(hotelCreationResult);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            int id, 
            [FromBody]HotelViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelEditionResult = null;

            try
            {
                if (await _authorization.AuthorizeAsync(User, "OwnerOnly"))
                {
                    if (!ModelState.IsValid)
                    {
                        throw new Exception("Correct data before editing");
                    }

                    User user = _membershipService.GetUserByPrinciples(User);
                    Hotel hotel = _hotelsRepository.GetSingle(id);
                    if (!user.Hotels.Any(h => h.Id == id))
                    {
                        var codeResult = new CodeResult(403);
                        return new ObjectResult(codeResult);
                    }

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
                else
                {
                    return HttpUnauthorized();
                }
            }
            catch (Exception ex)
            {
                hotelEditionResult = new Result()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            result = new ObjectResult(hotelEditionResult);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelDeletionResult = null;
            
            try
            {
                if (await _authorization.AuthorizeAsync(User, "OwnerOnly"))
                {
                    User user = _membershipService.GetUserByPrinciples(User);
                    Hotel hotel = user.Hotels.FirstOrDefault(h => h.Id == id);
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
                        var codeResult = new CodeResult(403);
                        return new ObjectResult(codeResult);
                    }
                }
                else
                {
                    HttpUnauthorized();
                }
            }
            catch (Exception ex)
            {
                hotelDeletionResult = new Result()
                {
                    Succeeded = false,
                    Message = ex.Message
                };

                _loggingRepository.Add(new Error() {
                    Message = ex.Message,
                    StackTrace = ex.StackTrace,
                    DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            result = new ObjectResult(hotelDeletionResult);
            return result;
        }
    }
}
