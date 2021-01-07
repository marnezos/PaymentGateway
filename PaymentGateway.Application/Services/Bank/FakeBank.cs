using PaymentGateway.Application.DTOs.Banks;
using PaymentGateway.Application.Interfaces.Bank;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGateway.Application.Services.Bank
{
    public class FakeBank : IAcquiringBank
    {
        public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
        {
            //Simulate Delay 3s -> 5s
            await Task.Delay(3000 + new Random(DateTime.Now.Millisecond).Next(0, 2000));

            bool success = false;

            //Simulate successful payments by expiration month. Even = success, Odd = failure
            if (request.CardExpirationMonth % 2 == 0)
            {
                success = true;
            }

            return new PaymentResponseDto()
            {
                GatewayUniqueRequestId = request.GatewayUniqueRequestId,
                ResponseId = Guid.NewGuid(),
                Successful = success,
                Timestamp = DateTime.Now
            };
        }
    }
}
