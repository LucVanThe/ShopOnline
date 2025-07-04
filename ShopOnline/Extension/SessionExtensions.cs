using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.Models;
using ShopOnline.ViewModel;
using ShopOnline.Extension;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShopOnline.Extension
{
    public static class SessionExtensions
    {
        //public static void Set<T>(this ISession session, string key, T value)
        //{
        //    var serializedValue = JsonConvert.SerializeObject(value);
        //    session.SetString(key, serializedValue);
        //}

        //public static T? Get<T>(this ISession session, string key)
        //{
        //    var value = session.GetString(key);
        //    return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        //}
        public static void SetObject<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
    }
}
   