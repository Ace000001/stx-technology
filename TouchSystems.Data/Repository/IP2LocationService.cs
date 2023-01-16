using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TouchSystems.BAL.Models;
using TouchSystems.Data.Helper;
using TouchSystems.Data.Interfaces;

namespace TouchSystems.Data.Repository
{
   public class IP2LocationService : IIP2LocationService
    {
        private readonly STXDBContext context;
        public string successResult = "Success";
        public string failedResult = "Failed";

        public IP2LocationService(STXDBContext context)
        {
            this.context = context;
        }


        public IEnumerable<Ip2location> GetAllIP2Locations()
        {
            IEnumerable<Ip2location> catlist = new List<Ip2location>();
            try
            {
                catlist = context.Ip2location;
            }
            catch (Exception ex)
            {
                var lineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                Utilities.WriteTextLog("IP2LocationRepo : GetAllIP2Location() : " + lineNumber, ex.Message + " " + ex.InnerException, DateTime.Now);
            }
            return catlist;
        }

        public Ip2location GetIP2Location(long IPFrom)
        {
            Ip2location cat = new Ip2location();
            try
            {
                //double _ipfrom = (double)IPFrom;
                cat = context.Ip2location.Where(x => x.Ipfrom >= IPFrom).OrderBy(x => x.Ipfrom).FirstOrDefault();
              
                // Utilities.WriteTextLog("IP2LocationRepo : GetIP2Location(Guid Id) : " + IPFrom, "", DateTime.Now);
            }
            catch (Exception ex)
            {
                var lineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                Utilities.WriteTextLog("IP2LocationRepo : GetIP2Location(Guid Id) : " + lineNumber, ex.Message + " " + ex.InnerException, DateTime.Now);
            }
            return cat;
        }

        public Ip2location GetIP2LocationByIPTo(double IPTo)
        {
            Ip2location cat = new Ip2location();
            try
            {
                cat = context.Ip2location.Where(x => x.Ipto == IPTo).FirstOrDefault();
            }
            catch (Exception ex)
            {
                var lineNumber = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetFileLineNumber();
                Utilities.WriteTextLog("IP2LocationService : GetIP2LocationByIPTo(double IPTo): " + lineNumber, ex.Message + " " + ex.InnerException, DateTime.Now);
            }

            return cat;
        }

       
    }
}
