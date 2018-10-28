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
        public HttpResponseMessage Create(HttpRequestMessage request,int idLoaiLuat, double sup, double con)
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
        public HttpResponseMessage GetAll(HttpRequestMessage request,int idLoaiLuat)
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
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
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
        #endregion
        #region Api tao danh sach luat: Khoa =>Mon cai thien
        [Route("createcaithien")]
        [HttpPost]
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
        #endregion

        //select distinct dcthk.MaMon
        //from ChuongTrinhDaoTao CTDT, MonHoc mh,SinhVien sv, DiemHocKy dhk, DiemCTHKy dcthk, Lop l,ChuyenNganh cn,
        //     Khoa k
        //where k.TenKhoa like 'CNTT' and k.MaKhoa = cn.MaKhoa and cn.MaChuyenNganh = l.MaChuyenNganh and l.ID_Lop = sv.ID_Lop

        //and dhk.MSSV = sv.MSSV and dcthk.MSSV = dhk.MSSV

        //and dcthk.MaMon not in (select ct.MaMon
        //                        from ChuongTrinhDaoTao ct

        //                        where ct.MaKhoa = k.MaKhoa and ct.ID_HocKi dcthk.ID_HocKi)

    }
}
