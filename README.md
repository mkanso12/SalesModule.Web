# Sales Module

A complete sales management module built with ASP.NET Web Forms, Web API, LINQ to SQL, and Unity DI, targeting .NET Framework 4.8.

## Features
- **Customers** – Manage customer records.
- **Items** – Manage inventory items with on?hand quantity.
- **Sales Orders** – Create orders with multiple line items; status management.
- **Invoices** – Create invoices from sales orders (auto?calculate totals with 11% VAT), post invoices (updates inventory and creates GL entries).
- **Payments** – Record payments against invoices (supports partial payments), updates invoice status and creates corresponding GL entries (Dr Cash, Cr AR).
- **API Endpoints** – RESTful API for payment retrieval and invoice posting.

## Technology Stack
- **Backend**: .NET Framework 4.8, LINQ to SQL
- **Database**: SQL Server (MSSQL)
- **Presentation**: Web Forms & Web API
- **Dependency Injection**: Unity Container
- **Mapping**: AutoMapper (for API DTOs)

## Database Setup
1. Run the script `Database/SalesModule.sql` to create the database, tables, indexes, stored procedure, and sample data.
2. The script includes:
   - Tables: Customer, Item, SalesOrder, SalesOrderLine, Invoice, InvoiceLine, Payment, GLTransaction, GLTransactionLine.
   - Indexes: `IX_Invoice_CustomerId_Status`, `IX_InvoiceLine_ItemId`.
   - Stored procedure: `sp_PostInvoice`.
   - Sample data: customers, items, a sales order, and an open invoice.

## Running the Application
1. Clone the repository.
2. Open the solution in Visual Studio (2019 or 2022).
3. Update the connection strings in `Web.config` (Web and API projects) to point to your SQL Server instance.
4. Build the solution (restore NuGet packages if needed).
5. Set `SalesModule.Web` as startup project and press F5.
   - To also run the API separately, set multiple startup projects.

## API Endpoints
- `GET /api/payments/{id}` – Returns payment details (AutoMapper DTO).
- `POST /api/invoices/{id}/post` – Posts an invoice (calls stored procedure).

## Key Design Decisions
- **LINQ to SQL** for simple, fast data access.
- **Repository pattern** with interfaces to keep data access abstracted.
- **Service layer** with validation and business rules.
- **Unity DI** for loose coupling and testability.
- **AutoMapper** for clean DTO mapping in API.
- **Stored procedure** for posting invoices to ensure transactional integrity and complex logic.
- **DTOs for session** to avoid DataContext disposal issues.

## SQL Query (Open Invoices by Customer)
See `OpenInvoicesByCustomer.sql` for the required JOIN query.

## Index Justification
- **IX_Invoice_CustomerId_Status** – Speeds up the open invoices query and filtering by customer and status, including GrossTotal to avoid key lookups.
- **IX_InvoiceLine_ItemId** – Optimizes inventory updates when posting invoices, by quickly locating lines by ItemId and including Qty.
