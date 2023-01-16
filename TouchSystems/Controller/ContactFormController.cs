using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using TouchSystems.Helper;
using TouchSystems.Services;
using TouchSystems.ViewModel;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace TouchSystems.Controller
{
    public class ContactFormController : SurfaceController
    {
        private readonly IConfiguration _config;
        private readonly IGoogleCaptchaService _googleCaptchaService;
        public string msgtxt = "";
        public string msgtype = "";
        public string successResult = "Success";
        public string failedResult = "Failed";

        private readonly IHttpContextAccessor httpContextAccessor;
        public ContactFormController(
            IConfiguration config,
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IHttpContextAccessor httpContextAccessor,
            IGoogleCaptchaService googleCaptchaService)
           : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _config = config;
            this.httpContextAccessor = httpContextAccessor;
            _googleCaptchaService = googleCaptchaService;

        }


        [HttpPost]
          public IActionResult Submit(ContactFromVM model)
        {
           
            try
            {
                if (!_googleCaptchaService.VerifyToken(model.Token, model.Threshold).GetAwaiter().GetResult())
                {
                    TempData["Msg"] = "The captcha is not valid.";
                } 
                else if ((string.IsNullOrEmpty(model.LastName)) && (string.IsNullOrEmpty(model.Product)))
                {
                    MailService ms = new MailService(_config);
                    ms.SendConactForm(model);
                    TempData["Msg"] = "Form Submitted Successfully.";

                }
                else {
                    TempData["Msg"] = "Form Not Submitted Successfully.";
                }
                
            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Failed! please try again.";
            }
          
          return  RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public IActionResult SubmitProductEnquiry(ContactFromVM model, string ProductEnquiry)
        {
            try
            {
                if (!_googleCaptchaService.VerifyToken(model.Token, model.Threshold).GetAwaiter().GetResult())
                {
                    TempData["Msg"] = "The captcha is not valid.";
                }
                else if ((string.IsNullOrEmpty(model.LastName)) && (string.IsNullOrEmpty(model.Product)))
                {
                    MailService ms = new MailService(_config);
                    ms.SendProductEnquiry(model, ProductEnquiry);
                    TempData["Msg"] = "Form Submitted Successfully.";

                }
                else
                {
                    TempData["Msg"] = "Form Not Submitted Successfully.";
                }

            }
            catch (Exception ex)
            {
                TempData["Msg"] = "Failed! please try again.";
            }

            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public IActionResult SubmitGetintouch(string captchaToken, double threshold, string name, string email, string phone, string message, string country,string Company, string lastname, string product )
        {
            try
            {
                if (!_googleCaptchaService.VerifyToken(captchaToken, threshold).GetAwaiter().GetResult())
                {
                    TempData["response"] = "The captcha is not valid.";
                }
                else if ((string.IsNullOrEmpty(lastname)) && (string.IsNullOrEmpty(product)))
                {
                    MailService ms = new MailService(_config);
                    ms.SendGetinTouch(name, email, phone, message, country, Company) ;
                TempData["response"] = "Form Submitted Successfully.";
                }
                else
                {
                    TempData["response"] = "Form Not Submitted Successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["response"] = "Failed! please try again.";
            }
           
            return RedirectToCurrentUmbracoPage();
        }
       

    }
}
