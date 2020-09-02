using DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HomeTrack.Helpers
{
    public static class AuthHelper
    {
        private const string PhoneClaimKey = "Phone";
        private const string NameClaimKey = "Name";
        private const string UserIdClaimKey = "UserId";

        public static Task AuthUserAsync(this HttpContext context, UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(PhoneClaimKey, user.Phone),
                new Claim(NameClaimKey, user.Name),
                new Claim(UserIdClaimKey, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            return context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

        public static long GetCurrentUserId(this HttpContext context)
        {
            var userId = Int64.Parse(context.User.Claims.Single(x => x.Type == UserIdClaimKey).Value);
            return userId;
        }

        public static string GetCurrentUserName(this HttpContext context)
        {
            var userName = context.User.Claims.Single(x => x.Type == NameClaimKey).Value;
            return userName;
        }
    }
}
