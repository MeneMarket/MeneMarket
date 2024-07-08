using MeneMarket.Models.Foundations.PaymentRequests;

namespace MeneMarket.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<PaymentRequest> InsertPaymentRequestAsync(PaymentRequest paymentRequest);
        IQueryable<PaymentRequest> SelectAllPaymentRequests();
        ValueTask<PaymentRequest> SelectPaymentRequestByIdAsync(Guid id);
        ValueTask<PaymentRequest> UpdatePaymentRequestAsync(PaymentRequest paymentRequest);
        ValueTask<PaymentRequest> DeletePaymentRequestAsync(PaymentRequest paymentRequest);
    }
}