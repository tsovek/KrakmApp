using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;
using KrakmApp.ViewModels;

namespace KrakmApp.Core.Services
{
    public interface IObjectsService
    {
        ObjectsViewModel GetObjectsViewModel(int userId);
    }

    public class ObjectsService : IObjectsService
    {
        private IEntertainmentRepository _entertainemntsRepo;
        private IMonumentRepository _monuments;
        private IPartnersRepository _partnersRepo;

        public ObjectsService(
            IMembershipService membershipService,
            ILoggingRepository loggingRepo,
            IMonumentRepository monumentsRepo,
            IEntertainmentRepository entertainmentRepo,
            IPartnersRepository partnersRepo)
        {
            _monuments = monumentsRepo;
            _entertainemntsRepo = entertainmentRepo;
            _partnersRepo = partnersRepo;
        }

        public ObjectsViewModel GetObjectsViewModel(int userId)
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
                                 .Where(partner => partner.UserId == userId));
            return new ObjectsViewModel()
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
        }
    }
}
