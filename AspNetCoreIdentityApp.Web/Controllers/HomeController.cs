using AspNetCoreIdentityApp.Web.Models;
using AspNetCoreIdentityApp.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using AspNetCoreIdentityApp.Web.Extensions;
using AspNetCoreIdentityApp.Web.Services;

namespace AspNetCoreIdentityApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> UserManager, SignInManager<AppUser> signInManager, IEmailService emailService)
        {
            _logger = logger;
            _UserManager = UserManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        public IActionResult SignUp()
        {
            return View();


        }

        public IActionResult ForgetPassword()
        { 
            return View();
        
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            var hasUser = await _UserManager.FindByEmailAsync(request.Email!);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır");
                return View();
            
            }
            string passwordResetToken = await _UserManager.GeneratePasswordResetTokenAsync(hasUser);

            string passwordResetLink = Url.Action("ResetPassword", "Home", new
            {
                userId=hasUser.Id,
                Token= passwordResetToken


            },HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmail(passwordResetLink!, hasUser.Email!);
            TempData["SuccessMessage"] = "Şifre Sıfırlama emaili adresine gönderilmiştir";
            return RedirectToAction(nameof(ForgetPassword));



            //https://localhost:44381?userId=12213&token=aaajsjsjjssjsjjs

        }

        public  IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

           


            return View();
        
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];

            if (userId == null || token == null)
            {
                throw new Exception("Bir Hata Meydana Geldi");
            }



            var hasUser = await _UserManager.FindByIdAsync(userId.ToString()!);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanıcı Bulunamamıştır");
               


            }

            IdentityResult result = await _UserManager.ResetPasswordAsync(hasUser!, token.ToString()!, request.Password);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "Şifreniz Başarıyla Yenilenmiştir.";

            }
            else
            {
                ModelState.AddModelErrorList(result.Errors.Select(x => x.Description).ToList());
                

            }


            return View();

        }

        public IActionResult SignIn()
        {
            return View();
        
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request,string? returnUrl = null)
        {
            
            returnUrl = returnUrl ?? Url.Action("Index", "Home");
            var hasUser = await _UserManager.FindByEmailAsync(request.Email!);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış");
                return View();
            
            }

            var signInResult = await  _signInManager.PasswordSignInAsync(hasUser, request.Password!, request.RememberMe, true);
            if (signInResult.Succeeded)
            {
                return Redirect(returnUrl!);

            
            }

            if (signInResult.IsLockedOut) 
            {

                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriş yapamazsınız" });
                return View();
            }

            var unsuccesCount = await _UserManager.GetAccessFailedCountAsync(hasUser);

            ModelState.AddModelErrorList(new List<string>() { "Email Veya Şifre yanlış" ,$"Bşarısız Giriş Sayısı={unsuccesCount}"});

            return View();



        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
         

            if (!ModelState.IsValid) 
            {
                return View();
            }

            var identityResult = await _UserManager.CreateAsync(new AppUser()
            {
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Email = request.Email
            }, request.PasswordConfirm);


            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Kayıt İşlemi Başarılır";
                return RedirectToAction(nameof(HomeController.SignUp));

            }
            ModelState.AddModelErrorList(identityResult.Errors.Select(x=>x.Description).ToList());
            //foreach(IdentityError item in identityResult.Errors)
            //{
            //    ModelState.AddModelError(string.Empty,item.Description);

            //}
            return View();


        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}