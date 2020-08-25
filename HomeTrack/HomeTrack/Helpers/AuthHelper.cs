using DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeTrack.Helpers
{
    public static class AuthHelper
    {
        private const string PhoneClaimKey = "Phone";
        private const string NameClaimKey = "Name";

        public static Task AuthUserAsync(this HttpContext context, UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(PhoneClaimKey, user.Phone),
                new Claim(NameClaimKey, user.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            return context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
