using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ForgetPasswordViewModel
    {


        [EmailAddress(ErrorMessage = "Email Formatı Yanlıştır")]
        [Required(ErrorMessage = "EMail Boş Bırakılamaz")]
        [Display(Name = "Email:")]
        public string? Email { get; set; }
    }
}
