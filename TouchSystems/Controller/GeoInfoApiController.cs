using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TouchSystems.Data.Interfaces;
using TouchSystems.ViewModel;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.Controllers;
namespace TouchSystems.Controller
{
    public class GeoInfoApiController : UmbracoApiController
    {
        private readonly IConfiguration _config;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IIP2LocationService ip2Locationlervice;
        private readonly string Ip2LKey;
        public GeoInfoApiController(IHttpContextAccessor httpContextAccessor, IIP2LocationService ip2locationlervice, IConfiguration config)
        {
            _config = config;
            Ip2LKey = _config.GetSection("AppSettings")["Ip2LKey"];
            _httpContextAccessor = httpContextAccessor;
            this.ip2Locationlervice = ip2locationlervice;
        }
        public string GetGeoData()
        {
            try
            {
                Helper.GEOAnalytics ant = new Helper.GEOAnalytics(_httpContextAccessor, ip2Locationlervice);
                Geoinfo ginfo = ant.GetAnalytics(GetUserIP());
                return ginfo.CountryName.ToLower();
            }
            catch (Exception ex)
            {
                return "";
            }
          

        }


        public string GetGeoDataAPI()
        {
            string res = "";
            string ipadr = GetUserIP();
            //https://api.ip2location.com/v2/?ip=103.148.8.220&format=json&package=WS3&addon=city&key=QARCYTRPCS
            string apiurl = "https://api.ip2location.com/v2/?ip="+ ipadr + "&format=json&package=WS3&addon=city&key=" + Ip2LKey;
            WebRequest request = WebRequest.Create(apiurl);
            using (WebResponse response = request.GetResponse())
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                res = sr.ReadToEnd();
            }

            if (res != null)
            {
                var result = JsonConvert.DeserializeObject<Ip2LocationResp>(res);

                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(20);
                HttpContext.Response.Cookies.Append("region", result.region_name, option);


            }

            return res;
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
