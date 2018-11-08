using AutoMapper;
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
    [RoutePrefix("api/luatphongdaotao")]
    [AllowCrossSiteJson]
    public class LuatPhongDaoTaoController : ApiControllerBase
    {
        #region Contructor

        private ILuatService _luatService;

        public LuatPhongDaoTaoController(IErrorService errorService, ILuatService luatService) : base(errorService)
        {
            this._luatService = luatService;
        }

        //Db classitem hỗ trợ thuật toán apriori
        private ClssItemCollection db = new ClssItemCollection();

        #endregion Contructor

        #region Api lấy sử dụng luật: Nhập học + Đậu => Khu vực

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

        #endregion Api lấy sử dụng luật: Nhập học + Đậu => Khu vực

        #region Api tạo danh sach luật: Nhập học + Đậu => Khu vực

        [Route("createkhuvucnhaphoc")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CreateKhuVucNhapHoc(HttpRequestMessage request, int idLoaiLuat, double sup, double con)
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
                    List<ClssRules> allRules = GetRulesNhapHocDau_KhuVuc(sup, con);
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

        #endregion Api tạo danh sach luật: Nhập học + Đậu => Khu vực

        #region Hàm lấy ra danh sách các luật: Nhập học + Đậu => Khu vực

        public List<ClssRules> GetRulesNhapHocDau_KhuVuc(double sup, double con)
        {
            //double minSupport = Double.Parse(formCollection["MinSupport"]);
            //double minConfidence = Double.Parse(formCollection["MinConfidence"]);
            WebDbContext dbContext = new WebDbContext();
            var dataListView = ((
    from HoSoXetTuyens in dbContext.HoSoXetTuyens
    from Tinhs in dbContext.Tinhs
    where
      HoSoXetTuyens.TruongTHPT.MaTinh == Tinhs.MaTinh &&
      HoSoXetTuyens.TinhTrangTrungTuyen == 1 &&
        (from SinhViens in dbContext.SinhViens
         select new
         {
             SinhViens.MaHoSo
         }).Contains(new { MaHoSo = HoSoXetTuyens.MaHoSo })
    select new
    {
        HoSoXetTuyens.CMDN,
        TinhTrangTrungTuyen = (int?)HoSoXetTuyens.TinhTrangTrungTuyen,
        Tinhs.TenTinh,
        TinhTrang = "Nhập học"
    }
).Union
(
    from HoSoXetTuyens in dbContext.HoSoXetTuyens
    from Tinhs in dbContext.Tinhs
    where
      HoSoXetTuyens.TruongTHPT.MaTinh == Tinhs.MaTinh &&
      HoSoXetTuyens.TinhTrangTrungTuyen == 1 &&
      !
        (from SinhViens in dbContext.SinhViens
         select new
         {
             SinhViens.MaHoSo
         }).Contains(new { MaHoSo = HoSoXetTuyens.MaHoSo })
    select new
    {
        CMDN = HoSoXetTuyens.CMDN,
        TinhTrangTrungTuyen = (int?)HoSoXetTuyens.TinhTrangTrungTuyen,
        TenTinh = Tinhs.TenTinh,
        TinhTrang = "Không nhập học"
    }
)).ToList();
            string result = "";
            foreach (var item in dataListView)
            {
                db.Add(new clssItemSet()
                {
                    item.TenTinh,
                    item.TinhTrang
                });
            }

            clssItemSet uniqueItems = db.GetUniqueItems();
            ClssItemCollection L = clssApriori.DoApriori(db, sup);
            List<ClssRules> allRules = clssApriori.Mine(db, L, con);
            result = "\n" + allRules.Count + " rules \n";

            return allRules;
        }

        #endregion Hàm lấy ra danh sách các luật: Nhập học + Đậu => Khu vực


        //********************************************

        #region Api tạo danh sach luật: Hình thức xét tuyển => Khu vực

        [Route("createhinhthuckhuvuc")]
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

        #endregion Api tạo danh sach luật: Hình thức xét tuyển => Khu vực

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

        #endregion Hàm lấy ra danh sách các luật: Hình thức xét tuyển => Khu vực
    }
}