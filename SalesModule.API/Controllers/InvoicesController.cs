using SalesModule.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SalesModule.API.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : ApiController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [Route("{id}/post")]
        public IHttpActionResult PostInvoice(int id)
        {
            if (id <= 0)
                return BadRequest("Invoice ID must be a positive integer.");
            try
            {
                _invoiceService.PostInvoice(id);
                return Ok(new { message = "Invoice posted successfully." });
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}