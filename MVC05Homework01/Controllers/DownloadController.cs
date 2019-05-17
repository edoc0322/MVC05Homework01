using ClosedXML.Excel;
using MVC05Homework01.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }


        private MemoryStream ExportExcelToMS(string SheetName, DataTable ExportData)
        {
            if (ExportData == null) throw new InvalidDataException("ExportData");
            if (string.IsNullOrWhiteSpace(SheetName)) SheetName = "Data";

            var memoryStream = new MemoryStream();
            try
            {
                var workbook = new XLWorkbook();

                if (ExportData != null)
                {
                    // Add all DataTables in the DataSet as a worksheets
                    workbook.Worksheets.Add(ExportData, SheetName);
                    if (workbook.TryGetWorksheet(SheetName, out IXLWorksheet worksheet))
                    {
                        //自動伸縮欄寬
                        for (int i = 0; i < ExportData.Columns.Count; i++)
                            worksheet.Column(i + 1).AdjustToContents();//Excel欄位從1開始
                    }
                    workbook.SaveAs(memoryStream);
                }
                workbook.Dispose();
                return memoryStream;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public FileResult ExportExcelV2(DataTable ExportData, string FileName = null, string SheetName = null)
        {
            if (ExportData == null) throw new InvalidDataException("ExportData");
            if (string.IsNullOrWhiteSpace(SheetName)) SheetName = "Data";
            if (string.IsNullOrWhiteSpace(FileName)) FileName = string.Concat("ExportData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");

            return File(ExportExcelToMS("Data", ExportData).ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
        }

        //public class ExportExcelResult : ActionResult
        //{
        //    public string SheetName { get; set; }
        //    public string FileName { get; set; }
        //    public DataTable ExportData { get; set; }
        //
        //
        //    public ExportExcelResult()
        //    {
        //
        //    }
        //
        //
        //    public override void ExecuteResult(ControllerContext context)
        //    {
        //        if (ExportData == null)
        //        {
        //            throw new InvalidDataException("ExportData");
        //        }
        //        if (string.IsNullOrWhiteSpace(this.SheetName))
        //        {
        //            this.SheetName = "Sheet1";
        //        }
        //        if (string.IsNullOrWhiteSpace(this.FileName))
        //        {
        //            this.FileName = string.Concat(
        //                "ExportData_",
        //                DateTime.Now.ToString("yyyyMMddHHmmss"),
        //                ".xlsx");
        //        }
        //
        //        this.ExportExcelEventHandler(context);
        //    }
        //
        //    /// <summary>
        //    /// Exports the excel event handler.
        //    /// </summary>
        //    /// <param name="context">The context.</param>
        //    private void ExportExcelEventHandler(ControllerContext context)
        //    {
        //        try
        //        {
        //            var workbook = new XLWorkbook();
        //
        //            if (this.ExportData != null)
        //            {
        //                context.HttpContext.Response.Clear();
        //
        //                // 編碼
        //                context.HttpContext.Response.ContentEncoding = Encoding.UTF8;
        //
        //                // 設定網頁ContentType
        //                context.HttpContext.Response.ContentType =
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //
        //                // 匯出檔名
        //                var browser = context.HttpContext.Request.Browser.Browser;
        //                var exportFileName = browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase)
        //                    ? this.FileName
        //                    : HttpUtility.UrlEncode(this.FileName, Encoding.UTF8);
        //
        //                context.HttpContext.Response.AddHeader(
        //                    "Content-Disposition",
        //                    string.Format("attachment;filename={0}", exportFileName));
        //
        //                // Add all DataTables in the DataSet as a worksheets
        //                workbook.Worksheets.Add(this.ExportData, this.SheetName);
        //
        //                if (workbook.TryGetWorksheet(this.SheetName, out IXLWorksheet worksheet))
        //                {
        //                    //自動伸縮欄寬
        //                    for (int i = 0; i < this.ExportData.Columns.Count; i++)
        //                        worksheet.Column(i + 1).AdjustToContents();//Excel欄位從1開始
        //                }
        //
        //                using (var memoryStream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(memoryStream);
        //                    memoryStream.WriteTo(context.HttpContext.Response.OutputStream);
        //                    memoryStream.Close();
        //                }
        //            }
        //            workbook.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}
    }
}