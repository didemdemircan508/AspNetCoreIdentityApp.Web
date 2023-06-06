using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignInViewModel
    {

        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır")]
        [Required(ErrorMessage = "EMail Boş Bırakılamaz")]
        [Display(Name = "Email:")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre:")]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public  string? Password { get; set; }

        [Display(Name = "Beni Hatırla:")]
        public bool RememberMe { get; set; }

    }
}
