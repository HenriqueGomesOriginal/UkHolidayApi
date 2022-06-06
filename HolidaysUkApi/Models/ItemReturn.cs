using System.Text.Json.Serialization;
using HolidaysUkApi.Models.DataReturn;

namespace HolidaysUkApi.Models
{
    public class ItemReturn
    {
        [JsonPropertyName("england-and-wales")]
        public DataReturn.DataReturn england { get; set; }
        public DataReturn.DataReturn scotland { get; set; }
        [JsonPropertyName("northern-ireland")]
        public DataReturn.DataReturn northIreland { get; set; }
    }
}
