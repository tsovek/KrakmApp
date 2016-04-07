using System;
using System.Security.Claims;
using KrakmApp.Core.Repositories.Base;
using KrakmApp.Entities;

namespace KrakmApp.Core.Services
{
    public class OwnershipService : IOwnershipService
    {
        IMembershipService _membershipService;

        public OwnershipService(
            IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        public bool IsUserOwnerOfHotel(ClaimsPrincipal claims, Hotel hotel)
        {
            User user = _membershipService.GetUserByPrinciples(claims);
            return hotel.User == user;
        }

        public bool IsUserOwnerOfPartner(ClaimsPrincipal claims, Partner partner)
        {
            User user = _membershipService.GetUserByPrinciples(claims);
            return partner.User == user;
        }

        public bool IsUserOwnerOfRoute(ClaimsPrincipal claims, Route route)
        {
            throw new NotImplementedException();
        }

    }
}
