﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAP_Core.Net.Models;
using BAP_Core.Net.Services;
using BAP_Core.Net.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BAP_Core.Net.Controllers.Web
{
    public class AppController : BaseController
    {
        private AppController(IMailService mailService, IConfigurationRoot config, BapContext context) : base(mailService, config, context){}

        public IActionResult Index()
        {
            
            var data = Db.Trips.ToList();
            return View(data);
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

            if (ModelState.IsValid)
            {
                MailService.SendMail(Config["MailSettings:ToAddress"], model.Email, "Test email", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }
            return View();

        }


    }
}
