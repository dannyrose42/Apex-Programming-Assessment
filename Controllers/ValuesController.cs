using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apex_Programming_Assessment.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        public void Post([FromBody]string value)
        {
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
