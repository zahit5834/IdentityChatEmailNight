using IdentityChatEmailNight.Context;
using IdentityChatEmailNight.Entites;
using IdentityChatEmailNight.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmailNight.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ProfileDetail()
        {
            ViewBag.Kategori = "Profil";
            ViewBag.AltKategori = "Profil Detayları";
            
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = new UpdateProfileViewModel
            {
                Name = values.Name,
                Surname = values.Surname,
            };
            ViewBag.GonderilenMesaj = _context.Messages.Where(x => x.SenderEmail==values.Email).Count();
            ViewBag.AlinanMesaj = _context.Messages.Where(x => x.ReceiverEmail == values.Email).Count();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> ProfileDetail(UpdateProfileViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            user.Name = p.Name;
            user.Surname = p.Surname;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (p.Password != null && p.NewPassword != null)
                {
                    var resultt = await _userManager.ChangePasswordAsync(user, p.Password, p.NewPassword);
                    if (resultt.Succeeded)
                    {
                        return RedirectToAction("Logout", "Login");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return RedirectToAction("ProfileDetail");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction("ProfileDetail");
        }
    }
}
