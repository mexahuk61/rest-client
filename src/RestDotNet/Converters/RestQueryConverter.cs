using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace RestDotNet.Converters
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
                if (!IsSerializebleField(prop, value)) continue;
                string propName = prop.Name;

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

        public virtual bool IsSerializebleField(PropertyInfo prop, object value)
        {
            return !prop.GetMethod.IsPrivate 
                && value != null;
        }

        private bool IsEnumerableProperty(PropertyInfo propertyInfo)
        {
            Type propType = propertyInfo.PropertyType;
            return typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(propType.GetTypeInfo()) && propType != typeof(string);
        }

        public virtual KeyValuePair<string, string> GetKeyValue(string propName, object value)
        {
            var name = propName.ToLower();

            string val = value.ToString();
            if (value is bool
                || value.GetType().GetTypeInfo().IsEnum)
                val = val.ToLower();
            
            return new KeyValuePair<string, string>(name, val);
        }
    }
}