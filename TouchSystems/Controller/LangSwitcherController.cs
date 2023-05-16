using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace TouchSystems.Controller
{
    public class LangSwitcherController : SurfaceController
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor httpContextAccessor;
        public string msgtxt = "";
        public string msgtype = "";
        public string successResult = "Success";
        public string failedResult = "Failed";
        private readonly string websiteurl;
        private readonly IPublishedContentQuery _publishedContentQuery;
        public LangSwitcherController(
            IConfiguration config,
       IUmbracoContextAccessor umbracoContextAccessor,
           IUmbracoDatabaseFactory databaseFactory,
           ServiceContext services,
           AppCaches appCaches,
           IProfilingLogger profilingLogger,
           IPublishedUrlProvider publishedUrlProvider,
            IHttpContextAccessor httpContextAccessor,
            IPublishedContentQuery publishedContentQuery)
           : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _config = config;
            this.httpContextAccessor = httpContextAccessor;
            _publishedContentQuery = publishedContentQuery;
            websiteurl = _config.GetSection("AppSettings")["WebsiteUrl"];
        }


        [HttpPost]
        public IActionResult Switch(string langcountry, string langdata, string url, string langpageid,string cultureinfo)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(20);
            HttpContext.Response.Cookies.Append(langdata, langcountry, option);


            //string culture = cultureinfo;
            var culture = new CultureInfo(cultureinfo);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            //    get region name
            if (!string.IsNullOrEmpty(langpageid)) {
                var node = GetContent(int.Parse(langpageid));
                var match = Regex.Match(node.Url(), @"((EN|en)-([A-Z]|[a-z]){2})");
                return Redirect(node.Url().Replace("/" + match.Value, "/" + langcountry));
            }
            else{
                  return Redirect(websiteurl + "/" + langcountry);
            }
           

            //return Json("ok");
        }

        public IPublishedContent GetContent(int nodeId)
        {
            var node = _publishedContentQuery.Content(nodeId);
             return node;
        }


    }
}
