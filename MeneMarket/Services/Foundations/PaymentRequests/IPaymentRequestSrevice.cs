using MeneMarket.Models.Foundations.PaymentRequests;

namespace MeneMarket.Services.Foundations.PaymentRequests
{
    public interface IPaymentRequestSrevice
    {
        ValueTask<PaymentRequest> AddPaymentRequestAsnc(PaymentRequest paymentRequest);
        IQueryable<PaymentRequest> RetrieveAllPaymentRequests(int pageNumber);
        ValueTask<PaymentRequest> ModifyPaymentRequestAsnc(PaymentRequest paymentRequest);
        ValueTask<PaymentRequest> RemovePaymentRequestAsnc(Guid id);
    }
}