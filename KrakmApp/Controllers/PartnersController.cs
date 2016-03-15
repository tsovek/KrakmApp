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
    public class PartnersController : Controller
    {
        IAuthorizationService _authorizationService;
        IPartnersRepository _partnersRepository;
        ILoggingRepository _loggingRepository;

        public PartnersController(
            IAuthorizationService authorizationService,
            IPartnersRepository partnersRepository,
            ILoggingRepository loggingRepository)
        {
            _authorizationService = authorizationService;
            _partnersRepository = partnersRepository;
            _loggingRepository = loggingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var partnersVM = Enumerable.Empty<PartnerViewModel>();

            try
            {
                if (await _authorizationService.AuthorizeAsync(User, "AdminOnly"))
                {
                    IEnumerable<Partner> partners = _partnersRepository
                        .GetAll()
                        .OrderBy(a => a.Id);

                    partnersVM = Mapper
                        .Map<IEnumerable<Partner>, IEnumerable<PartnerViewModel>>(partners);
                }
                else
                {
                    var codeResult = new CodeResult(401);
                    return new ObjectResult(codeResult);
                }
            }
            catch (Exception ex)
            {
                _loggingRepository.Add(new Error() { Message = ex.Message, StackTrace = ex.StackTrace, DateCreated = DateTime.Now });
                _loggingRepository.Commit();
            }

            return new ObjectResult(partnersVM);
        }
    }
}
