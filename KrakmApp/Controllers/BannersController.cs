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
    public class BannersController : BaseController
    {
        private IBannersRepository _banners;

        public BannersController(
            IMembershipService membership,
            ILoggingRepository logging,
            IBannersRepository banners)
            : base(membership, logging)
        {
            _banners = banners;
        }

        [HttpGet]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get()
        {
            var bannerVM = Enumerable.Empty<BannerViewModel>();

            try
            {
                IEnumerable<Banner> banners = _banners
                    .GetAll()
                    .Where(e => e.UserId == GetUser().Id);
                    
                bannerVM = Mapper.Map<
                    IEnumerable<Banner>,
                    IEnumerable<BannerViewModel>>(banners);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(bannerVM);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get(int id)
        {
            BannerViewModel bannerVM = null;
            try
            {
                Banner banner = _banners
                    .GetAll()
                    .Where(e => e.UserId == GetUser().Id && e.Id == id)
                    .SingleOrDefault();

                if (banner == null)
                {
                    return HttpBadRequest();
                }

                bannerVM = Mapper.Map<Banner, BannerViewModel>(banner);
            }
            catch (Exception ex)
            {
                LogFail(ex);
            }

            return new ObjectResult(bannerVM);
        }

        [HttpPost]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Post(
            [FromBody]BannerViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            Result bannerCreationResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

                User user = GetUser();
                var banner = new Banner
                {
                    User = user,
                    Name = value.Name,
                    PhotoUrl = value.ImageUrl,
                    Description = value.Description,
                    Start = value.StartPromotion,
                    End = value.EndPromotion
                };
                _banners.Add(banner);
                _banners.Commit();

                bannerCreationResult = new Result()
                {
                    Succeeded = true,
                    Message = "Adding succeeded"
                };
            }
            catch (Exception ex)
            {
                bannerCreationResult = GetFailedResult(ex);
            }

            result = new ObjectResult(bannerCreationResult);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result bannerDeletionResult = null;

            try
            {
                Banner banner = _banners
                    .GetAll()
                    .SingleOrDefault(e => e.Id == id && e.UserId == GetUser().Id);
                if (banner != null)
                {
                    _banners.Delete(banner);
                    _banners.Commit();

                    bannerDeletionResult = new Result()
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
                bannerDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(bannerDeletionResult);
            return result;
        }
    }
}
