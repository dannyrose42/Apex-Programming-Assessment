using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apex_Programming_Assessment.Models
{
	public class CustomerInvoice
	{
		public string InvoiceNumber { get; set; }
		public decimal InvoiceTotal { get; set; }
		public string SoldAt { get; set; }
		public string SoldTo { get; set; }
		public string AccountNumber { get; set; }
		public string CustomerPONumber { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime DueDate { get; set; }
		public string ProductNumber { get; set; }
		public int OrderQty { get; set; }
		public decimal UnitNet { get; set; }
		public decimal LineTotal { get; set; }
	}
}