using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using MD5 = System.Security.Cryptography.MD5;

namespace pluralsightfuncs
{
    public static class GenerateLicenseFile
    {
        [FunctionName("GenerateLicenseFile")]
        public static void Run(
            [QueueTrigger("orders", Connection = "AzureWebJobsStorage")]
            Order order,
            [Blob("orders/{rand-guid}.json", FileAccess.Write)]
            TextWriter outputBlob,
            ILogger log)
        {
            outputBlob.WriteLine($"OrderId: {order.OrderId}");
            outputBlob.WriteLine($"Email: {order.Email}");
            outputBlob.WriteLine($"ProductId: {order.ProductId}");
            outputBlob.WriteLine($"PurchaseDate: {DateTime.UtcNow}");

            var bytes = Encoding.UTF8.GetBytes(order.Email + "secret");
            var hash = MD5.Create().ComputeHash(bytes);
            outputBlob.WriteLine($"SecretCode: {BitConverter.ToString(hash).Replace("-", "")}");

            
        }
    }
}
