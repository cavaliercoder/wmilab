namespace System.Management
{
    using System;
    using System.Text;

    public static class PropertyDataHelper
    {
        /// <summary>
        /// Epoch data used for calculating UInt64 timestamps in WMI.
        /// </summary>
        private static DateTime epoch = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string GetValueAsString(this PropertyData p, PropertyDataValueMap valueMap)
        {
            // Format value as string
            if (p.Value == null)
            {
                return String.Empty;
            }

            else
            {
                // Is the value an array?
                if (p.Value.GetType().IsArray)
                {
                    // Expand Byte Array
                    if (p.Value.GetType().IsAssignableFrom(typeof(Byte)))
                    {
                        var bytes = (Byte[])p.Value;
                        var count = Math.Min(bytes.Length, 8);
                        var sb = new StringBuilder();

                        sb.Append("[");
                        for (int i = 0; i < count; i++)
                        {
                            if (i > 0)
                                sb.Append(", ");
                            sb.AppendFormat("0x{0:X}", (Byte)bytes[i]);
                        }

                        if (bytes.Length > count)
                            sb.Append(", ...");

                        sb.Append("]");
                        return sb.ToString();
                    }

                    // Expand object array
                    Object[] objects;
                    try
                    {
                        objects = (Object[])p.Value;
                    }

                    catch (InvalidCastException)
                    {
                        return (p.Value.GetType().Name);
                    }

                    string[] values = new string[objects.Length];
                    for (int i = 0; i < objects.Length; i++)
                    {
                        values[i] = objects[i].ToString();
                    }

                    return String.Join(", ", values);
                }

                else
                {
                    // Is a reference to another management class?
                    if (p.Value.GetType().IsAssignableFrom(typeof(ManagementBaseObject)))
                    {
                        // Expand object
                        return ((ManagementBaseObject)p.Value).GetRelativePath();
                    }

                    else if (p.Type == CimType.DateTime)
                    {
                        DateTime datetime = ManagementDateTimeConverter.ToDateTime(p.Value.ToString());
                        return datetime.ToString();
                    }

                    else if (p.Type == CimType.UInt64 && p.Name == "TIME_CREATED")
                    {
                        Double ms = ((UInt64)p.Value) / 10000;
                        var datetime = epoch.AddMilliseconds(ms);
                        return datetime.ToLocalTime().ToString();
                    }

                    else
                    {
                        // Plain old string!
                        return p.Value.ToString();
                    }
                }
            }
        }
        
        public static String GetValueAsString(this PropertyData p)
        {
            return GetValueAsString(p, null);
        }
    }
}
