using DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HomeTrack.Models
{
    public class RegisterModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "RegisterModel_NameRequired")]
        [DisplayName("RegisterModel_Name")]
        [RegularExpression(@"^[a-zA-Z]+|[а-яА-ЯёЁ]+$", ErrorMessage = "RegisterModel_NameOnlyLetters")]
        [MaxLength(256, ErrorMessage = "RegisterModel_NameLength")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "RegisterModel_PhoneRequired")]
        [DisplayName("RegisterModel_Phone")]
        [RegularExpression(@"^\d+$", ErrorMessage = "RegisterModel_PhoneOnlyNumbers")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "RegisterModel_PhoneLength")]
        public string Phone { get; set; }

        [DisplayName("RegisterModel_Email")]
        [EmailAddress(ErrorMessage = "RegisterModel_EmailNotValid")]
        [MaxLength(320, ErrorMessage = "RegisterModel_EmailLength")]
        public string Email { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "RegisterModel_PasswordRequired")]
        [DisplayName("RegisterModel_Password")]
        [MaxLength(20, ErrorMessage = "RegisterModel_PasswordLength")]
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
