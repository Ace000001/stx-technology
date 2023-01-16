using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;
using TouchSystems.Data.Interfaces;
using TouchSystems.ViewModel;
using System.Net;
using System.IO;

namespace TouchSystems.Components
{
    public class GeoInfoViewComponent : ViewComponent
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IIP2LocationService ip2Locationlervice;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        public GeoInfoViewComponent(IUmbracoContextAccessor umbracoContextAccessor, IHttpContextAccessor httpContextAccessor, IIP2LocationService ip2locationlervice)
        {
            _httpContextAccessor = httpContextAccessor;
            this.ip2Locationlervice = ip2locationlervice;
            _umbracoContextAccessor = umbracoContextAccessor;
        }
        public IViewComponentResult Invoke()
        {
            //Helper.GEOAnalytics ant = new Helper.GEOAnalytics(_httpContextAccessor, ip2Locationlervice);
            Geoinfo ginfo = new Geoinfo();
            return View(ginfo);
        }


        public string GetUserIP()
        {
            string strIP = String.Empty;

            if (HttpContext.Session.GetString("geoip") == null)
            {
                //  HttpRequest httpReq = HttpContext.Current.Request;
                string remoteip = HttpContext.Connection.RemoteIpAddress?.ToString();
                var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
                string LocalIPAddr = feature?.LocalIpAddress?.ToString();
                //test for non-standard proxy server designations of client's IP
                if ((remoteip != "::1") && (!string.IsNullOrEmpty(remoteip)))
                {
                    strIP = HttpContext.Connection.RemoteIpAddress?.ToString();
                }

                else if ((LocalIPAddr != "::1") && (!string.IsNullOrEmpty(LocalIPAddr)))
                {
                    //returning 127.0.0.1
                    strIP = LocalIPAddr;
                }
                //test for host address reported by the server
                //        else if
                //        (
                ////if exists
                //_httpContextAccessor.HttpContext.
                //(httpReq.UserHostAddress.Length != 0)
                //            &&
                //            //and if not localhost IPV6 or localhost name
                //            ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
                //        )
                //        {
                //            strIP = httpReq.UserHostAddress;
                //        }
                //finally, if all else fails, get the IP from a web scrape of another server


                if ((strIP == "127.0.0.1") || (string.IsNullOrEmpty(strIP)))
                {
                    WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                    using (WebResponse response = request.GetResponse())
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        strIP = sr.ReadToEnd();
                    }
                    //scrape ip from the html
                    int i1 = strIP.IndexOf("Address: ") + 9;
                    int i2 = strIP.LastIndexOf("</body>");
                    strIP = strIP.Substring(i1, i2 - i1);
                }
                HttpContext.Session.SetString("geoip", strIP);
            }
            else
            {
                strIP = HttpContext.Session.GetString("geoip");
            }
            return strIP;
        }
    }
}
