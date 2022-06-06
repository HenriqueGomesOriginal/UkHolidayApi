using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HolidayUkFunction
{
    public static class HolidayFunction
    {
        [FunctionName("HolidayFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult(LoadJson());
        }

        public static string LoadJson()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("https://www.gov.uk/bank-holidays.json");
                if (json.Length > 0) { return json; }
                else { return null; }
            }
        }
    }
}
