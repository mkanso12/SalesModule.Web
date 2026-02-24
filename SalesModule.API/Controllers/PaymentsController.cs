using AutoMapper;
using SalesModule.API.Models;
using SalesModule.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SalesModule.API.Controllers
{
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public PaymentsController(
            IPaymentService paymentService,
            IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetPayment(int id)
        {
            var payment = _paymentService.GetPayment(id);
            if (payment == null)
                return NotFound();

            var dto = _mapper.Map<PaymentDto>(payment);
            return Ok(dto);
        }
    }
}