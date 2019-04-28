using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace pluralsightfuncs
{
    public static class OnPaymentReceived
    {
        [FunctionName("OnPaymentReceived")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Queue("orders"/*, Connection="MyAppSetting"*/)] IAsyncCollector<Order> orderQueue,
            ILogger log)
            {
                log.LogInformation("Received a payment.");

                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var order = JsonConvert.DeserializeObject<Order>(requestBody);
                await orderQueue.AddAsync(order);
                log.LogInformation($"Order {order.OrderId} received from {order.Email} for product {order.ProductId}");
                return new OkObjectResult($"Thank you for your purchase");
            }
    }
    public class Order
    {
        public string ProductId { get; set; }
        public string Email { get; set; }
        public string OrderId { get; set; }
    }
}