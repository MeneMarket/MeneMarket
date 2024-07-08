using MeneMarket.Models.Foundations.PaymentRequests;

namespace MeneMarket.Services.Orchestrations.PaymentRequests
{
    public interface IPaymentRequestOrchestrationService
    {
        ValueTask<PaymentRequest> AddPaymentRequestAsync(PaymentRequest paymentRequest);
        IQueryable<PaymentRequest> RetrieveAllPaymentRequests(int pageNumber);
        ValueTask<PaymentRequest> ModifyPaymentRequestAsync(PaymentRequest paymentRequest);
        ValueTask<PaymentRequest> RemovePaymentRequestAsync(Guid id);
    }
}