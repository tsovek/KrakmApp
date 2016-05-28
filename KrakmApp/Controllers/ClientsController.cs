using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using KrakmApp.Core.Common;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Core.Services;
using KrakmApp.Entities;
using KrakmApp.ViewModels;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace KrakmApp.Controllers
{
    [Route("api/[controller]")]
    public class ClientsController : BaseController
    {
        private IClientRepository _clients;
        private IRandomGeneratorService _randomGen;

        public ClientsController(
            IMembershipService membership, 
            ILoggingRepository logging,
            IClientRepository clients,
            IRandomGeneratorService randomGen)
            : base(membership, logging)
        {
            _clients = clients;
            _randomGen = randomGen;
        }

        [HttpGet]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get()
        {
            var clientVM = Enumerable.Empty<ClientViewModel>();

            try
            {
                IEnumerable<Hotel> hotels = GetUser().Hotels;
                IEnumerable<Client> clients = _clients
                    .GetAll()
                    .Where(client => hotels.Any(hotel => hotel.Id == client.HotelId));

                clientVM = Mapper.Map<
                    IEnumerable<Client>,
                    IEnumerable<ClientViewModel>>(clients);
            }
            catch (Exception ex)
            {
                LogFail(ex);
                return HttpBadRequest();
            }

            return new ObjectResult(clientVM);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Get(int id)
        {
            ClientViewModel clientVM = null;

            try
            {
                Client client = _clients.GetSingle(id);
                IEnumerable<Hotel> hotels = GetUser().Hotels;
                if (client == null ||
                    !hotels.Any(hotel => hotel.Id == client.HotelId))
                {
                    return HttpBadRequest();
                }

                clientVM = Mapper.Map<Client, ClientViewModel>(client);
            }
            catch (Exception ex)
            {
                LogFail(ex);
                return HttpBadRequest();
            }

            return new ObjectResult(clientVM);
        }

        [HttpPost]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Post(
            [FromBody]ClientViewModel value)
        {
            IActionResult result = new ObjectResult(false);
            ClientResult clientCreationResult = null;

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Correct data before adding");
                }

                User user = GetUser();
                var client = new Client
                {
                    Name = value.Name,
                    Activated = false,
                    HotelId = value.HotelId,
                    CheckIn = value.CheckIn,
                    CheckOut = value.CheckOut,
                    UniqueKey = _randomGen.GenerateRandomUniqueKey()
                };
                _clients.Add(client);
                _clients.Commit();

                clientCreationResult = new ClientResult()
                {
                    Succeeded = true,
                    Message = "Adding succeeded",
                    UniqueId = client.UniqueKey
                };
            }
            catch (Exception ex)
            {
                Result res = GetFailedResult(ex);
                return new ObjectResult(res);
            }

            result = new ObjectResult(clientCreationResult);
            return result;
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "OwnerOnly")]
        public IActionResult Delete(int id)
        {
            IActionResult result = new ObjectResult(false);
            Result clientDeletionResult = null;

            try
            {
                IEnumerable<Hotel> hotels = GetUser().Hotels;
                Predicate<int> canDelete = 
                    hotelId => hotels.Any(hotel => hotel.Id == hotelId);
                Client client = _clients
                    .GetAll()
                    .SingleOrDefault(e => e.Id == id && canDelete(e.HotelId));
                if (client != null)
                {
                    _clients.Delete(client);
                    _clients.Commit();

                    clientDeletionResult = new Result()
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
                clientDeletionResult = GetFailedResult(ex);
            }

            result = new ObjectResult(clientDeletionResult);
            return result;
        }
    }
}
