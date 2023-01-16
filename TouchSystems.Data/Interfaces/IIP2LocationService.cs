using System;
using System.Collections.Generic;
using System.Text;
using TouchSystems.BAL.Models;


namespace TouchSystems.Data.Interfaces
{
    public interface IIP2LocationService
    {
        Ip2location GetIP2Location(long IPFrom);
        Ip2location GetIP2LocationByIPTo(double IPTo);
        IEnumerable<Ip2location> GetAllIP2Locations();
       
    }
}
