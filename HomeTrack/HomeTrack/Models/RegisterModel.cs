using DAL;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomeTrack.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя должно быть заполнено.")]
        [Display(Name="Ваше имя *")]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Телефон должен быть заполнен.")]
        [Display(Name="Ваш телефон *")]
        [MaxLength(10, ErrorMessage = "Максимальная длина значения 10.")]
        public string Phone { get; set; }

        [Display(Name="Адрес электронной почты")]
        [MaxLength(256)]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо ввести пароль.")]
        [Display(Name="Пароль *")]
        [MaxLength(20)]
        public string Password { get; set; }

        public bool CheckPhone(AppDbContext context)
        {
            var user = context.Users
                .Where(u => u.Phone == Phone)
                .SingleOrDefault();

            return user != null;
        }

        public bool CheckEmail(AppDbContext context)
        {
            var user = context.Users
                .Where(u => u.Email == Email)
                .SingleOrDefault();

            return user != null;
        }
    }
}
