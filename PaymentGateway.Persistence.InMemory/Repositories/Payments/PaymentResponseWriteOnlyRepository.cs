using PaymentGateway.Application.Interfaces.Storage.Write;
using PaymentGateway.Domain.Payments;
using PaymentGateway.Persistence.InMemory.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGateway.Persistence.InMemory.Repositories.Payments
{
    public class PaymentResponseWriteOnlyRepository : BaseWriteRepository<PaymentResponse, DataEntities.Payments.PaymentResponse>, IPaymentResponseWriteOnlyRepository
    {
        public PaymentResponseWriteOnlyRepository(PaymentGatewayContext context) : base(context) { }
    }
}
