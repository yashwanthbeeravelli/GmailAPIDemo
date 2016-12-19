using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using GmailAPIDemo.GmailAPI;
using System.Threading.Tasks;
using System;
using GmailAPIDemo.GmailEntities;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net.Mail;
using MimeKit;

namespace GmailAPIDemo.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : GmailAPIDemoControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }     
    }
}