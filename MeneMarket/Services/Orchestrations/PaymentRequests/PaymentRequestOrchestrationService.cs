using MeneMarket.Models.Foundations.PaymentRequests;
using MeneMarket.Services.Foundations.PaymentRequests;
using MeneMarket.Services.Foundations.Users;

namespace MeneMarket.Services.Orchestrations.PaymentRequests
{
    public class PaymentRequestOrchestrationService : IPaymentRequestOrchestrationService
    {
        private readonly IPaymentRequestSrevice paymentRequestSrevice;
        private readonly IUserService userService;

        public PaymentRequestOrchestrationService(
            IPaymentRequestSrevice paymentRequestSrevice,
            IUserService userService)
        {
            this.paymentRequestSrevice = paymentRequestSrevice;
            this.userService = userService;
        }

        public async ValueTask<PaymentRequest> AddPaymentRequestAsync(PaymentRequest paymentRequest)
        {
            var user = await this.userService.RetrieveUserByIdAsync(paymentRequest.UserId);

            if (user == null || user.Balance < paymentRequest.Amount)
            {
                throw new Exception("Invalid Operation");
            }
            else
                paymentRequest.Id = Guid.NewGuid();
            paymentRequest.UserImage = user.Image;
            paymentRequest.CreatedDate = DateTime.Now;

            return await this.paymentRequestSrevice.AddPaymentRequestAsnc(paymentRequest);
        }

        public async ValueTask<PaymentRequest> ModifyPaymentRequestAsync(PaymentRequest paymentRequest) =>
            await this.paymentRequestSrevice.ModifyPaymentRequestAsnc(paymentRequest);

        public async ValueTask<PaymentRequest> RemovePaymentRequestAsync(Guid id) =>
            await this.paymentRequestSrevice.RemovePaymentRequestAsnc(id);

        public IQueryable<PaymentRequest> RetrieveAllPaymentRequests(int pageNumber) =>
            this.paymentRequestSrevice.RetrieveAllPaymentRequests(pageNumber);
    }
}
