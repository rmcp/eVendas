using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using System.Text.Unicode;

namespace SalesAPI.Extensions
{
    public static class CustomExtensions
    {

        private static readonly UTF8Encoding Utf8NoBom = new UTF8Encoding(false);

        public static Guid ErrorWithId(this ILogger logger, Exception ex)
        {
            var id = Guid.NewGuid();
            logger.LogError(ex.Message,"ErrorId", id);            
            
            return id;
        }

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            Converters = new JsonConverter[] { new StringEnumConverter() }
        };

        /// <summary>
        /// Converts the object to json bytes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static byte[] ToJsonBytes(this object source)
        {
            if (source == null)
                return null;
            var instring = JsonConvert.SerializeObject(source, Formatting.Indented, JsonSettings);
            
            return Utf8NoBom.GetBytes(instring);
        }
    }
}
