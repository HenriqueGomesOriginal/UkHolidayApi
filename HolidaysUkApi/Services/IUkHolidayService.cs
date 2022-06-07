using HolidaysUkApi.Models;

namespace HolidaysUkApi.Services
{
    public interface IUkHolidayService
    {
        public Task<ItemReturn> GetHolidayUk(FilterParams filterParams);
        public Task<ItemReturn> GetAllHolidayUk();
    }
}
