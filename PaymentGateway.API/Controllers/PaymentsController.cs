using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ProcessedPaymentStatusDto> PostAsync([FromBody] PaymentProcessRequestDto request)
        {
            ClaimsPrincipal currentUser = User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var serial = currentUser.FindFirst(ClaimTypes.SerialNumber).Value;

            var reply = await _bus.SendRequest<ProcessedPaymentStatusDto>(request, null, TimeSpan.FromSeconds(10));

            return reply;
        }

    }
}
