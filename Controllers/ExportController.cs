using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apex_Programming_Assessment.Controllers
{
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult Index()
        {
            return View();
        }
        private class Column : SpreadsheetBuilder.ColumnTemplate<DAL.vCustomerInvoices> { }
        [HttpGet]
        public ActionResult RequestExcelDownload(DateTime startDate, DateTime endDate)
        {
            List<DAL.vCustomerInvoices> invoices = new List<DAL.vCustomerInvoices>();
            using (var db = new DAL.DataAccess())
            {
                invoices = db.vCustomerInvoices.Where(i => i.OrderDate >= startDate && i.OrderDate <= endDate).Take(15).ToList();
            }
            var pkg = new ExcelPackage();
            var wbk = pkg.Workbook;
            var sheet = wbk.Worksheets.Add("Invoice Data");
            var normalStyle = "Normal";
            var acctStyle = wbk.CreateAccountingFormat();
            var data = invoices;
            var columns = new[]
            {

                new Column { Title = "Sold At", Style = normalStyle, Action = i => i.SoldAt, },
                new Column { Title = "Sold To", Style = normalStyle, Action = i => i.FirstName + " " + i.LastName, },
                new Column { Title = "Account Number", Style = normalStyle, Action = i => i.AccountNumber, },
                new Column { Title = "Invoice #", Style = normalStyle, Action = i => i.SalesOrderID, },
                new Column { Title = "Order Date", Style = normalStyle, Action = i => i.OrderDate, },
                new Column { Title = "Due Date", Style = normalStyle, Action = i => i.DueDate, },
                new Column { Title = "Invoice Total", Style = acctStyle, Action = i => i.TotalDue, },
                new Column { Title = "ProductNumber", Style = normalStyle, Action = i => i.ProductNumber, },
                new Column { Title = "Order Qty", Style = normalStyle, Action = i => i.OrderQty, },
                new Column { Title = "Unit Net", Style = normalStyle, Action = i => i.LineTotal / i.OrderQty, },
                new Column { Title = "Line Total", Style = normalStyle, Action = i =>  i.LineTotal, },                
            };

            sheet.SaveData(columns, data);
            // Generate a new unique identifier against which the file can be stored
            string handle = Guid.NewGuid().ToString();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                pkg.SaveAs(memoryStream);
                memoryStream.Position = 0;
                TempData[handle] = memoryStream.ToArray();
            }

            // Note we are returning a filename as well as the handle
            JsonResult result = new JsonResult()
            {
                Data = new { FileGuid = handle, FileName = "AdventureWorksOrderData.xlsx" },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            return result;
        }
        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            //Retrieve the recenetly created file using the guid
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.ms-excel", fileName);
            }
            else
            {
                // Handle errors
                return new EmptyResult();
            }
        }
    }
}