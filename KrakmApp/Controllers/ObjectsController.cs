using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using KrakmApp.Entities;

namespace KrakmApp.Controllers
{
    [Route("api/objects")]
    public class ObjectsController : BaseController
    {
        private IEntertainmentRepository _entertainemntsRepo;
        private IMonumentRepository _monuments;
        private IPartnersRepository _partnersRepo;

        public ObjectsController(
            IMembershipService membershipService, 
            ILoggingRepository loggingRepo,
            IMonumentRepository monumentsRepo,
            IEntertainmentRepository entertainmentRepo,
            IPartnersRepository partnersRepo)
            : base(membershipService, loggingRepo)
        {
            _monuments = monumentsRepo;
            _entertainemntsRepo = entertainmentRepo;
            _partnersRepo = partnersRepo;
        }

        [HttpGet]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get()
        {
            var enterSingles = Mapper.Map<
                IEnumerable<Entertainment>, 
                IEnumerable<SingleObjectViewModel>>(
                    _entertainemntsRepo.AllIncluding(enter => enter.Localization));

            var monumentSingles = Mapper.Map<
                IEnumerable<Monument>,
                IEnumerable<SingleObjectViewModel>>(
                    _monuments.AllIncluding(mon => mon.Localization));

            var partnersSingles = Mapper.Map<
                IEnumerable<Partner>,
                IEnumerable<SingleObjectViewModel>>(
                    _partnersRepo.AllIncluding(partner => partner.Localization)
                                 .Where(partner => partner.UserId == GetUser().Id));
            var objectsVM = new ObjectsViewModel()
            {
                Objects = new List<GroupObjectViewModel>
                {
                    new GroupObjectViewModel()
                    {
                        SingleObjects = enterSingles,
                        Type = ObjectType.Entertainments.ToString()
                    },
                    new GroupObjectViewModel()
                    {
                        SingleObjects = monumentSingles,
                        Type = ObjectType.Monuments.ToString()
                    },
                    new GroupObjectViewModel()
                    {
                        SingleObjects = partnersSingles,
                        Type = ObjectType.Partners.ToString()
                    },
                }
            };

            return new ObjectResult(objectsVM);
        }
    }
}
