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
        public FileResult DownloadExcelSinhVienForMat()
        {
            string path = "/ExcelFormat/Students.xlsx";
            return File(path, "application/vnd.ms-excel", "Students.xlsx");
        }
        [HttpPost]
        public ActionResult UploadExcelSinhVien(SinhVien students, HttpPostedFileBase FileUpload)
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
                    return RedirectToAction("Index","SinhViens");
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
    }
}