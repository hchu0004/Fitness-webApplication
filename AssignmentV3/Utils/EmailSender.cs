using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AssignmentV3.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void ReservationSendMail(String toEmail, String subject, String contents, String program, String starttime, String endtime)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@XIIFIT.com", "XIIFIT - Body Transforamtion");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<h3><span style='color: #ffffff; background-color: #800080;'><strong>Thank you for booking a consultation time with XIIFIT</strong></span></h3>" 
                                + "<h4>" + contents + "</h4><p>********************************************</p><p>Program: " + program + "</p><p>Start time: "
                                + starttime + "</p><p>End time: " + endtime + "</p><p>********************************************</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

        public void NewUserSendMail(String fromEmail, String subject, String customerName, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(fromEmail, "Customer");
            var to = new EmailAddress("harrychung1995@gmail.com", "");
            var plainTextContent = contents;
            var htmlContent = "<h3><span style='color: #ffffff; background-color: #800080;'><strong>This is new customer consultation</strong></span></h3>"
                                + "<h4></h4><p>********************************************</p><p>Customer Email: " + fromEmail + "</p><p>Customer Name: "
                                + customerName + "</p><p>Contents: " + contents + "</p><p>********************************************</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}