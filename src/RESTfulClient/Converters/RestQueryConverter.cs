using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace RESTfulClient.Converters
{
    public class RestQueryConverter : IQueryConverter
    {
        public string Serialize(object request)
        {
            IEnumerable<string> array = GetNameValueCollection(request)
                .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}");
            
            return string.Format("?{0}", string.Join("&", array));
        }

        private IList<KeyValuePair<string, string>> GetNameValueCollection(object request)
        {
            var nvc = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo prop in request.GetType().GetTypeInfo().DeclaredProperties)
            {
                object value = prop.GetValue(request, null);
                if (value == null) continue;
                string propName = prop.Name.ToLower();

                if (!IsEnumerableProperty(prop))
                {
                    nvc.Add(GetKeyValue(propName, value));
                    continue;
                }

                ((IEnumerable)value)
                    .OfType<object>()
                    .ToList()
                    .ForEach(val => nvc.Add(GetKeyValue(propName, val)));
            }
            return nvc;
        }

        private bool IsEnumerableProperty(PropertyInfo propertyInfo)
        {
            Type propType = propertyInfo.PropertyType;
            return typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propType.GetTypeInfo()) && propType != typeof(string);
        }

        private KeyValuePair<string, string> GetKeyValue(string name, object value)
        {
            string val = value.ToString();
            if (value is bool
                || value.GetType().GetTypeInfo().IsEnum)
                val = val.ToLower();
            
            return new KeyValuePair<string, string>(name, val);
        }
    }
}