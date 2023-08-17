using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Assignment.Common;
using Test_Assignment.Context;
using Test_Assignment.Services;

namespace Test_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestDbContext _dbContext;
        private readonly IUserMessageService _userMessageService;

        public HomeController(TestDbContext dbContext, IUserMessageService userMessageService)
        {
            _dbContext = dbContext;
            _userMessageService = userMessageService;
        }

        public IActionResult Main()
        {
            ViewBag.Username = CommonOptions.currentMachineName;
            return View();
        }

        async public Task<IActionResult> GetAllUsersMessages()
        {
            try
            {
                return Json(await _userMessageService.GetAllUsersMessagesAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        async public Task<IActionResult> GetCurrentUserMessages()
        {
            try
            {
                return Json(await _userMessageService.GetCurrentUserMessagesAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw; 
            }
        }

        async public Task<IActionResult> Message(string message)
        {
            await _userMessageService.AddMessageAsync(message);
            return Json(new { success = true, message = "Message successfully added" });
        }
    }
}
