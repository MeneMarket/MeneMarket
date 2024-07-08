using MeneMarket.Brokers.Storages;
using MeneMarket.Models.Foundations.PaymentRequests;

namespace MeneMarket.Services.Foundations.PaymentRequests
{
    public class PaymentRequestSrevice : IPaymentRequestSrevice
    {
        private readonly IStorageBroker storageBroker;

        public PaymentRequestSrevice(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<PaymentRequest> AddPaymentRequestAsnc(PaymentRequest paymentRequest) =>
            await this.storageBroker.InsertPaymentRequestAsync(paymentRequest);

        public async ValueTask<PaymentRequest> RemovePaymentRequestAsnc(Guid id)
        {
            var payment = await this.storageBroker.SelectPaymentRequestByIdAsync(id);

            return await this.storageBroker.DeletePaymentRequestAsync(payment);
        }

        public IQueryable<PaymentRequest> RetrieveAllPaymentRequests(int pageNumber)
        {
            int pageSize = 100;
            int usersToSkip = (pageNumber - 1) * pageSize;

            IQueryable<PaymentRequest> paymentRequests = this.storageBroker.SelectAllPaymentRequests()
                                                        .Skip(usersToSkip)
                                                        .Take(pageSize);

            return paymentRequests;
        }

        public async ValueTask<PaymentRequest> ModifyPaymentRequestAsnc(PaymentRequest paymentRequest) =>
            await this.storageBroker.UpdatePaymentRequestAsync(paymentRequest);
    }
}