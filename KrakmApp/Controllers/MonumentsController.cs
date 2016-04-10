using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KrakmApp.Core.Repositories.Base;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/[controller]")]
    public class MonumentsController : BaseController
    {
        IMonumentRepository _monumentsRepository;
        ILoggingRepository _loggingRepository;
        IAuthorizationService _authorization;
        ILocalizationRepository _localization;
        IMembershipService _membership;

        public MonumentsController(
            IMonumentRepository monumentsRepository,
            ILoggingRepository loggingRepository,
            IAuthorizationService authorization,
            ILocalizationRepository localization,
            IMembershipService membership)
            : base(membership, loggingRepository)
        {
            _monumentsRepository = monumentsRepository;
            _loggingRepository = loggingRepository;
            _authorization = authorization;
            _localization = localization;
            _membership = membership;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
