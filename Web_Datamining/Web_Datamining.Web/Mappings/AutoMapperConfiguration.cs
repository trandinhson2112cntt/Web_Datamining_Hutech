using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_Datamining.Models;
using Web_Datamining.Web.Models;

namespace Web_Datamining.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Luat, LuatViewModel>();
            
        }
    }
}