using MeneMarket.Models.Foundations.PaymentRequests;
using Microsoft.EntityFrameworkCore;

namespace MeneMarket.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PaymentRequest> PaymentRequests;

        public async ValueTask<PaymentRequest> InsertPaymentRequestAsync(PaymentRequest paymentRequest) =>
            await InsertAsync(paymentRequest);

        public IQueryable<PaymentRequest> SelectAllPaymentRequests() =>
            SelectAll<PaymentRequest>();

        public async ValueTask<PaymentRequest> SelectPaymentRequestByIdAsync(Guid id) =>
            await SelectAsync<PaymentRequest>(id);

        public async ValueTask<PaymentRequest> UpdatePaymentRequestAsync(PaymentRequest paymentRequest) =>
            await UpdateAsync(paymentRequest);

        public async ValueTask<PaymentRequest> DeletePaymentRequestAsync(PaymentRequest paymentRequest) =>
            await DeleteAsync(paymentRequest);
    }
}
