using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Datamining.Data;
using Web_Datamining.Model.Models;
using Web_Datamining.Models;
using Web_Datamining.Service;
using Web_Datamining.Web.Infrastructure.Core;
using Web_Datamining.Web.Models;
<<<<<<< HEAD
=======

>>>>>>> parent of 2fd01a8... Revert "cap nhat bang Khao sao va Api Them Khao Sat"
namespace Web_Datamining.Web.Api
{
    [RoutePrefix("api/Khaosat")]
    [AllowCrossSiteJson]
    public class KhaoSatController : ApiControllerBase
    {
        #region Contructor

        private IKhaoSatService _khaosatService;

        public KhaoSatController(IErrorService errorService, IKhaoSatService khaosatService) : base(errorService)
        {
            this._khaosatService = khaosatService;
        }

      

        #endregion Contructor
        [Route("create")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, string cmnd,string khoi,double d1,double d2,double d3)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    //THEM kHAO SAT
                    KhaoSat KhaoSat = new KhaoSat
                     {
                         CMND = cmnd.ToString(),
                         Khoi = khoi.ToString(),
                         DiemMon1 = (decimal)d1,
                         DiemMon2 = (decimal)d2,
                            DiemMon3 = (decimal)d3, 
                        };
                     _khaosatService.Add(KhaoSat);
                     _khaosatService.Save();
                    response = Request.CreateResponse("Them Thanh Cong");
                }
                return response;
            });
        }
    }
}