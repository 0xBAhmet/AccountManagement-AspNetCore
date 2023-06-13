using AhmetBAYRAM_FinalProject.Entities;
using AhmetBAYRAM_FinalProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.Security.Claims;

namespace AhmetBAYRAM_FinalProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;



        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) {
                //login işlemleri
                string md5Salt=_configuration.GetValue<string>("AppSetting:MD5Salt");
                string saltedPassword=model.Password+md5Salt;
                string hashedPassword = saltedPassword.MD5();

                Users user = _databaseContext.Users.FirstOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);

                if (user!=null)
                {

                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username),"Kullanıcı Modu");
                    }

                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Name ?? string.Empty));
                    claims.Add(new Claim("Username", user.Username));

                    ClaimsIdentity identity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya Şifre Hatalı");
                }

            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Users.Any(x=>x.Username.ToLower()==model.Username.ToLower())) 
                {
                    ModelState.AddModelError(nameof(model.Username),"Kullanıcı adı daha önceden alınmıştır");
                    return View(model);
                }
                string md5Salt = _configuration.GetValue<string>("AppSetting:MD5Salt");
                string saltedPassword=model.Password+md5Salt;
                string hashedPassword = saltedPassword.MD5();
                //Register işlemleri
                Users user = new Users()
                {
                    Username = model.Username,
                    Password = hashedPassword,
                    Name = model.Name,
                };
                _databaseContext.Users.Add(user);
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Login));

            }
            return View(model);
        }

        public async Task<IActionResult> LogoutAsync() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Profile() 
        {
            Users user = _databaseContext.Users.FirstOrDefault(x => x.Username.ToLower() == @User.FindFirst("Username").Value.ToLower());
            
            return View(user); 
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateViewModel model)
        {

            string md5Salt = _configuration.GetValue<string>("AppSetting:MD5Salt");
            string saltedPassword = model.Currently_Password + md5Salt;
            string hashedPassword = saltedPassword.MD5();

            Users user = _databaseContext.Users.FirstOrDefault(x => x.Username.ToLower() == @User.FindFirst("Username").Value.ToLower() && x.Password == hashedPassword);
            if (user != null) 
            {   
                //şifre güncelleme ekranı

                string saltedPassword1 = model.Password + md5Salt;
                string hashedPassword1 = saltedPassword1.MD5();
                

                string saltedPassword2 = model.RePassword + md5Salt;
                string hashedPassword2 = saltedPassword2.MD5();
                //şifreler aynı değilse hata veriyor
                if (hashedPassword1!=hashedPassword2) {

                    ModelState.AddModelError("", "Şifreler uyuşmuyor");
                    return View(model);
                }

                user.Password = hashedPassword1;
                _databaseContext.Users.Update(user);
                _databaseContext.SaveChanges();
                //kaydettikten sonra çıkış yapıyoruz
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(Login));
            }
            else
            {
                ModelState.AddModelError("", "Şifre Hatalı");
            }

            return View(model);
        }
    }
}
