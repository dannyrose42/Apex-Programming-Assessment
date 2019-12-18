using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Apex_Programming_Assessment.Controllers
{
    public class ValuesController : ApiController
    {
        private class Column : SpreadsheetBuilder.ColumnTemplate<DAL.vCustomerInvoices> { }
        // GET api/values
        public IHttpActionResult Get(DateTime startDate, DateTime endDate)
        {
            
            List<DAL.vCustomerInvoices> invoices = new List<DAL.vCustomerInvoices>();
            using (var db = new DAL.DataAccess())
            {
                invoices = db.vCustomerInvoices.Where(i => i.OrderDate >= startDate && i.OrderDate <= endDate).Take(15).ToList();
            }
            return Ok(invoices);
        }
        public HttpResponseMessage Get(DateTime startDate, DateTime endDate, bool getSpreadSheet)
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
                new Column { Title = "Unit Net", Style = normalStyle, Action = i => i.UnitPrice, },
                new Column { Title = "Line Total", Style = normalStyle, Action = i => i.LineTotal, },
                //new Column { Title = "Invoice Total", Style = acctStyle, Action = i => i.InvoiceTotal, TotalAction = () => data.Sum(x=>x.InvoiceTotal), },
            };

            sheet.SaveData(columns, data);

            var bytes = pkg.GetAsByteArray();

            var stream = new MemoryStream(bytes);
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Invoices.xls"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

             return result;
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            List<DAL.vCustomerInvoices> invoices = new List<DAL.vCustomerInvoices>();
            using (var db = new DAL.DataAccess())
            {
                invoices = db.vCustomerInvoices.Where(i => i.SalesOrderID == id).ToList();
            }
            return Ok(invoices);
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
