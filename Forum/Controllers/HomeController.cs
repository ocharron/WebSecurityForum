using Forum.Context;
using Forum.Entities;
using Forum.Models.Home;
using Forum.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace Forum.Controllers
{
    [Authorize]
    public class HomeController(UserManager<User> userManager, ApplicationDbContext context) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        public async Task<IActionResult> Index(int? page)
        {
            // Manage the current page
            if (page is null || page <= 0)
                page = 1;

            int skip = ((int)page - 1) * Constantes.PAGE_SIZE;

            List<Message> messagesPage = _context.Messages
                .Include(m => m.Commentaires)
                .AsNoTracking()
                .OrderByDescending(m => m.Date)
                .Skip(skip)
                .Take(Constantes.PAGE_SIZE)
                .ToList();

            List<MessageVM> messages = messagesPage.Select(message => new MessageVM
            {
                Id = message.Id,
                UserName = _userManager.Users.FirstOrDefault(u => u.Id == message.UserId)?.UserName ?? string.Empty,
                Contenu = message.Contenu,
                Date = message.Date,
                Commentaires = message.Commentaires?.Select(commentaire => new CommentaireVM
                {
                    Id = commentaire.Id,
                    UserName = _userManager.Users.FirstOrDefault(u => u.Id == commentaire.UserId)?.UserName ?? string.Empty,
                    Contenu = commentaire.Contenu,
                    Date = commentaire.Date
                }).ToList()
            }).ToList();

            // Has admin permissions
            User? user = await _userManager.GetUserAsync(HttpContext.User);
            if (user is null)
                throw new InvalidOperationException("The user not supposed to be null because it is required to be logged in.");

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            ForumVM vm = new()
            {
                Messages = messages,
                NewMessage = new()
                {
                    Contenu = string.Empty
                },
                IsAdmin = userRoles.Contains(Constantes.ADMIN_ROLE),
                CurrentPage = (int)page,
                HasPreviousPage = skip >= 5,
                HasNextPage = _context.Messages.Count() > skip + Constantes.PAGE_SIZE
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewMessage(NewMessageVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Contenu))
                return View(vm);

            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
                throw new InvalidOperationException("The user that posted the new message was null.");

            Message newMessage = new()
            {
                Contenu = HttpUtility.HtmlEncode(vm.Contenu), // Encode to prevent scripting
                User = user
            };

            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(Guid messageId)
        {
            Message? messageToDelete = _context.Messages.Include(m => m.Commentaires).FirstOrDefault(m => m.Id == messageId);

            if (messageToDelete is null)
                throw new InvalidOperationException($"The message {messageId} was not found.");

            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
                throw new InvalidOperationException("The user that deleted the message was null.");

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (user.Id != messageToDelete.UserId && !userRoles.Contains(Constantes.ADMIN_ROLE))
                throw new UnauthorizedAccessException("The user do not have the permission to do delete that message.");

            foreach (Commentaire commentaire in messageToDelete.Commentaires!)
                _context.Commentaires.Remove(commentaire);

            _context.Messages.Remove(messageToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCommentaire(NewCommentaireVM vm)
        {
            if (string.IsNullOrWhiteSpace(vm.Contenu))
                return View(vm);

            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
                throw new InvalidOperationException("The user that posted the new message was null.");

            Message? message = _context.Messages.Find(vm.MessageId);

            if (message == null)
                throw new InvalidOperationException($"The message {vm.MessageId} that the comment was posted to was null.");

            Commentaire newCommentaire = new()
            {
                Contenu = HttpUtility.HtmlEncode(vm.Contenu), // Encode to prevent scripting
                User = user
            };

            message.Commentaires!.Add(newCommentaire);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCommentaire(Guid commentaireId)
        {
            Commentaire? commentaireToDelete = _context.Commentaires.FirstOrDefault(c => c.Id == commentaireId);

            if (commentaireToDelete is null)
                throw new InvalidOperationException($"The comment {commentaireId} was not found.");

            User? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user is null)
                throw new InvalidOperationException("The user that deleted the comment was null.");

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (user.Id != commentaireToDelete.UserId && !userRoles.Contains(Constantes.ADMIN_ROLE))
                throw new UnauthorizedAccessException("The user do not have the permission to do delete that comment.");

            _context.Commentaires.Remove(commentaireToDelete);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
