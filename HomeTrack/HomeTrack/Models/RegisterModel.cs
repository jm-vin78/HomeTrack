using DAL;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomeTrack.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя должно быть заполнено.")]
        [Display(Name="Ваше имя *")]
        [RegularExpression(@"^[a-zA-Z]+|[а-яА-ЯёЁ]+$", ErrorMessage ="Разрешено использовать только буквы.")]
        [MaxLength(256, ErrorMessage = "Длина значения не может превышать 256 символов.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Телефон должен быть заполнен.")]
        [Display(Name="Ваш телефон (без +7) *")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Разрешены только цифры.")]
        [StringLength(10, MinimumLength=10, ErrorMessage = "Длина значения должна составлять 10 символов.")]
        public string Phone { get; set; }

        [Display(Name="Адрес электронной почты")]
        [EmailAddress(ErrorMessage = "Адрес электронной почты неверный.")]
        [MaxLength(320, ErrorMessage = "Длина значения адреса электронной почты не должен превышать 320 символов.")]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Необходимо ввести пароль.")]
        [Display(Name="Пароль *")]
        [MaxLength(20, ErrorMessage = "Длина пароля не должна превышать 20 символов.")]
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
