using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
namespace PruebaLogin.Helper
{
    public static class HelperClaim
    {
        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
