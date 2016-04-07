using System.Security.Claims;
using KrakmApp.Entities;

namespace KrakmApp.Core.Repositories.Base
{
    public interface IOwnershipService
    {
        bool IsUserOwnerOfHotel(ClaimsPrincipal claims, Hotel hotel);
        bool IsUserOwnerOfPartner(ClaimsPrincipal claims, Partner partner);
        bool IsUserOwnerOfRoute(ClaimsPrincipal claims, Route route);
    }
}
