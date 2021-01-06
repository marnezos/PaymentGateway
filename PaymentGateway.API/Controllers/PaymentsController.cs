using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.ViewModels.Payments;
using PaymentGateway.Application.DTOs.Payments;
using Rebus;
using Rebus.Bus;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaymentGateway.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]//No need to check ModelState.IsValid
    public class PaymentsController : ControllerBase
    {
        private readonly IBus _bus;
        public PaymentsController(IBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [Authorize(Policy = "MerchantsOnly")]
        public async Task<ProcessedPaymentStatusDto> PostAsync([FromBody] ProcessPaymentRequest request)
        {
            ClaimsPrincipal currentUser = User;
            string serial = currentUser.FindFirst(ClaimTypes.SerialNumber).Value;

            PaymentProcessRequestDto requestDto = request;
            requestDto.MerchantId = int.Parse(serial);

            //Assume max timeout @ 60 secs
            ProcessedPaymentStatusDto reply = await _bus.SendRequest<ProcessedPaymentStatusDto>(requestDto, null, TimeSpan.FromSeconds(60));

            return reply;
        }

    }
}
