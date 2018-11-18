using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Datamining.Data;
using Web_Datamining.Models;
using Web_Datamining.Service;
using Web_Datamining.Web.Infrastructure.Core;
using Web_Datamining.Web.Models;

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
        private ClssItemCollection db = new ClssItemCollection();

        #endregion Contructor

        #region Api lấy sử dụng luật

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

        #endregion Api lấy sử dụng luật

        #region Api tìm kiếm luật xét tuyển

        [Route("findrules")]
        [HttpGet]
        public HttpResponseMessage FindRules(HttpRequestMessage request, int idLoaiLuat,string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _luatService.GetAll(idLoaiLuat,keyword);
                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.X);

                var responseData = Mapper.Map<IEnumerable<Luat>, List<LuatViewModel>>(query);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Api tìm kiếm luật xét tuyển

        #region Api Xóa luật

        [Route("deleterule")]
        [HttpGet]
        public HttpResponseMessage DeleteRule(HttpRequestMessage request, int keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var item = _luatService.GetById(keyword);
                var model = _luatService.DeleteItem(item);
                _luatService.Save();

                var response = request.CreateResponse(HttpStatusCode.OK, "Xóa thành công");
                return response;
            });
        }

        #endregion Api xóa luật
        //********************************************

        #region Ham lay ra ds cac luat :Hoc ba: Nganh=> diem xet tuyen

        public List<ClssRules> XTHocBa(double sup, double con)
        {
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
            WebDbContext dbContext = new WebDbContext();
            var dataListView = (from ds in dbContext.DSNguyenVongs
                                where
                                  ds.HoSoXetTuyen.DiemXetTuyen.HinhThucXetTuyen == false &&
                                  ds.HoSoXetTuyen.TinhTrangTrungTuyen == 1
                                select new
                                {
                                    ds.NganhTheoBo.TeNganh,
                                    TongDiem = (ds.DiemTong < 30) ? (ds.DiemTong > 27) ? "[27..30]"
                                     : (ds.DiemTong > 24) ? "[24..27]"
                                    : (ds.DiemTong > 21) ? "[21..24]"
                                    : (ds.DiemTong > 18) ? "[18..21]"
                                    : (ds.DiemTong > 15) ? "[15..18]"
                                    : "[0..15]" : ""
                                }).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                   item.TeNganh,
                   Convert.ToString(item.TongDiem)
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Ham lay ra ds cac luat :Hoc ba: Nganh=> diem xet tuyen

        #region Api tao ds luat : hoc ba: Nganh=>Diem xet tuyen

        [Route("createxthb")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateXTHB(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = XTHocBa(sup, con);
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

        #endregion Api tao ds luat : hoc ba: Nganh=>Diem xet tuyen

        //*****************

        #region Ham lay ra ds cac luat :Thi Tuyen: Nganh=> diem xet tuyen

        public List<ClssRules> ThiTuyen(double sup, double con)
        {
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
            WebDbContext dbContext = new WebDbContext();
            var dataListView = (from ds in dbContext.DSNguyenVongs
                                where
                                  ds.HoSoXetTuyen.DiemXetTuyen.HinhThucXetTuyen == false &&
                                  ds.HoSoXetTuyen.TinhTrangTrungTuyen == 1
                                select new
                                {
                                    ds.NganhTheoBo.TeNganh,
                                    TongDiem = (ds.DiemTong < 30) ? (ds.DiemTong > 27) ? "[27..30]"
                                     : (ds.DiemTong > 24) ? "[24..27]"
                                    : (ds.DiemTong > 21) ? "[21..24]"
                                    : (ds.DiemTong > 18) ? "[18..21]"
                                    : (ds.DiemTong > 15) ? "[15..18]"
                                    : "[0..15]" : ""
                                }).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                   item.TeNganh,
                   Convert.ToString(item.TongDiem)
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Ham lay ra ds cac luat :Thi Tuyen: Nganh=> diem xet tuyen

        #region Api tao ds luat : hoc ba: Nganh=>Diem xet tuyen

        [Route("createthituyen")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateThiTuyen(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = ThiTuyen(sup, con);
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

        #endregion Api tao ds luat : hoc ba: Thi Tuyển =>Diem xet tuyen
    }
}