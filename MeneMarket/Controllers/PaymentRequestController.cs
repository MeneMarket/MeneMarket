using MeneMarket.Models.Foundations.PaymentRequests;
using MeneMarket.Services.Orchestrations.PaymentRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace MeneMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentRequestController : RESTFulController
    {
        private readonly IPaymentRequestOrchestrationService paymentRequestOrchestrationService;

        public PaymentRequestController(IPaymentRequestOrchestrationService paymentRequestOrchestrationService) =>
            this.paymentRequestOrchestrationService = paymentRequestOrchestrationService;

        [HttpPost]
        [Authorize(Roles = "User")]
        public async ValueTask<ActionResult<PaymentRequest>> PostPaymentRequestAsync(PaymentRequest paymentRequest) =>
            await this.paymentRequestOrchestrationService.AddPaymentRequestAsync(paymentRequest);

        [HttpGet]
        [Authorize(Roles = "Admin,Owner")]
        public ActionResult<IQueryable<PaymentRequest>> GetAllPaymentRequests(int pageNumber)
        {
            var allpaymentRequests =
                this.paymentRequestOrchestrationService.RetrieveAllPaymentRequests(pageNumber);

            return Ok(allpaymentRequests);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Owner")]
        public async ValueTask<ActionResult<PaymentRequest>> PutPaymentRequestAsync(PaymentRequest paymentRequest) =>
            await this.paymentRequestOrchestrationService.ModifyPaymentRequestAsync(paymentRequest);

        [HttpDelete]
        [Authorize(Roles = "Owner")]
        public async ValueTask<ActionResult<PaymentRequest>> DeletePaymentRequestAsync(Guid id) =>
            await this.paymentRequestOrchestrationService.RemovePaymentRequestAsync(id);
    }
}