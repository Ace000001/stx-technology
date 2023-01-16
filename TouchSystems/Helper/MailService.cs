using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using TouchSystems.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TouchSystems.Helper
{
    public class MailService
    {
        private readonly IConfiguration _config;
        private readonly string smtphost;
        private readonly string smtpport;
        private readonly string smtpusername;
        private readonly string smtppassword;
        private readonly string enableSSL;
        private readonly string sender;
        private readonly string receiver;
        public MailService(IConfiguration config)
        {
            _config = config;
            smtphost = _config.GetSection("AppSettings")["smtphost"];
            smtpport = _config.GetSection("AppSettings")["smtpport"];
            smtpusername = _config.GetSection("AppSettings")["smtpusername"];
            smtppassword = _config.GetSection("AppSettings")["smtppassword"];
            enableSSL = _config.GetSection("AppSettings")["enableSSL"];
            sender = _config.GetSection("AppSettings")["sender"];
            receiver = _config.GetSection("AppSettings")["receiver"];
        }

        public bool SendConactForm(ContactFromVM model)
        {
          
            string strBody = "";
            strBody += "Hello Admin <br><br>";
            strBody += "You received and enquiry from website.<br><br>";
            strBody += "Name "+ model.Name +"<br><br>";
            strBody += "Company " + model.CompanyName + "<br><br>";
            strBody += "Phone No. " + model.PhoneNumber + "<br><br>";
            strBody += "Email " + model.Email + "<br><br>";
            strBody += "Message " + model.Message + "<br><br>";
           
            bool status = SendEmail(strBody, sender, receiver, "Web Contact Us | " + model.Country);
            return status;
        }
        public bool SendProductEnquiry(ContactFromVM model, string product)
        {

            string strBody = "";
            strBody += "Hello Admin <br><br>";
            strBody += "You received and enquiry from website.<br><br>";
            strBody += "Name " + model.Name + "<br><br>";
            strBody += "Company " + model.CompanyName + "<br><br>";
            strBody += "Phone No. " + model.PhoneNumber + "<br><br>";
            strBody += "Email " + model.Email + "<br><br>";
            strBody += "Message " + model.Message + "<br><br>";
            strBody += "Product<br> " + product + "<br><br>";


            bool status = SendEmail(strBody, sender, receiver, "Web Contact Us | " + model.Country);
            return status;
        }
        public bool SendGetinTouch(string name, string email, string phone, string message, string country, string Company)
        {
            string strBody = "";
            strBody += "Hello Admin <br><br>";
            strBody += "You received and enquiry from website.<br><br>";
            strBody += "Name " + name + "<br><br>";
            strBody += "Company " + Company + "<br><br>";
            strBody += "Phone No." + phone + "<br><br>";
            strBody += "Email " + email + "<br><br>";
            strBody += "Message " + message + "<br><br>";

            bool status = SendEmail(strBody, sender, receiver, "Web Get in Touch | " + country);
            return status;
        }


        public bool SendEmail(string msgBody, string fromAdd, string toEmail, string emailSub)
        {
            try
            {
                /* CREDENTIALS
                 * ===================================================*/
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-EN");
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(toEmail);
                myMessage.From = new MailAddress(fromAdd);
                myMessage.Subject = emailSub;
                myMessage.Body = msgBody;
                myMessage.IsBodyHtml = true;
                myMessage.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtphost;
                smtp.Port = Convert.ToInt32(smtpport);
                smtp.Credentials = new System.Net.NetworkCredential(smtpusername, smtppassword);
                smtp.EnableSsl = Convert.ToBoolean(enableSSL);
                smtp.Send(myMessage);
                return true;
            }

            catch (Exception e)
            {
                Utilities.WriteTextLog("SendEmail(string msgBody, string fromAdd, string toEmail, string emailSub):", e.Message + " " + e.InnerException, DateTime.Now);
                return false;
            }
        }
    }
}
