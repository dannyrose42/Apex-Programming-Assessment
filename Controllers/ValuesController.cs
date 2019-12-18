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
        /// <summary>
        ///     Returns data on the the first 15 orders bertween the params startDate and endDate
        /// </summary>
        public IHttpActionResult Get(DateTime startDate, DateTime endDate)
        {
            
            List<DAL.vCustomerInvoices> invoices = new List<DAL.vCustomerInvoices>();
            using (var db = new DAL.DataAccess())
            {
                invoices = db.vCustomerInvoices.Where(i => i.OrderDate >= startDate && i.OrderDate <= endDate).Take(15).ToList();
            }
            return Ok(invoices);
        }
        // GET api/values/5
        /// <summary>
        ///     Returns data on the order with a SalesOrderID matching the id param
        /// </summary>
        public IHttpActionResult Get(int id)
        {
            List<DAL.vCustomerInvoices> invoices = new List<DAL.vCustomerInvoices>();
            using (var db = new DAL.DataAccess())
            {
                invoices = db.vCustomerInvoices.Where(i => i.SalesOrderID == id).ToList();
            }
            return Ok(invoices);
        }
    }
}
