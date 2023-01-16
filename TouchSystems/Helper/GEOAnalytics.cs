using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using TouchSystems.ViewModel;
using TouchSystems.Data.Interfaces;

namespace TouchSystems.Helper
{
    public class GEOAnalytics
    {
		private IHttpContextAccessor _httpContextAccessor;
		private readonly IIP2LocationService ip2Locationlervice;

		public GEOAnalytics(IHttpContextAccessor httpContextAccessor, IIP2LocationService ip2locationlervice)
		{
			_httpContextAccessor = httpContextAccessor;
			this.ip2Locationlervice = ip2locationlervice;
		}

		public Geoinfo GetAnalytics(string GetUserIP)
		{
			Geoinfo AnalyticsDT = new Geoinfo();
			string strCity = "";
			string strRegion = "";
			string strcountryName = "";
			string ip = "";
			long longIp;
			try
			{
				if (_httpContextAccessor.HttpContext.Session.GetString("geoinfo") == null)
				{
					ip = GetUserIP;//
					longIp = ToInt(ip); //

					if (ip != "")
					{
						if (ip != "::1" && ip != "127.0.0.1")
						{
							var ip2location = ip2Locationlervice.GetIP2Location(longIp);

							if (ip2location != null)
							{
								strCity = ip2location.CityName;
								strRegion = ip2location.RegionName;
								strcountryName = ip2location.CountryName;
							}
						}
											}

					AnalyticsDT.IP = ip;
					AnalyticsDT.City = strCity;
					AnalyticsDT.Region = strRegion;
					AnalyticsDT.CountryName = strcountryName;
					AnalyticsDT.LongIP = longIp.ToString();
					//save session
					var jsonString = JsonConvert.SerializeObject(AnalyticsDT);
					_httpContextAccessor.HttpContext.Session.SetString("geoinfo", jsonString);
					return AnalyticsDT;
				}
				else
				{
					// get session
					var geoinfostr = _httpContextAccessor.HttpContext.Session.GetString("geoinfo");
					var geoinfoobj = JsonConvert.DeserializeObject<Geoinfo>(geoinfostr);
					return geoinfoobj;
				}
				//end
			}
			catch (Exception ex)
			{

				AnalyticsDT.IP = ip;
				AnalyticsDT.City = strCity;
				AnalyticsDT.Region = strRegion;
				AnalyticsDT.CountryName = strcountryName;
				AnalyticsDT.LongIP = "";
				return AnalyticsDT;
			}

		}

		private long Dot2LongIp(string ip)
		{
			string[] ipSplit = ip.Split('.');
			long longip = 16777216 * Convert.ToInt32(ipSplit[0]) + 65536 * Convert.ToInt32(ipSplit[1]) + 256 * Convert.ToInt32(ipSplit[2]) + Convert.ToInt32(ipSplit[3]);
			//IPAddress ipa = IPAddress.Parse(ip);
			//long longip = (long)IPAddress.NetworkToHostOrder((int)System.BitConverter.ToUInt32(ipa.GetAddressBytes(), 0));
			//return BitConverter.ToInt32(IPAddress.Parse(ip).GetAddressBytes().Reverse().ToArray(), 0);
			return longip;
		}

		static long ToInt(string addr)
		{
			// careful of sign extension: convert to uint first; 
			// unsigned NetworkToHostOrder ought to be provided. 
			return (long)(uint)IPAddress.NetworkToHostOrder(
				 (int)IPAddress.Parse(addr).Address);
		}

		static string ToAddr(long address)
		{
			return IPAddress.Parse(address.ToString()).ToString();
			// This also works: 
			// return new IPAddress((uint) IPAddress.HostToNetworkOrder( 
			//    (int) address)).ToString(); 
		}


	}
}
