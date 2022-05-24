using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using kizilayKlubuOrnek-master.Models;
    public class YetkiController : Controller
    
    { private UserManager<AppUser> userManager;
    private RoleManager<IdentityRole> roleManager;
        public YetkiController(UserManager<AppUser> _userManager,RoleManager<IdentityRole> _roleManager)
        {
            userManager =_userManager;
            roleManager =_roleManager;
        }

        public IActionResult Index()
        {
            var yetkiler=roleManager.Roles.ToList();
            return View(yetkiler);
        }
        public IActionResult Yeni()
        {

          //TODO: Implement Realistic Implementation
          return View();
        }
        [HttpPost]
        public async Task<IActionResult> Yeni(string Ad)
        {
            if(string.IsNullOrEmpty(Ad)){
                ViewBag.Hata="Yetki adı boş bırakılamaz";
                return View();
            }
            var sonuc=await roleManager.CreateAsync(new IdentityRole() { Name = Ad});
            if(sonuc.Succeeded) {
                return RedirectToAction(nameof(Index));
            } else {
                ViewBag.Hata=sonuc.Errors.First().Description;
            }
            return View();
        }
        public async Task<IActionResult> Duzenle(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if( role== null) return NotFound();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> Duzenle(IdentityRole m)
        {
            if(ModelState.IsValid){
                var sonuc= await roleManager.UpdateAsync(m);
                if(!sonuc.Succeeded){
                    ModelState.AddModelError("Name",
                    string.Join("<br>",
                    sonuc.Errors.Select(x => x.Description).ToList()));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        
    }
