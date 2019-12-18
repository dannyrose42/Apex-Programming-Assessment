CREATE VIEW [Sales].[vCustomerInvoices] AS 
SELECT DISTINCT 
	s.Name as [SoldAt],
	per.FirstName,
	per.LastName,
	sod.[SalesOrderID],
	c.AccountNumber,
	soh.PurchaseOrderNumber,
	soh.OrderDate,
	soh.DueDate,
	soh.TotalDue,
	p.ProductNumber,
	sod.OrderQty,
	sod.UnitPrice,
	sod.UnitPriceDiscount,	
	sod.LineTotal	
FROM [AdventureWorks2017].[Sales].[SalesOrderDetail] sod
	JOIN [AdventureWorks2017].[Sales].[SalesOrderHeader] soh ON soh.SalesOrderID = sod.SalesOrderID
	JOIN [AdventureWorks2017].[Production].[Product] p ON sod.ProductID = p.ProductID
	JOIN [AdventureWorks2017].[Sales].[Customer] c ON c.CustomerID = soh.CustomerID
	JOIN [AdventureWorks2017].[Sales].[Store] s ON s.BusinessEntityID = c.StoreID
	JOIN [AdventureWorks2017].[Person].[Person] per ON per.BusinessEntityID = c.PersonID


