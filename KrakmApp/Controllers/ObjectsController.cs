using System.Collections.Generic;
using System.Linq;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KrakmApp.Entities;
using KrakmApp.Core.Services;

namespace KrakmApp.Controllers
{
    [Route("api/objects")]
    public class ObjectsController : BaseController
    {
        IObjectsService _objectsService;

        public ObjectsController(
            IMembershipService membershipService, 
            ILoggingRepository loggingRepo,
            IObjectsService objectsService)
            : base(membershipService, loggingRepo)
        {
            _objectsService = objectsService;
        }

        [HttpGet]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get()
        {
            ObjectsViewModel objectsViewModel = 
                _objectsService.GetObjectsViewModel(GetUser().Id);
            return new ObjectResult(objectsViewModel);
        }
    }
}
