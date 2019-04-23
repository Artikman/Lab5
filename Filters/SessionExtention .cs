using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Lab_4
{
    public static class SessionExtention
    {
        public static void Set(this ISession session, string key, object pairs)
        {
            session.SetString(key, JsonConvert.SerializeObject(pairs));
        }

        public static T Get<T>(this ISession session, string key)
        {
            return JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}