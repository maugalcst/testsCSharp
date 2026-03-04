using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly OrderCounterService _orderCounter;

        // 👇 "Oye, necesito un EmailService. Alguien me lo dará."
        public OrderController(INotificationService notificationService, OrderCounterService orderCounter)
        {
            _notificationService = notificationService;
            _orderCounter = orderCounter;
        }

        [HttpPost]
        public IActionResult PlaceOrder(string product)
        {
            var orderNumber = _orderCounter.AddOrder();
            _notificationService.Send($"Your order for {product} is confirmed!");
            return Ok($"Order placed! You are order number {orderNumber}");
        }
    }
}