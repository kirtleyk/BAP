using System;
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
    public class BaseController : Controller
    {
        protected readonly IMailService MailService;
        protected readonly IConfigurationRoot Config;
        protected BapContext Db;

        public BaseController(IMailService mailService, IConfigurationRoot config, BapContext context)
        {
            MailService = mailService;
            Config = config;
            Db = context;
        }
    }
}
