using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Web_Datamining.Models;
using Web_Datamining.Service;
using Web_Datamining.Web.Infrastructure.Core;
using Web_Datamining.Web.Models;

namespace Web_Datamining.Web.Api
{
    [RoutePrefix("api/data")]
    [AllowCrossSiteJson]
    public class DataController : ApiControllerBase
    {
        #region Contructor

        private IKhoaService _khoaService;
        private IMonHocService _monHocService;

        public DataController(IErrorService errorService, IKhoaService khoaService, IMonHocService monHocService) : base(errorService)
        {
            this._khoaService = khoaService;
            this._monHocService = monHocService;
        }

        #endregion Contructor

        #region Api lấy sử dụng lấy danh sách tất cả các khoa

        [Route("getallkhoa")]
        [HttpGet]
        public HttpResponseMessage GetAllKhoa(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _khoaService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.TenKhoa);

                var responseData = Mapper.Map<IEnumerable<Khoa>, List<KhoaViewModel>>(query);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Api lấy sử dụng lấy danh sách tất cả các khoa

        #region Api lấy sử dụng lấy danh sách tất cả các môn học

        [Route("getallmonhoc")]
        [HttpGet]
        public HttpResponseMessage GetAllMonHoc(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _monHocService.GetAll();
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.TenMon);

                var responseData = Mapper.Map<IEnumerable<MonHoc>, List<MonHocViewModel>>(query);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Api lấy sử dụng lấy danh sách tất cả các môn học
    }
}