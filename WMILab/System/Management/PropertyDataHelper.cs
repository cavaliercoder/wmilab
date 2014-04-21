namespace System.Management
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public static class PropertyDataHelper
    {
        /// <summary>
        /// Epoch data used for calculating UInt64 timestamps in WMI.
        /// </summary>
        private static DateTime epoch = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static String[] GetValueAsStringArray(this PropertyData p, PropertyDataValueMap valueMap)
        {
            // Format value as string
            if (p.Value == null)
            {
                return new String[] { String.Empty };
            }

            else
            {
                // Is the value an array?
                if (p.Value.GetType().IsArray)
                {
                    var objects = (IEnumerable)p.Value;
                    var values = new List<String>();
                    foreach (var obj in objects)
                    {
                        values.Add(GetObjectAsString(obj, p, valueMap));
                    }

                    return values.ToArray();
                }

                else
                {
                    return new String[] { GetObjectAsString(p.Value, p, valueMap) };
                }
            }
        }

        public static String[] GetValueAsStringArray(this PropertyData p, PropertyDataValueMapCollection valueMaps)
        {
            if (valueMaps != null && valueMaps.ContainsKey(p.Name))
                return GetValueAsStringArray(p, valueMaps[p.Name]);

            return GetValueAsStringArray(p);
        }

        public static String[] GetValueAsStringArray(this PropertyData p)
        {
            return GetValueAsStringArray(p, PropertyDataValueMap.Empty);
        }

        public static String GetValueAsString(this PropertyData p, PropertyDataValueMap valueMap)
        {
            return String.Join(", ", GetValueAsStringArray(p, valueMap));
        }

        public static String GetValueAsString(this PropertyData p, PropertyDataValueMapCollection valueMaps)
        {
            if (valueMaps != null && valueMaps.ContainsKey(p.Name))
                return GetValueAsString(p, valueMaps[p.Name]);

            return GetValueAsString(p);
        }
        
        public static String GetValueAsString(this PropertyData p)
        {
            return GetValueAsString(p, PropertyDataValueMap.Empty);
        }

        private static String GetObjectAsString(Object obj, PropertyData p, PropertyDataValueMap map)
        {
            if (obj == null)
            {
                return String.Empty;
            }

            // Is a reference to another management class?
            else if (obj.GetType().IsAssignableFrom(typeof(ManagementBaseObject)))
            {
                // Expand object
                return ((ManagementBaseObject)obj).GetRelativePath();
            }

            else if (p.Type == CimType.DateTime)
            {
                DateTime datetime = ManagementDateTimeConverter.ToDateTime(p.Value.ToString());
                return datetime.ToString();
            }

            else if (p.Type == CimType.UInt64 && p.Name == "TIME_CREATED")
            {
                Double ms = ((UInt64)obj) / 10000;
                var datetime = epoch.AddMilliseconds(ms);
                return datetime.ToLocalTime().ToString();
            }

            else if (map != null && obj != null && map.ContainsKey(obj.ToString()))
            {
                return String.Format("{0} ({1})", map[obj.ToString()], obj.ToString());
            }

            else
            {
                // Plain old string!
                return obj.ToString();
            }
        }
    }
}
