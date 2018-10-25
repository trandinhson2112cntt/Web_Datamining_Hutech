using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Datamining.Models;
using Web_Datamining.Web.Infrastructure.Core;
using Web_Datamining.Web.LuatModel;
using Web_Datamining.Web.Models;
using Web_Datamining.Data;
using Web_Datamining.Service;

namespace Web_Datamining.Web.Api
{
    [RoutePrefix("api/luatxettuyen")]
    [AllowCrossSiteJson]
    public class LuatXetTuyenController : ApiControllerBase
    {
        #region Contructor
        private ILuatService _luatService;
        public LuatXetTuyenController(IErrorService errorService, ILuatService luatService) : base(errorService)
        {
            this._luatService = luatService;
        }
        //Db classitem hỗ trợ thuật toán apriori
        ClssItemCollection db = new ClssItemCollection();
        #endregion

        #region Api tạo danh sach luật: Hình thức xét tuyển => Khu vực
        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = GetRulesXetTuyen(sup, con);
                    //Xóa những dữ liệu luật cũ theo idLoaiLuat
                    var listLuatTheoIdLoaiLuat = _luatService.GetAll(idLoaiLuat);
                    foreach (var item in listLuatTheoIdLoaiLuat)
                    {
                        _luatService.DeleteItem(item);
                    }
                    _luatService.Save();
                    //Đẩy danh sach các luật vào cơ sở dữ liệu
                    foreach (ClssRules rule in allRules)
                    {
                        Luat luat = new Luat
                        {
                            X = rule.X.ToString(),
                            Y = rule.Y.ToString(),
                            Support = (decimal)rule.Support,
                            Confidence = (decimal)rule.Confidence,
                            LuatId = idLoaiLuat //Thêm loại luật để phân biệt giữa các luật
                        };
                        _luatService.Add(luat);
                    }
                    _luatService.Save();
                    var newListLuat = _luatService.GetAll(idLoaiLuat);
                    var responseData = Mapper.Map<IEnumerable<Luat>, List<LuatViewModel>>(newListLuat);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }
        #endregion

        #region Api lấy sử dụng luật: Hình thức xét tuyển => Khu vực
        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int idLoaiLuat)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _luatService.GetAll(idLoaiLuat);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.X);

                var responseData = Mapper.Map<IEnumerable<Luat>, List<LuatViewModel>>(query);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        #endregion

        #region Hàm lấy ra danh sách các luật: Hình thức xét tuyển => Khu vực
        public List<ClssRules> GetRulesXetTuyen(double sup, double con)
        {
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
            WebDbContext dbContext = new WebDbContext();
            var dataListView = (from HoSoXetTuyens in dbContext.HoSoXetTuyens
                               from Tinhs in dbContext.Tinhs
                               where
                                 HoSoXetTuyens.TruongTHPT.MaTinh == Tinhs.MaTinh
                               select new
                               {
                                   HoSoXetTuyens.CMDN,
                                   HoSoXetTuyens.TruongTHPT.TenTruong,
                                   Tinhs.TenTinh,
                                   HinhThucXetTuyen = (bool?)HoSoXetTuyens.DiemXetTuyen.HinhThucXetTuyen
                               }).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    (item.HinhThucXetTuyen == true ? "Thi tuyển" : "Học bạ"),
                    item.TenTinh
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }
        #endregion
    }
}
