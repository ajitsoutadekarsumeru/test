using System.Text;
using System.Text.Json;

namespace ENTiger.ENCollect
{
    public static class CommonExtensions
    {
        public static StringContent AsJson(this object o)
            => new StringContent(JsonSerializer.Serialize(o), Encoding.UTF8, "application/json");
    }
}