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
    //[RoutePrefix("api/luatxettuyen")]
    //[AllowCrossSiteJson]
//    public class LuatXetTuyenController : ApiControllerBase
//    {
//        private ILuatXetTuyenService _luatXetTuyenService;
//        public LuatXetTuyenController(IErrorService errorService,ILuatXetTuyenService luatXetTuyenService) : base(errorService)
//        {
//            this._luatXetTuyenService = luatXetTuyenService;
//        }

//        [Route("getall")]
//        [HttpGet]
//        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword)
//        {
//            return CreateHttpResponse(request, () =>
//            {
//                int totalRow = 0;
//                var model = _luatXetTuyenService.GetAll(keyword);
//                totalRow = model.Count();
//                var query = model.OrderByDescending(x => x.X);

//                var responseData = Mapper.Map<IEnumerable<LuatXetTuyen>, List<LuatXetTuyenViewModel>>(query);

//                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
//                return response;
//            });
//        }
//        [Route("create")]
//        [HttpPost]
//        [AllowAnonymous]
//        public HttpResponseMessage Create(HttpRequestMessage request,double sup, double con)
//        {
//            return CreateHttpResponse(request, () =>
//            {
//                HttpResponseMessage response = null;
//                if (!ModelState.IsValid)
//                {
//                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
//                }
//                else
//                {
//                    List<ClssRules> allRules = GetRulesXetTuyen(sup, con);
//                    //Delete old rules
//                    var listLuat = _luatXetTuyenService.GetAll();
//                    foreach (var item in listLuat)
//                    {
//                        _luatXetTuyenService.DeleteItem(item);
//                    }
//                    _luatXetTuyenService.Save();

//                    //Insert new rules
//                    foreach (ClssRules rule in allRules)
//                    {
//                        LuatXetTuyen luat = new LuatXetTuyen
//                        {
//                            X = rule.X.ToString(),
//                            Y = rule.Y.ToString(),
//                            Support = (decimal)rule.Support,
//                            Confidence = (decimal)rule.Confidence
//                        };
//                        _luatXetTuyenService.Add(luat);
//                    }
//                    _luatXetTuyenService.Save();
//                    var newListLuat = _luatXetTuyenService.GetAll();
//                    var responseData = Mapper.Map<IEnumerable<LuatXetTuyen>, List<LuatXetTuyenViewModel>>(newListLuat);
//                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
//                }
//                return response;
//            });
//        }

//        //Static
//        ClssItemCollection db = new ClssItemCollection();
//        public List<ClssRules> GetRulesXetTuyen(double sup, double con)
//        {
//            //double minSupport = Double.Parse(formCollection["MinSupport"]);
//            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
//            WebDbContext dbContext = new WebDbContext();
//            var dataListView = (from dsnv in dbContext.DSNguyenVongs
//                                join hoso in dbContext.HoSoXetTuyens on dsnv.MaHoSo equals hoso.MaHoSo
//                                join dxt in dbContext.DiemXetTuyens on hoso.DXT_ID equals dxt.DXT_ID
//                                where hoso.DXT_ID == dxt.DXT_ID
//                                select new ListViewXetTuyen
//                                {
//                                    MaHoSo = dsnv.MaHoSo,
//                                    HoTen = dsnv.HoSoXetTuyen.HoTen,
//                                    TinhTrangTrungTuyen = (hoso.TinhTrangTrungTuyen == 1) ? "Đậu" : "Rớt",
//                                    TongDiem = (dxt.DiemToan + dxt.DiemLy + dxt.DiemHoa < 30) ? (dxt.DiemToan + dxt.DiemLy + dxt.DiemHoa > 20) ? "[20..30]" : (dxt.DiemToan + dxt.DiemLy + dxt.DiemHoa > 13) ? "[13..20]" : "[0..13]" : "",
//                                    KhoaXetTuyen = dsnv.NganhTheoBo.TeNganh

//                                }).ToList();
//            string result = "";
//            foreach (var item in dataListView)
//            {
//                db.Add(new clssItemSet()
//                {
//                    item.TongDiem,
//                    item.KhoaXetTuyen,
//                    item.TinhTrangTrungTuyen
//                });
//            }

//            clssItemSet uniqueItems = db.GetUniqueItems();
//            ClssItemCollection L = clssApriori.DoApriori(db, sup);
//            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
//            result = "\n" + allRules.Count + " rules \n";

//            return allRules;
//            /*
//            //Delete old rules
//            var listLuat = dbContext.LuatXetTuyens.ToList();
//            foreach (var item in listLuat)
//            {
//                dbContext.LuatXetTuyens.DeleteOnSubmit(item);
//                dbContext.SubmitChanges();
//            }

//            //Insert new rules
//            foreach (ClssRules rule in allRules)
//            {
//                LuatXetTuyen luat = new LuatXetTuyen
//                {
//                    X = rule.X.ToString(),
//                    Y = rule.Y.ToString(),
//                    Support = (decimal)rule.Support,
//                    Confidence = (decimal)rule.Confidence
//                };
//                dbContext.LuatXetTuyens.InsertOnSubmit(luat);
//            }
//            dbContext.SubmitChanges();

//            //Show
//            var listRules = dbContext.LuatXetTuyens.ToList();
//            foreach (ClssRules rule in allRules)
//            {
//                Console.WriteLine(rule + "\n");
//                result += rule + "\n";
//            }
//            */
//        }

////         select distinct dcthk.MaMon
////         from ChuongTrinhDaoTao CTDT, MonHoc mh,SinhVien sv, DiemHocKy dhk, DiemCTHKy dcthk, Lop l,ChuyenNganh cn, 
////              Khoa k
////         where k.TenKhoa like 'CNTT' and k.MaKhoa = cn.MaKhoa and cn.MaChuyenNganh = l.MaChuyenNganh and l.ID_Lop =                    sv.ID_Lop
////         and dhk.MSSV = sv.MSSV and dcthk.MSSV = dhk.MSSV
////         and dcthk.MaMon not in (select ct.MaMon
////                                 from ChuongTrinhDaoTao ct
////                                 where ct.MaKhoa = k.MaKhoa and ct.ID_HocKi   dcthk.ID_HocKi)
//    }
}
