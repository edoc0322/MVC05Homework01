using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC05Homework01.Services
{
    public class ExcelService<T> where T : class
    {
        public string FileName { get; set; }
        public string SheetName { get; set; }

        //public MemoryStream ExportExcel(IEnumerable<T> exportData, string FileName, string SheetName)
        //{
        //    #region 初始化

        //    if (exportData == null)
        //        throw new InvalidDataException("ExportData");
        //    if (string.IsNullOrWhiteSpace(this.SheetName))
        //        this.SheetName = "Sheet1";
        //    else
        //        this.SheetName = SheetName;
        //    if (string.IsNullOrWhiteSpace(this.FileName))
        //        this.FileName = string.Concat("ExportData_", DateTime.Now.ToString("yyyyMMddHHmmss"), ".xlsx");
        //    else
        //        this.FileName = FileName;

        //    #endregion

        //    #region  Export

        //    try
        //    {
        //        var workbook = new XLWorkbook();

        //        if (this.ExportData != null)
        //        {
        //            var MemoryStream = new MemoryStream();

        //            context.HttpContext.Response.Clear();

        //            // 編碼
        //            context.HttpContext.Response.ContentEncoding = Encoding.UTF8;

        //            // 設定網頁ContentType
        //            context.HttpContext.Response.ContentType =
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //            // 匯出檔名
        //            var browser = context.HttpContext.Request.Browser.Browser;
        //            var exportFileName = browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase)
        //                ? this.FileName
        //                : HttpUtility.UrlEncode(this.FileName, Encoding.UTF8);

        //            context.HttpContext.Response.AddHeader(
        //                "Content-Disposition",
        //                string.Format("attachment;filename={0}", exportFileName));

        //            // Add all DataTables in the DataSet as a worksheets
        //            workbook.Worksheets.Add(this.ExportData, this.SheetName);

        //            using (var memoryStream = new MemoryStream())
        //            {
        //                workbook.SaveAs(memoryStream);
        //                memoryStream.WriteTo(context.HttpContext.Response.OutputStream);
        //                memoryStream.Close();
        //            }
        //        }
        //        workbook.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //    #endregion






        //    return null;
        //}
    }
}