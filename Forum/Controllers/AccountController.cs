using Forum.Context;
using Forum.Entities;
using Forum.Models.Account;
using Forum.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Forum.Controllers
{
    [Authorize]
    public class AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<Guid>> roleManager, ApplicationDbContext context) : Controller
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signInManager = signInManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        private readonly ApplicationDbContext _context = context;

        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(LogInVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Verify if the user has been timed out in the last 10 minutes.
            bool timedOut = _context.SecurityLogs.Any(sl => sl.Username == vm.Username && sl.Date > DateTime.Now.AddMinutes(-Constantes.TIMEOUT_DURATION_IN_MINUTES) && sl.TimedOut == true);
            if (timedOut)
            {
                _context.SecurityLogs.Add(new SecurityLog()
                {
                    Username = vm.Username!,
                    TypeLog = TypeLog.LogInAttemptWhileTimedOut,
                    TimedOut = false
                });
                _context.SaveChanges();

                ModelState.AddModelError(string.Empty, "Too many attempts. Please try again in few minutes.");
                return View(vm);
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(vm.Username!, vm.Password! + Constantes.GLOBAL_PEPPER, false, false);

            if (!result.Succeeded)
            {
                // Timeout user for 5 minutes after 10 bad login attempts in a row.
                List<SecurityLog> logs = _context.SecurityLogs
                    .AsNoTracking()
                    .Where(sl => sl.Username == vm.Username && sl.Date > DateTime.Now.AddMinutes(-5))
                    .OrderByDescending(l => l.Date)
                    .Take(9)
                    .ToList();

                bool timeOutNow = false;
                if (logs.Count == 10)
                    timeOutNow = !logs.Any(l => l.TypeLog != TypeLog.LogInAttempt);

                _context.SecurityLogs.Add(new SecurityLog()
                {
                    Username = vm.Username!,
                    TypeLog = TypeLog.LogInAttempt,
                    TimedOut = timeOutNow
                });
                _context.SaveChanges();

                ModelState.AddModelError(string.Empty, "Log in failed. Please try again.");
                return View(vm);
            }

            _context.SecurityLogs.Add(new SecurityLog()
            {
                Username = vm.Username!,
                TypeLog = TypeLog.SuccessfulLogIn,
                TimedOut = false
            });
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            _context.SecurityLogs.Add(new SecurityLog()
            {
                Username = HttpContext.User.Identity!.Name!,
                TypeLog = TypeLog.LogOut,
                TimedOut = false
            });
            _context.SaveChanges();

            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception e)
            {
                _context.ExceptionLogs.Add(new ExceptionLog()
                {
                    Source = e.Source,
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    InnerStackTrace = e.InnerException?.StackTrace,
                    InnerMessage = e.InnerException?.Message
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            bool userExists = _userManager.Users.Any(u => u.NormalizedUserName == vm.UserName!.ToUpper());
            if (userExists)
            {
                ModelState.AddModelError(string.Empty, "This username is unavailable. Try a different one.");
                return View(vm);
            }

            // Create the user.
            User newUser = new(vm.UserName!);
            IdentityResult result = await _userManager.CreateAsync(newUser, vm.Password + Constantes.GLOBAL_PEPPER);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Unable to create the user.\r\nUsername: {vm.UserName}, Password is null: {string.IsNullOrWhiteSpace(vm.Password)}");

            // Add the user to it's role.
            IdentityRole<Guid>? role = await _roleManager.FindByNameAsync(Constantes.USER_ROLE);
            if (role is null)
                throw new InvalidOperationException("Unable to find the User role in order to add the new user to it's role.");

            result = await _userManager.AddToRoleAsync(newUser, role.Name!);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Unable to add the new user {vm.UserName} to the role {role.Name}.");

            // Automatically sign in the user.
            await _signInManager.SignInAsync(newUser, false);

            return RedirectToAction("Index", "Home");
        }
    }
}