using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;
using Test_Assignment.Models;

namespace Test_Assignment.Controllers
{
    public class HomeController : Controller
    {
        private string filePath = @".\DbFile\usersDb.xml";
        string currentMachineName = Environment.MachineName;
        List<User> usersMessages;

        public HomeController(ILogger<HomeController> logger)
        { }

        public IActionResult Main()
        {
            ViewBag.Username = currentMachineName;
            return View();
        }

        public IActionResult GetAllUsersMessages()
        {
            Thread.Sleep(10);
            return Json(DeserializationListOfUsers(usersMessages, filePath));
        }

        public IActionResult GetCurrentUserMessages()
        {
            if (System.IO.File.Exists(filePath))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(List<User>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    usersMessages = (List<User>)Serializer.Deserialize(reader);
                }
            }

            List<User> filteredMessages = usersMessages.Where(u => u.UserName == currentMachineName).ToList();

            if (filteredMessages.Count > 10)
            {
                int index = usersMessages.FindIndex(u => u.Id == filteredMessages[0].Id);
                if (index >= 0)
                {
                    usersMessages.RemoveAt(index);
                }
                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, usersMessages);
                }
            }

            return Json(usersMessages.Where(u => u.UserName == currentMachineName).ToList());
        }
        public IActionResult Message(string message)
        {
            List<User> usersList = DeserializationListOfUsers(usersMessages, filePath);

            int newId = 0;
            if (usersList.Count > 0)
            {
                newId = usersList.Max(u => u.Id) + 1;
            }

            User newUser = new User
            {
                Id = newId,
                UserName = currentMachineName,
                Message = message,
                Time = DateTime.Now,
            };

            usersList.Add(newUser);

            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, usersList);
            }

            TempData["SuccessMessage"] = "Message successfully added";

            return Json(new { success = true, message = TempData["SuccessMessage"] });
        }

        static public List<User> DeserializationListOfUsers(List<User> usersMessages, string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                XmlSerializer Serializer = new XmlSerializer(typeof(List<User>));
                using (StreamReader reader = new StreamReader(filePath))
                {
                    usersMessages = (List<User>)Serializer.Deserialize(reader);
                }
            }

            if (usersMessages.Count > 20)
            {
                usersMessages.RemoveAt(0);

                XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, usersMessages);
                }
            }

            return usersMessages;
        }
    }
}
