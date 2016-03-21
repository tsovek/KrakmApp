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

        public HotelsController(
            IHotelRepository hotelsRepository, 
            ILoggingRepository loggingRepository, 
            IAuthorizationService authorization)
        {
            _hotelsRepository = hotelsRepository;
            _loggingRepository = loggingRepository;
            _authorization = authorization;
        }

        // GET: api/values
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
        public async Task<HotelViewModel> Get(int id)
        {
            HotelViewModel hotelVM = null;

            try
            {
                Hotel hotel = await _hotelsRepository.GetSingleAsync(id);
                hotelVM = Mapper.Map<Hotel, HotelViewModel>(hotel);
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

            return hotelVM;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]HotelViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result hotelCreationResult = null;

            try
            {
                if (await _authorization.AuthorizeAsync(User, "AdminOnly"))
                {
                    if (!ModelState.IsValid)
                    {
                        throw new Exception("Correct data before adding");
                    }

                    _hotelsRepository.Add(value, User);
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

                //_loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                //_loggingRepository.Commit();
            }

            result = new ObjectResult(hotelCreationResult);
            return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
