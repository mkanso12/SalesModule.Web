SELECT i.Id AS InvoiceId, i.Date, i.GrossTotal, i.Status, c.Name AS CustomerName
FROM Invoice i
INNER JOIN Customer c ON i.CustomerId = c.Id
WHERE i.Status IN ('Open', 'PartiallyPaid')
ORDER BY c.Name, i.Date;