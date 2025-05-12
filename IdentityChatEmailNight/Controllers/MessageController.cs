using IdentityChatEmailNight.Context;
using IdentityChatEmailNight.Entites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityChatEmailNight.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly EmailContext _context;
        private readonly UserManager<AppUser> _userManager;

        public MessageController(EmailContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> Inbox(string search)
        {

            ViewBag.Kategori = "Mesajlar";
            ViewBag.AltKategori = "Gelen Kutusu";
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Message> messages;
            if (!string.IsNullOrWhiteSpace(search))
            {
                messages = _context.Messages.Where(x=>x.ReceiverEmail==values.Email).Where(x=> x.Subject.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                messages = _context.Messages.Where(x => x.ReceiverEmail == values.Email).ToList();
                
            }
            messages = messages.OrderByDescending(x => x.SendDate).ToList();
            return View(messages);
        }
        public async Task<IActionResult> SendBox(string search)
        {
            ViewBag.Kategori = "Mesajlar";
            ViewBag.AltKategori = "Giden Kutusu";
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            List<Message> messages;
            if (!string.IsNullOrWhiteSpace(search))
            {
                messages = _context.Messages.Where(x => x.SenderEmail == values.Email).Where(x => x.Subject.ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                messages = _context.Messages.Where(x => x.SenderEmail == values.Email).ToList();

            }
            messages = messages.OrderByDescending(x => x.SendDate).ToList();
            return View(messages);
        }
        [HttpGet]
        public IActionResult CreateMessage()
        {
            ViewBag.Kategori = "Mesajlar";
            ViewBag.AltKategori = "Yeni Mesaj";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage(Message message)
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            message.SenderEmail = values.Email;
            message.IsRead = false;
            message.SendDate = DateTime.Now;

            _context.Messages.Add(message);
            _context.SaveChanges();

            ViewBag.ShowSuccess = true;
            ViewBag.SuccessMessage = "Mesaj başarıyla gönderildi!";
            return View(); // Burada redirect yok! SweetAlert'ten sonra yönlendirme yapılacak
        }
        public IActionResult MessageDetail(int id)
        {
            ViewBag.Kategori = "Mesajlar";
            ViewBag.AltKategori = "Mesaj Detayları";
            var message = _context.Messages.Find(id);
            return View(message);
        }
    }
}
