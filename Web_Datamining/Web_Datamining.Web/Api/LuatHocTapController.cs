﻿using AutoMapper;
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
        ClssItemCollection db = new ClssItemCollection();
        #endregion

        #region Api tạo danh sach luật: Khoa => Môn học vượt
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

        #region Api lấy sử dụng luật: Khoa => Môn học vượt
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
        #endregion
        #region Create Diem Tang Cai Thien Function
        [Route("CreateDiemTangCaiThien")]
        [HttpPost]
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
        #endregion

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
        #endregion
    }
}
