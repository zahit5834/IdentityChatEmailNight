using IdentityChatEmailNight.Context;
using IdentityChatEmailNight.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmailNight.ViewComponents
{
    public class _SidebarComponentPartial : ViewComponent
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public _SidebarComponentPartial(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            //ViewBag.ReceiverMessageCount = 0;
            //ViewBag.SenderMessageCount = 0;
            //var user= await _userManager.FindByNameAsync(User.Identity.Name);
            //var email = user.Email;
            //ViewBag.ReceiverMessageCount = _context.Messages.Count(x => x.ReceiverEmail == email && x.IsRead == false);
            //ViewBag.SenderMessageCount = _context.Messages.Count(x => x.SenderEmail == email && x.IsRead == false);
            return View();
        }
    }
}
