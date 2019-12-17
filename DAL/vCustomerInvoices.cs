namespace Apex_Programming_Assessment.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sales.vCustomerInvoices")]
    public partial class vCustomerInvoices
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string SoldAt { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string LastName { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SalesOrderID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(10)]
        public string AccountNumber { get; set; }

        [StringLength(25)]
        public string PurchaseOrderNumber { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime OrderDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime DueDate { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "money")]
        public decimal TotalDue { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(25)]
        public string ProductNumber { get; set; }

        [Key]
        [Column(Order = 9)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short OrderQty { get; set; }

        [Key]
        [Column(Order = 10, TypeName = "money")]
        public decimal UnitPrice { get; set; }

        [Key]
        [Column(Order = 11, TypeName = "money")]
        public decimal UnitPriceDiscount { get; set; }

        [Key]
        [Column(Order = 12, TypeName = "numeric")]
        public decimal LineTotal { get; set; }
    }
}
