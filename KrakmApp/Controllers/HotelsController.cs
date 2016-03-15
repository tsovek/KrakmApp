using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        IHotelRepository _hotelsRepository;
        ILoggingRepository _loggingRepository;
        public HotelsController(
            IHotelRepository hotelsRepository, 
            ILoggingRepository loggingRepository)
        {
            _hotelsRepository = hotelsRepository;
            _loggingRepository = loggingRepository;
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

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
