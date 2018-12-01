using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Datamining.Data;
using Web_Datamining.Models;

namespace Web_Datamining.Web.Areas.Admin.Controllers
{
    public class ImportExcelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        // GET: ImportExcel
        #region Excel Format Dowload Functions
        public FileResult DownloadExcelSinhVienForMat()
        {
            string path = "/ExcelFormat/Students.xlsx";
            return File(path, "application/vnd.ms-excel", "StudentFormat.xlsx");
        }
        public FileResult DownloadExcelHoSoXetTuyenFormat()
        {
            string path = "/ExcelFormat/HoSoXetTuyenFormat.xlsx";
            return File(path, "application/vnd.ms-excel", "HoSoXetTuyenFormat.xlsx");
        }
        public FileResult DownloadExcelDiemXetTuyenFormat()
        {
            string path = "/ExcelFormat/DiemXetTuyenFormat.xlsx";
            return File(path, "application/vnd.ms-excel", "DiemXetTuyenFormat.xlsx");
        }
        public FileResult DownloadExcelDiemChiTietFormat()
        {
            string path = "/ExcelFormat/DiemChiTietFormat.xlsx";
            return File(path, "application/vnd.ms-excel", "DiemChiTietFormat.xlsx");
        }
        public FileResult DownloadExcelMonHocFormat()
        {
            string path = "/ExcelFormat/MonHocFormat.xlsx";
            return File(path, "application/vnd.ms-excel", "MonHocFormat.xlsx");
        }
        public FileResult DownloadExcelDiemFormat()
        {
            string path = "/ExcelFormat/DiemFormat.xlsx";
            return File(path, "application/vnd.ms-excel", "DiemFormat.xlsx");
        }
        #endregion
        #region Upload Functions
        [HttpPost]
        public JsonResult UploadExcelSinhVien(SinhVien students, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch(Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<SinhVien>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.MSSV != "" && a.MaHoSo != 0 && a.ID_Lop != 0)
                            {
                                SinhVien TU = new SinhVien();
                                TU.MSSV = a.MSSV;
                                TU.MaHoSo = a.MaHoSo;
                                TU.CoVanHocTap = a.CoVanHocTap;
                                TU.ID_Lop = a.ID_Lop;
                                db.SinhViens.Add(TU);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.MSSV == "" || a.MSSV == null) data.Add("<li> MSSV is required</li>");
                                if (a.MaHoSo == 0 || a.MaHoSo == null) data.Add("<li> MaHoSo is required</li>");
                                if (a.ID_Lop == 0 || a.ID_Lop == null) data.Add("<li>IP_Lop is required</li>");

                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UploadExcelHoSoXetTuyen(HoSoXetTuyen hoSoXetTuyens, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<HoSoXetTuyen>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.MaHoSo != 0 && a.MaTruongTHPT != 0 && a.TinhTrangTrungTuyen != 0 && a.DXT_ID != 0)
                            {
                                HoSoXetTuyen temp = new HoSoXetTuyen();
                                temp.MaHoSo = a.MaHoSo;
                                temp.MaTruongTHPT = a.MaTruongTHPT;
                                temp.CMDN = a.CMDN;
                                temp.NgaySinh = a.NgaySinh;
                                temp.HoTen = a.HoTen;
                                temp.GioiTinh = a.GioiTinh;
                                temp.DanToc = a.DanToc;
                                temp.TinhTrangTrungTuyen = a.TinhTrangTrungTuyen;
                                temp.DXT_ID = temp.DXT_ID;
                                db.HoSoXetTuyens.Add(temp);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.MaHoSo == 0 ) data.Add("<li> MaHoSo property is required</li>");
                                if (a.MaTruongTHPT == 0) data.Add("<li> MaTruongTHPT property is required</li>");
                                if (a.TinhTrangTrungTuyen == 0 || a.TinhTrangTrungTuyen == null) data.Add("<li>TinhTrangTrungTuyen property is required</li>");
                                if (a.DXT_ID != 0) data.Add("<li> DXT_ID property is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UploadExcelDiemXetTuyen(DiemXetTuyen diemXetTuyens, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<DiemXetTuyen>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.DXT_ID != 0 && a.HinhThucXetTuyen != null)
                            {
                                DiemXetTuyen temp = new DiemXetTuyen();
                                temp.DXT_ID = a.DXT_ID;
                                temp.DiemToan = a.DiemToan;
                                temp.DiemVan = a.DiemVan;
                                temp.DiemLy = a.DiemLy;
                                temp.DiemHoa = a.DiemHoa;
                                temp.DiemSinh = a.DiemSinh;
                                temp.DiemDia = a.DiemDia;
                                temp.DiemGDCD = a.DiemGDCD;
                                temp.DiemNN = temp.DiemNN;
                                temp.HinhThucXetTuyen = temp.HinhThucXetTuyen;
                                db.DiemXetTuyens.Add(temp);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.DXT_ID == 0) data.Add("<li> DXT_ID property is required</li>");
                                if (a.HinhThucXetTuyen != null) data.Add("<li> HinhThucXetTuyen property is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UploadExcelDiemChiTietHocKy(DiemCTHKy diemChiTietHocKies, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<DiemCTHKy>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.MaMon != null && a.MSSV != null && a.ID_HocKi !=0)
                            {
                                DiemCTHKy temp = new DiemCTHKy();
                                temp.MaMon = a.MaMon;
                                temp.MSSV = a.MSSV;
                                temp.ID_HocKi = a.ID_HocKi;
                                temp.DiemTH = a.DiemTH;
                                temp.DiemQT = a.DiemQT;
                                temp.DiemThi1 = a.DiemThi1;
                                temp.DiemThi2 = a.DiemThi2;
                                temp.TiLeDiemThi1 = a.TiLeDiemThi1;
                                temp.TiLeDiemThi2 = temp.TiLeDiemThi2;
                                temp.DiemTKHe10 = temp.DiemTKHe10;
                                temp.DiemTKHe4 = temp.DiemTKHe4;
                                temp.DiemTKChu = temp.DiemTKChu;
                                db.DiemCTHKys.Add(temp);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.MaMon == null) data.Add("<li> MaMon property is required</li>");
                                if (a.MSSV != null) data.Add("<li> MSSV property is required</li>");
                                if (a.ID_HocKi != 0) data.Add("<li> ID_HocKi property is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UploadExcelMonHoc(MonHoc monHoc, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<MonHoc>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.MaMon != null && a.MaKhoa != null)
                            {
                                MonHoc temp = new MonHoc();
                                temp.MaMon = a.MaMon;
                                temp.TenMon = a.TenMon;
                                temp.TichLuy = a.TichLuy;
                                temp.DiemDat = a.DiemDat;
                                temp.MaKhoa = a.MaKhoa;
                                db.MonHocs.Add(temp);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.MaMon == null) data.Add("<li> MaMon property is required</li>");
                                if (a.MaKhoa != null) data.Add("<li> MaKhoa property is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult UploadExcelDiem(DiemHocKy diemHocKy, HttpPostedFileBase FileUpload)
        {
            WebDbContext db = new WebDbContext();
            List<string> data = new List<string>();
            if (FileUpload != null)
            {
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");  
                if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string filename = FileUpload.FileName;
                    string targetpath = Server.MapPath("~/ExcelFormat/");
                    FileUpload.SaveAs(targetpath + filename);
                    string pathToExcelFile = targetpath + filename;
                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                    }

                    var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
                    var ds = new DataSet();
                    try
                    {
                        adapter.Fill(ds, "ExcelTable");
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Excel file has wrong format. Please try another one.";
                    }

                    DataTable dtable = ds.Tables["ExcelTable"];

                    string sheetName = "Sheet1";

                    var excelFile = new ExcelQueryFactory(pathToExcelFile);
                    var artistAlbums = from a in excelFile.Worksheet<DiemHocKy>(sheetName) select a;

                    foreach (var a in artistAlbums)
                    {
                        try
                        {
                            if (a.MSSV != null && a.ID_HocKi != 0 && a.DiemTBTLHe4 != 0)
                            {
                                DiemHocKy temp = new DiemHocKy();
                                temp.MSSV = a.MSSV;
                                temp.ID_HocKi = a.ID_HocKi;
                                temp.SoTCDK = a.SoTCDK;
                                temp.SoTCD = a.SoTCD;
                                temp.SoTCTL = a.SoTCTL;
                                db.DiemHocKys.Add(temp);
                                db.SaveChanges();
                            }
                            else
                            {
                                data.Add("<ul>");
                                if (a.MSSV == null) data.Add("<li> MSSV property is required</li>");
                                if (a.ID_HocKi != 0) data.Add("<li> ID_HocKi property is required</li>");
                                if (a.DiemTBTLHe4 != 0) data.Add("<li> DiemTBTLHe4 property is required</li>");
                                data.Add("</ul>");
                                data.ToArray();
                                return Json(data, JsonRequestBehavior.AllowGet);
                            }
                        }
                        catch (DbEntityValidationException ex)
                        {
                            foreach (var entityValidationErrors in ex.EntityValidationErrors)
                            {

                                foreach (var validationError in entityValidationErrors.ValidationErrors)
                                {

                                    Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);

                                }
                            }
                        }
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(pathToExcelFile)))
                    {
                        System.IO.File.Delete(pathToExcelFile);
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //alert message for invalid file format  
                    data.Add("<ul>");
                    data.Add("<li> Only Excel file format is allowed </li>");
                    data.Add("</ul>");
                    data.ToArray();
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                data.Add("<ul>");
                if (FileUpload == null) data.Add("<li> Please choose Excel file </li>");
                data.Add("</ul>");
                data.ToArray();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}