using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı Boş Bırakılamaz")]
        [Display(Name = "Kullanıcı Adı:")]
        public  string UserName   { get; set; }

        [EmailAddress(ErrorMessage ="Email Formatı Yanlıştır")]
        [Required(ErrorMessage = "Mail Boş Bırakılamaz")]
        [Display(Name = "Email:")]
        public  string Email  { get; set; }

        [Required(ErrorMessage = "Telefon Boş Bırakılamaz")]
        [Display(Name = "Telefon:")]
        public  string Phone { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre:")]
        [Required(ErrorMessage = "Şifre Boş Bırakılamaz")]
        public  string Password   { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre Tekrar:")]
        [Required(ErrorMessage = "Şifre Tekrar AlanıBoş Bırakılamaz")]
        [Compare(nameof(Password),ErrorMessage ="Girmiş Olduğunuz Şifre Aynı Değildir")]
        public string PasswordConfirm { get; set; }



    }
}
