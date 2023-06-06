using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {

        [DataType(DataType.Password)]
        [Display(Name = " Yeni Şifre:")]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar:")]
        [Required(ErrorMessage = "Şifre Tekrar AlanıBoş Bırakılamaz")]
        [Compare(nameof(Password), ErrorMessage = "Girmiş Olduğunuz Şifre Aynı Değildir")]
        public string PasswordConfirm { get; set; }
    }
}
