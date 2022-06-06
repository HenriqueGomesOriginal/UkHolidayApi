using HolidaysUkApi.Configuration;
using HolidaysUkApi.Models;
using Microsoft.Extensions.Options;

namespace HolidaysUkApi.Services.Implementation
{
    public class UkHolidayService : IUkHolidayService
    {
        private AzureFunctions _azureFunctions { get; }

        public UkHolidayService(IOptions<AzureFunctions> azureFunctions)
        {
            _azureFunctions = azureFunctions.Value ?? throw new ArgumentNullException(nameof(_azureFunctions));
        }

        public async Task<ItemReturn> GetHolidayUk(FilterParams filterParams)
        {
            try
            {
                HttpClient client = new HttpClient();

                HttpResponseMessage response = await client.GetAsync(_azureFunctions.HolidayUkFunctionUrl);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<ItemReturn>();
                }

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
