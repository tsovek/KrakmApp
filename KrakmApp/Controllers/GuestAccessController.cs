using System;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KrakmApp.Controllers
{
    [Route("api/mobile")]
    public class GuestAccessController : BaseController
    {
        IObjectsService _objectsService;
        IBannersRepository _bannersRepo;
        IAllRoutesFactory _allRoutesFactory;
        IHotelRepository _hotelRepo;
        IClientRepository _clientRepo;

        public GuestAccessController(
            IMembershipService membershipService, 
            ILoggingRepository loggingRepo,
            IObjectsService objectsService,
            IBannersRepository bannersRepo,
            IAllRoutesFactory allRoutesFactory,
            IHotelRepository hotelRepo,
            IClientRepository clientRepo) 
            : base(membershipService, loggingRepo)
        {
            _objectsService = objectsService;
            _bannersRepo = bannersRepo;
            _allRoutesFactory = allRoutesFactory;
            _hotelRepo = hotelRepo;
            _clientRepo = clientRepo;
        }

        // GET api/values/5
        [HttpGet("byHotelId")]
        public IActionResult Get(int hotelId, string key)
        {
            MobileObjectsViewModel viewModel = null;

            try
            {
                if (hotelId == 0 || string.IsNullOrWhiteSpace(key))
                {
                    return BadRequest();
                }

                Hotel hotel = _hotelRepo
                    .AllIncluding(e => e.Localization)
                    .SingleOrDefault(e => e.Id == hotelId);
                Client client = _clientRepo.GetSingle(e => e.UniqueKey == key);
                if (hotel == null || client == null)
                {
                    return NoContent();
                }

                var allRoutes = new AllRoutesViewModel()
                {
                    Routes = _allRoutesFactory.GetAllByUserId(hotel.UserId)
                };
                var allBanners = new AllBannersViewModel()
                {
                    Banners = Mapper.Map<
                        IEnumerable<Banner>,
                        IEnumerable<BannerViewModel>>(_bannersRepo.GetAllByUserId(hotel.UserId))
                };
                viewModel = new MobileObjectsViewModel()
                {
                    Objects = _objectsService.GetObjectsViewModel(hotel.UserId),
                    Client = Mapper.Map<Client, ClientViewModel>(client),
                    Hotel = Mapper.Map<Hotel, HotelViewModel>(hotel),
                    AllRoutes = allRoutes,
                    AllBanners = allBanners
                };
            }
            catch (Exception ex)
            {
                LogFail(ex);
                return BadRequest();
            }

            return new ObjectResult(viewModel);
        }
    }
}
