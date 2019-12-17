//var initialData = [
//    { name: "Well-Travelled Kitten", sales: 352, price: 75.95 },
//    { name: "Speedy Coyote", sales: 89, price: 190.00 },
//    { name: "Furious Lizard", sales: 152, price: 25.00 },
//    { name: "Indifferent Monkey", sales: 1, price: 99.95 },
//    { name: "Brooding Dragon", sales: 0, price: 6350 },
//    { name: "Ingenious Tadpole", sales: 39450, price: 0.35 },
//    { name: "Optimistic Snail", sales: 420, price: 1.50 }
//];

$.get("api/values/43860", function (data) {
    
    initialData = data;
    ko.applyBindings(new PagedGridModel(initialData))
});

var PagedGridModel = function (items) {
    this.items = ko.observableArray(items);

    this.addItem = function () {
        this.items.push({ name: "New item", sales: 0, price: 100 });
    };

    this.sortByName = function () {
        this.items.sort(function (a, b) {
            return a.name < b.name ? -1 : 1;
        });
    };

    this.jumpToFirstPage = function () {
        this.gridViewModel.currentPageIndex(0);
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