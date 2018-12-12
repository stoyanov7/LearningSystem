namespace LearningSystem.Utilities.Common
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public static class SessionExtensions
    {
        public static void Put<T>(this ISession session, string key, T value)
            where T : class
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
            where T : class
        {
            var result = session.GetString(key);

            return result == null ? null : JsonConvert.DeserializeObject<T>(result);
        }
    }
}