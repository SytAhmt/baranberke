using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using YesilayKlubu.Models;

namespace YesilayKlubu.Controllers
{
    public class HesapController : Controller
    {
        //Oturum işlerini yapmamızı sağlar
        private SignInManager<AppUser> signInManager;
        //Kullanıcı işlemlerini yapmamızı sağlar
        private UserManager<AppUser> userManager;
        public HesapController(SignInManager<AppUser> _s, UserManager<AppUser> _u)
        {
            signInManager = _s;
            userManager = _u;
        }
        public IActionResult Kayit()
        {
            // TODO: Your code here
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Kayit(Kayit m)
        {
            if (ModelState.IsValid)
            {
                var kullanici = new AppUser
                {
                    UserName = m.Email,
                    Ad = m.Ad,
                    Soyad = m.Soyad,
                    Email = m.Email,
                    OkulNumarasi = m.OkulNumarasi
                };
                var sonuc = await userManager.CreateAsync(kullanici, m.Sifre);
                if (sonuc.Succeeded)
                {
                    ViewBag.OK = "Üye kaydınız başarıyla gerçekleşti, Lütfen giriş yapınız";
                    string icerik = $"Sayın {m.Ad} {m.Soyad}  <br> Sitemize kayıt olduğunuz için teşekkür ederiz ";
                    icerik += $" Giriş yapmak için {this.Request.Scheme}://{this.Request.Host}/Hesap/";
                    Islemler.MailGonder("Hoşgeldin", icerik, m.Email);
                }
                else
                {
                    //kayıt hata ile sonuçlanırsa ön tarafa 
                    //göstermesi için hata ekle
                    ModelState.AddModelError("", string.Join("<br>",
                    sonuc
                    .Errors
                    .Select(x => x.Description)
                    .ToList()));
                }
            }
            return View(m);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(giris m)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(m.Email, m.Sifre, true, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Hesap kilitli");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş başarısız");
                    }

                }
            }
            return View();
        }

        public IActionResult Cikis()
        {
            signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
        
                public async Task<IActionResult> SifremiUnuttum(string Email)
        {
            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.Hata = "Email alanı boş bırakılamaz";
                return View();
            }

            AppUser kullanici = await userManager.FindByEmailAsync(Email);
            if (kullanici == null)
            {
                ViewBag.Hata = "Girilen email ile sistemde kullanıcı yok";
                return View();
            }

            //şifre sıfırlama linkini oluştur
            var token = await userManager.GeneratePasswordResetTokenAsync(kullanici);
            var link = Url.Action(nameof(SifreSifirla), "Hesap",
            new { token, email = kullanici.Email }, Request.Scheme);
            //email ile gönderiliyor
            string emailmesaj=$"Merhaba {kullanici.Ad} <br> Şifrenizi sıfırlamak için aşağıdaki bağlantıya tıklayınız <br>";
            Islemler.MailGonder("Şifre sıfırlama bağlantısı", emailmesaj+link, kullanici.Email);
            string mesaj = $" <i class='bi bi-check'></i>Sıfırlama  bağlantısı <b>{Email}</b> adresine gönderildi <br>";
            
            string l = Url.ActionLink(nameof(Index), "Hesap", null,Request.Scheme);
            mesaj+=$"<a href='{l}'>Giriş yapın</a>";
            return View("Views/Shared/_basarili.cshtml", mesaj);
            }
        public IActionResult SifreSifirla()
        {
            return View();
        }
         [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SifreSifirla(SifreSifirla m)
        {
            if (!ModelState.IsValid)
                return View(m);

            var kullanici = await userManager.FindByEmailAsync(m.Email);
            if (kullanici == null)
                RedirectToAction(nameof(SifremiUnuttum));

            var sifirla = await userManager.ResetPasswordAsync(kullanici, m.token, m.Sifre);
            if (!sifirla.Succeeded)
            {
                foreach (var error in sifirla.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return View("Views/Shared/_basarili.cshtml", "Şifreniz başarıyla değiştirildi");
        }
    }
}