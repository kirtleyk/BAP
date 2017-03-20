using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAP_Core.Net.Services;
using BAP_Core.Net.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BAP_Core.Net.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
            
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com")) ModelState.AddModelError("Email", "We don't send to AOL.com");

            if(ModelState.IsValid)
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "Test email", model.Message);

            return View();

        }
    }
}
