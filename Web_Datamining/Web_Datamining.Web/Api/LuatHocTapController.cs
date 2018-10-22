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
        private ILuatService _luatXetTuyenService;
        public LuatHocTapController(IErrorService errorService, ILuatService luatXetTuyenService) : base(errorService)
        {
            this._luatXetTuyenService = luatXetTuyenService;
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
                    var listLuatTheoIdLoaiLuat = _luatXetTuyenService.GetAll(idLoaiLuat);
                    foreach (var item in listLuatTheoIdLoaiLuat)
                    {
                        _luatXetTuyenService.DeleteItem(item);
                    }
                    _luatXetTuyenService.Save();
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
                        _luatXetTuyenService.Add(luat);
                    }
                    _luatXetTuyenService.Save();
                    var newListLuat = _luatXetTuyenService.GetAll();
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
        public HttpResponseMessage GetAll(HttpRequestMessage request,int idLoaiLuat, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _luatXetTuyenService.GetAll(keyword);
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
