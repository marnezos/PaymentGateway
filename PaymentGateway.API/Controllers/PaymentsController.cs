using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.API.ViewModels.Payments;
using PaymentGateway.Application.DTOs;
using PaymentGateway.Application.DTOs.Payments.PaymentDetails;
using PaymentGateway.Application.DTOs.Payments.ProcessPayment;
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

        [HttpGet]
        [Route("{request}")]
        [Authorize(Policy = "MerchantsOnly")]
        public async Task<ActionResult<PaymentDetailsResponse>> GetAsync([FromQuery] PaymentDetailsRequest request)
        {

            ClaimsPrincipal currentUser = User;
            string serial = currentUser.FindFirst(ClaimTypes.SerialNumber).Value;

            PaymentDetailsRequestDto requestDto = request;
            requestDto.MerchantId = int.Parse(serial);

            //Assume max timeout @ 60 secs
            ApplicationMessage<PaymentDetailsResponseDto> reply = await _bus.SendRequest<ApplicationMessage<PaymentDetailsResponseDto>>(requestDto, null, TimeSpan.FromSeconds(60));
            if (reply.ServiceSuccess)
            {
                return Ok(reply.Payload);
            }
            else
            {
                return new NotFoundObjectResult(new { Error=reply.ErrorMessage });
            }
        }

        [HttpPost]
        [Authorize(Policy = "MerchantsOnly")]
        public async Task<ActionResult<ProcessPaymentResponse>> PostAsync([FromBody] ProcessPaymentRequest request)
        {
            ClaimsPrincipal currentUser = User;
            string serial = currentUser.FindFirst(ClaimTypes.SerialNumber).Value;

            PaymentProcessRequestDto requestDto = request;
            requestDto.MerchantId = int.Parse(serial);

            //Assume max timeout @ 60 secs
            ApplicationMessage<ProcessedPaymentStatusDto> reply = await _bus.SendRequest<ApplicationMessage<ProcessedPaymentStatusDto>>(requestDto, null, TimeSpan.FromSeconds(60));
            if (reply.ServiceSuccess)
            {
                return Ok(reply.Payload);
            }
            else
            {
                return new BadRequestObjectResult(new { Error = reply.ErrorMessage });
            }

        }

    }
}
