using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ShopApp.Web.Extensions
{
    public static class ITempDataDictionaryExtensions
    {
        // TODO [GM]: Maybe support collections?
        public static void AddSerialized<T>(this ITempDataDictionary dictionary, string key, object item)
        {
            var serializedObject = JObject.FromObject(item);

            serializedObject["Type"] = item.GetType().FullName;

            if (dictionary.ContainsKey(key))
            {
                dictionary.Remove(key);
            }
            
            dictionary.Add(key, serializedObject.ToString());
        }
        
        public static T GetValues<T>(this ITempDataDictionary dictionary, string key)
        {
            dictionary.TryGetValue(key, out object seralizedObj);

            if (seralizedObj != null)
            {
                var jsonStringRepresentationOfObject = seralizedObj.ToString();

                var result = JsonConvert.DeserializeObject<T>(jsonStringRepresentationOfObject);

                return result;
            }

            return default(T);
        }
    }
}