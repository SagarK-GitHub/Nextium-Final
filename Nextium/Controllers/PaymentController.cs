using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;
using System.Collections.Generic;

namespace Nextium.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] OrderRequest request)
        {
            var client = new RazorpayClient("rzp_test_UpCAVDOPC6txDy", "RVx3SowJLjmeu9j1BcEiAx3L");

            var options = new Dictionary<string, object>
        {
            { "amount", request.Amount * 100 }, // Amount is in currency subunits. Default currency is INR.
            { "currency", "INR" },
            { "receipt", "order_rcptid_11" }
        };

            var order = client.Order.Create(options);
            return Ok(order);
        }
        public IActionResult Index()
        {
            return View();
        }
    }

    public class OrderRequest
    {
        public int Amount { get; set; }
    }
}
