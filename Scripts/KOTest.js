



$(function () {
    $("#startDate").datepicker();
    $("#startDate").datepicker("setDate", "07/01/2011");

    $("#endDate").datepicker();
    $("#endDate").datepicker("setDate", "07/02/2011");
});

$.get("api/values", {
    startDate: "01/12/2012",
    endDate: "02/12/2012",
})
    .done(function (data) {
        initialData = data;
        ko.applyBindings(new PagedGridModel(initialData))
    });

var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.addItem = function () {
        $.get("api/values", {
            startDate: "01/12/2012",
            endDate: "02/12/2012",
            getSpreadSheet: true
        })
            .done(function (data) {
                
            });
    };

    this.gridViewModel = new ko.simpleGrid.viewModel({
        data: this.items,
        columns: [
            { headerText: "Sold At", rowText: "SoldAt" },
            { headerText: "Sold To", rowText: function (item) { return item.FirstName + " " + item.LastName } },                        
            { headerText: "Account Number", rowText: "AccountNumber" },
            { headerText: "Invoice #", rowText: "SalesOrderID" },
            { headerText: "Customer PO #", rowText: "PurchaseOrderNumber" },
            { headerText: "Order Date", rowText: "OrderDate" },
            { headerText: "Due Date", rowText: "DueDate" },
            { headerText: "Invoice Total", rowText: function (item) { return "$" + item.TotalDue.toFixed(2) } },
            { headerText: "Product Number", rowText: "ProductNumber" },
            { headerText: "Order Qty", rowText: "OrderQty" },
            { headerText: "Unit Net", rowText: function (item) { return "$" + item.UnitPrice.toFixed(2) } },
            { headerText: "Line Total", rowText: function (item) { return "$" + item.LineTotal.toFixed(2) } }
        ],
        pageSize: 15
    });
};

//ko.applyBindings(new PagedGridModel(initialData))