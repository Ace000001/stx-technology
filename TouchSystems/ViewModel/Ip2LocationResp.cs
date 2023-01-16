using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TouchSystems.ViewModel
{
    public class Ip2LocationResp
    {
        public string response { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string region_name { get; set; }
        public string city_name { get; set; }
        public object city { get; set; }
        public string credits_consumed { get; set; }
    }
}
