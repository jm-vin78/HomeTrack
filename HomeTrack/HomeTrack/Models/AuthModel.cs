using DAL;
using DAL.Entities;
using HomeTrack.Helpers;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeTrack.Models
{
    public class AuthModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Телефон должен быть заполнен.")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль должен быть заполнен.")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        /// <summary>
        /// Check if user with given credentials exists
        /// </summary>
        public bool CheckUser(AppDbContext context, out UserEntity user)
        {
            user = null;

            var passwordHash = Password.ComputeHashMD5();
            user = context.Users
                .Where(u => u.Phone == Phone && u.PasswordHash == passwordHash)
                .SingleOrDefault();

            return user != null;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        public Task AuthAsync(HttpContext httpContext, UserEntity user)
        {
            return httpContext.AuthUserAsync(user);
        }
    }
}