﻿using AutoMapper;
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
    [RoutePrefix("api/luathoctap")]
    [AllowCrossSiteJson]
    public class LuatHocTapController : ApiControllerBase
    {
        #region Contructor

        private ILuatService _luatService;

        public LuatHocTapController(IErrorService errorService, ILuatService luatService) : base(errorService)
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
        public HttpResponseMessage FindRules(HttpRequestMessage request, int idLoaiLuat, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _luatService.GetAll(idLoaiLuat, keyword);
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

        #region Api tạo danh sach luật: Khoa => Môn học vượt

        [Route("create")]
        [HttpGet]
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

        #endregion Api tạo danh sach luật: Khoa => Môn học vượt

        #region Hàm lấy ra danh sách các luật: Khoa => Môn học vượt

        public List<ClssRules> GetRulesXetTuyen(double sup, double con)
        {
            WebDbContext dbContext = new WebDbContext();
            var dataListView = (from CTDT in dbContext.ChuongTrinhDaoTaos
                                from dcthk in dbContext.DiemCTHKys
                                from sv in dbContext.SinhViens
                                from dhk in dbContext.DiemHocKys
                                where dcthk.MSSV == sv.MSSV
                                      &&
                                    !(from ct in dbContext.ChuongTrinhDaoTaos
                                      where sv.Lop.Khoa.MaKhoa == ct.MaKhoa && ct.ID_HocKi == dcthk.ID_HocKi
                                      select new
                                      {
                                          ct.MaMon
                                      }).Contains(new { MaMon = dcthk.MaMon })
                                select new
                                {
                                    sv.Lop.Khoa.TenKhoa,
                                    dcthk.MonHoc.TenMon,
                                    sv.MSSV
                                }).Distinct().ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    item.TenMon,
                    item.TenKhoa
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Hàm lấy ra danh sách các luật: Khoa => Môn học vượt

        //********************************************

        #region Ham lay ra danh sach cac luat:Khoa =>Mon cai thien

        public List<ClssRules> LuatCaiThien(double sup, double con)
        {
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
            WebDbContext dbContext = new WebDbContext();
            var dataListView = (from a in (
    (from dcthk in dbContext.DiemCTHKys
     from sv in dbContext.SinhViens
     where
       dcthk.MSSV == sv.MSSV
     select new
     {
         dcthk.MaMon,
         dcthk.MSSV,
         dcthk.DiemTKHe4,
         dcthk.MonHoc.TenMon,
         sv.Lop.Khoa.TenKhoa
     }))
                                group a by new
                                {
                                    a.MaMon,
                                    a.MSSV,
                                    a.TenMon,
                                    a.TenKhoa
                                } into g
                                where g.Count() > 1
                                select new
                                {
                                    g.Key.TenMon,
                                    g.Key.TenKhoa
                                }).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    item.TenMon,
                    item.TenKhoa
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Ham lay ra danh sach cac luat:Khoa =>Mon cai thien

        #region Api tao danh sach luat: Khoa =>Mon cai thien

        [Route("createcaithien")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateCaiThien(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = LuatCaiThien(sup, con);
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

        #endregion Api tao danh sach luat: Khoa =>Mon cai thien

        //********************************************

        #region Api tạo danh sach luật: Môn học cải thiện => Điểm tăng

        [Route("CreateDiemTangCaiThien")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateDiemTangCaiThien(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = GetRulesHocCaiThien(sup, con);
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

        #endregion Api tạo danh sach luật: Môn học cải thiện => Điểm tăng

        #region Hàm lấy ra danh sách các luật: Môn cải thiện => điểm tăng

        public List<ClssRules> GetRulesHocCaiThien(double sup, double con)
        {
            WebDbContext dbContext = new WebDbContext();
            var dataListView = ((from a in (
    (from dcthk in dbContext.DiemCTHKys
     where
       dcthk.DiemTKHe10 >= dcthk.MonHoc.DiemDat
     select new
     {
         dcthk.MSSV,
         dcthk.MonHoc.MaMon,
         dcthk.DiemTKHe10,
         dcthk.MonHoc.TenMon,
         dcthk.ID_HocKi
     }))
                                 group a by new
                                 {
                                     a.MSSV,
                                     a.MaMon,
                                     a.TenMon
                                 } into g
                                 where (g.Max(p => p.DiemTKHe10) - g.Min(p => p.DiemTKHe10)) > 0
                                 select new
                                 {
                                     g.Key.MSSV,
                                     g.Key.MaMon,
                                     g.Key.TenMon,
                                     chechlech = (double?)(g.Max(p => p.DiemTKHe10) - g.Min(p => p.DiemTKHe10))
                                 })).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    item.TenMon,
                    item.chechlech.ToString()
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Hàm lấy ra danh sách các luật: Môn cải thiện => điểm tăng

        //********************************************

        #region Api tạo danh sach luật: Môn học vượt => Điểm tăng

        [Route("CreateDiemTangHocVuot")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateDiemTangHocVuot(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = GetRulesHocVuot(sup, con);
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

        #endregion Api tạo danh sach luật: Môn học vượt => Điểm tăng

        #region Hàm lấy ra danh sách các luật: Môn học vượt => điểm tăng

        public List<ClssRules> GetRulesHocVuot(double sup, double con)
        {
            WebDbContext dbContext = new WebDbContext();
            var dataListView = ((from CTDT in dbContext.ChuongTrinhDaoTaos
                                 from dcthk in dbContext.DiemCTHKys
                                 where
                                   !
                                     (from ct in dbContext.ChuongTrinhDaoTaos
                                      where
      ct.MaKhoa == dcthk.DiemHocKy.SinhVien.Lop.ChuyenNganh.Khoa.MaKhoa &&
      ct.ID_HocKi == dcthk.ID_HocKi
                                      select new
                                      {
                                          ct.MaMon
                                      }).Contains(new { MaMon = dcthk.MaMon })
                                 select new
                                 {
                                     dcthk.MaMon,
                                     dcthk.DiemHocKy.SinhVien.MSSV,
                                     dcthk.DiemTKHe10,
                                     dcthk.MonHoc.TenMon,
                                     dcthk.ID_HocKi
                                 })).Distinct().ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    item.TenMon,
                    item.DiemTKHe10.ToString()
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Hàm lấy ra danh sách các luật: Môn học vượt => điểm tăng

        //********************************************
    }
}