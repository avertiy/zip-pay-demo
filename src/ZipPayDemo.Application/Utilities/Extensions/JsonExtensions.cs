using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZipPayDemo.Application.Utilities.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(
          this T data,
          SerializationType? serializationType = null,
          bool referenceLoopIgnore = false)
        {
            if ((object)data != null)
                return JsonConvert.SerializeObject((object)data, JsonExtensions.GetSerializationSettings(serializationType, referenceLoopIgnore));
            throw new ArgumentNullException(nameof(data));
        }

        public static T FromJson<T>(this string data, SerializationType? serializationType = null) => !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<T>(data, JsonExtensions.GetSerializationSettings(serializationType)) : default(T);

        public static string ToJsonString<T>(this T data, bool referenceLoopIgnore = false) => data.ToJson<T>(referenceLoopIgnore: referenceLoopIgnore);

        public static string ToCamelCaseJsonString<T>(this T data, bool referenceLoopIgnore = false) => data.ToJson<T>(new SerializationType?(SerializationType.CamelCase), referenceLoopIgnore);

        public static string ToSnakeCaseJsonString<T>(this T data, bool referenceLoopIgnore = false) => data.ToJson<T>(new SerializationType?(SerializationType.SnakeCase), referenceLoopIgnore);

        public static T FromJsonCamelCaseStringToObject<T>(this string data) => data.FromJson<T>(new SerializationType?(SerializationType.CamelCase));

        public static T FromJsonSnakeCaseStringToObject<T>(this string data) => data.FromJson<T>(new SerializationType?(SerializationType.SnakeCase));

        private static JsonSerializerSettings GetSerializationSettings(
          SerializationType? serializationType,
          bool referenceLoopIgnore = false)
        {
            JsonSerializerSettings serializerSettings = (JsonSerializerSettings)null;
            if (serializationType.HasValue)
            {
                NamingStrategy namingStrategy1;
                if (serializationType.HasValue)
                {
                    switch (serializationType.GetValueOrDefault())
                    {
                        case SerializationType.CamelCase:
                            CamelCaseNamingStrategy caseNamingStrategy1 = new CamelCaseNamingStrategy();
                            caseNamingStrategy1.OverrideSpecifiedNames = false;
                            caseNamingStrategy1.ProcessDictionaryKeys = true;
                            namingStrategy1 = (NamingStrategy)caseNamingStrategy1;
                            goto label_6;
                        case SerializationType.SnakeCase:
                            SnakeCaseNamingStrategy caseNamingStrategy2 = new SnakeCaseNamingStrategy();
                            caseNamingStrategy2.OverrideSpecifiedNames = false;
                            caseNamingStrategy2.ProcessDictionaryKeys = true;
                            namingStrategy1 = (NamingStrategy)caseNamingStrategy2;
                            goto label_6;
                    }
                }
                CamelCaseNamingStrategy caseNamingStrategy3 = new CamelCaseNamingStrategy();
                caseNamingStrategy3.OverrideSpecifiedNames = false;
                caseNamingStrategy3.ProcessDictionaryKeys = true;
                namingStrategy1 = (NamingStrategy)caseNamingStrategy3;
                label_6:
                NamingStrategy namingStrategy2 = namingStrategy1;
                serializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = (IContractResolver)new DefaultContractResolver()
                    {
                        NamingStrategy = namingStrategy2
                    }
                };
            }
            if (!referenceLoopIgnore)
                return serializerSettings;
            if (serializerSettings == null)
                serializerSettings = new JsonSerializerSettings();
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            return serializerSettings;
        }
    }

    public enum SerializationType
    {
        CamelCase,
        SnakeCase,
    }
}
