using HolidaysUkApi.Configuration;
using HolidaysUkApi.Models;
using HolidaysUkApi.Models.DataReturn;
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

        // Return object in range of date
        public List<Events> FilterItemDate(FilterParams filterParams, List<Events> data)
        {
            var events = new List<Events>();

            foreach (var item in data)
            {
                // Convert string to datetime
                var date = DateTime.Parse(item.date);
                if (date.Year == filterParams.date.Year)
                {
                    events.Add(item);
                }
            }

            return events;
        }

        // Find location from Data
        public ItemReturn FilterItem(FilterParams filterParams, ItemReturn item)
        {
            // Init vars
            var ret = new ItemReturn();
            var events = new List<Events>();

            // Compare location and call filter for dates
            switch (filterParams.location)
            {
                case "northern-ireland":
                    events = FilterItemDate(filterParams, item.northIreland.events);
                    return new ItemReturn
                    {
                        northIreland = new DataReturn
                        {
                            division = item.northIreland.division,
                            events = events
                        },
                        england = new DataReturn(),
                        scotland = new DataReturn()
                    };
                case "england-and-wales":
                    events = FilterItemDate(filterParams, item.england.events);
                    return new ItemReturn
                    {
                        england = new DataReturn
                        {
                            division = item.england.division,
                            events = events
                        },
                        northIreland = new DataReturn(),
                        scotland = new DataReturn()
                    };
                case "scotland":
                    events = FilterItemDate(filterParams, item.scotland.events);
                    return new ItemReturn
                    {
                        scotland = new DataReturn
                        {
                            division = item.scotland.division,
                            events = events
                        },
                        england = new DataReturn(),
                        northIreland = new DataReturn()
                    }; ;
                default:
                    return null;
            };
        }

        public async Task<ItemReturn> GetHolidayUk(FilterParams filterParams)
        {
            // Get all JSON result
            var item = await GetAllHolidayUk();
            return FilterItem(filterParams, item);
        }


        // Download JSON file from Azure Function
        public async Task<ItemReturn> GetAllHolidayUk()
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
