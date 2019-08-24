using AssignmentV3.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace AssignmentV3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(String name, String email, String message)
        {
            String customerName = name;
            String customerEmail = email;
            if (!IsValidEMailAddress(customerEmail))
            {
                return View();
            }
            String customerMessage = message;
            String subject = "new Customer Consultation";
            EmailSender es = new EmailSender();
            es.NewUserSendMail(customerEmail, subject, customerName, customerMessage);
            return View();
        }

        public static bool IsValidEMailAddress(string email)
        {
            return Regex.IsMatch(email, @"^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$");
        }

        protected override void HandleUnknownAction(string actionName)
        {
            Response.Redirect("~/Home/Index");
            base.HandleUnknownAction(actionName);
        }
    }
}