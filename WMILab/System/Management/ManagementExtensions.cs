/*
 * Copyright (c) 2014 Ryan Armstrong (www.cavaliercoder.com)
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to
 * deal in the Software without restriction, including without limitation the
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
 * sell copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * The Software shall be used for Good, not Evil.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */
namespace System.Management
{
    using System;

    public static class ManagementExtensions
    {
        #region CimType

        public static Boolean IsNumeric(this CimType cimType)
        {
            return IsRealNumeric(cimType) || IsInteger(cimType);
        }

        public static Boolean IsRealNumeric(this CimType cimType)
        {
            return cimType == (cimType & (CimType.Real32 | CimType.Real64));
        }

        public static Boolean IsInteger(this CimType cimType)
        {
            return cimType == (cimType & (
                CimType.UInt8 | CimType.UInt16 | CimType.UInt32 | CimType.UInt64 |
                CimType.SInt8 | CimType.SInt16 | CimType.SInt32 | CimType.SInt64));
        }

        #endregion

        #region QualifierDataCollection

        public static Boolean Exists(this QualifierDataCollection qualifiers, string qualifier)
        {
            foreach (QualifierData q in qualifiers)
            {
                if (q.Name.Equals(qualifier, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        #endregion

        #region ManagementBaseObject

        /// <summary>
        /// Returns a System.String representation of the specified ManagementBaseObjects instance.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementBaseObject to be assessed.</param>
        /// <returns>A System.String representation of the specified ManagementBaseObjects instance.</returns>
        public static string GetRelativePath(this ManagementBaseObject obj)
        {
            if (obj.SystemProperties["__RELPATH"].Value != null)
                return obj.SystemProperties["__RELPATH"].Value.ToString();
            return obj.ClassPath.RelativePath;
        }

        public static Boolean HasProperty(this ManagementBaseObject obj, String name)
        {
            return obj.Properties.Contains(name);
        }

        /// <summary>
        /// Return true if the specified qualifier name exists in the specified ManagementBaseObject's qualifier collection.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementBaseObject to be assessed.</param>
        /// <param name="qualifier">The name of the qualifier to be search for.</param>
        /// <returns>True if the specified qualifier name exists in the specified ManagementBaseObject's qualifier collection.</returns>
        public static Boolean HasQualifier(this ManagementBaseObject obj, string qualifier)
        {
            return Exists(obj.Qualifiers, qualifier);
        }

        public static Boolean DerivesFrom(this ManagementBaseObject child, string ancestor)
        {
            string[] derivations = (string[])child.SystemProperties["__DERIVATION"].Value;
            foreach (string test in derivations)
            {
                if (test.Equals(ancestor, StringComparison.InvariantCultureIgnoreCase)) return true;
            }

            return false;
        }

        public static Boolean DerivesFrom(this ManagementBaseObject child, ManagementBaseObject ancestorClassName)
        {
            return DerivesFrom(child, ancestorClassName.ClassPath.ClassName);
        }
        
        /// <summary>
        /// Returns true if the specified ManagementBaseObject is a system class.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementBaseObject to be assessed.</param>
        /// <returns>True if the ManagementBaseObject is a system class.</returns>
        public static Boolean IsSystemClass(this ManagementBaseObject obj)
        {
            return (!String.IsNullOrEmpty(obj.ClassPath.ClassName) && obj.ClassPath.ClassName.StartsWith("__"));
        }

        public static Boolean IsAssociation(this ManagementBaseObject obj)
        {
            return (HasQualifier(obj, "Association") && (Boolean)obj.Qualifiers["Association"].Value == true);
        }

        /// <summary>
        /// Returns true if the specified ManagementBaseObject is an event.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementBaseObject to be assessed.</param>
        /// <returns>True if the ManagementBaseObject is an event.</returns>
        public static Boolean IsEvent(this ManagementBaseObject obj)
        {
            return DerivesFrom(obj, "__event");
        }

        public static Boolean IsSingleton(this ManagementBaseObject obj)
        {
            return HasQualifier(obj, "Singleton");
        }

        public static Boolean IsPerformanceCounter(this ManagementBaseObject obj)
        {
            return DerivesFrom(obj, "Win32_Perf");
        }

        /// <summary>
        /// Return the System.String description of the specified ManagementBaseObject.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementBaseObject for which the description will be returned.</param>
        /// <returns>The System.String description of the specified ManagementBaseObject.</returns>
        public static String GetDescription(this ManagementBaseObject obj)
        {
            String description = (obj.HasQualifier("Description")) ? (String) obj.Qualifiers["Description"].Value : String.Empty;
            
            // TODO: Fix line endings
            // description = Regex.Replace(description, "[^\r]\n", "\r\n");

            return description;
        }

        /// <summary>
        /// Gets a WQL query string to perform as basic query of the specified WMI object.
        /// </summary>
        /// <param name="managementClass">The WMI object for which a basic query will be returned.</param>
        /// <returns>A WQL query string to perform as basic query of the specified WMI object.</returns>
        public static String GetDefaultQuery(this ManagementBaseObject obj)
        {
            if (obj.IsEvent())
            {
                return String.Format("SELECT * FROM {0} WITHIN 5", obj.ClassPath.ClassName);
            }

            else
            {
                return String.Format("SELECT * FROM {0}", obj.ClassPath.ClassName);
            }
        }

        #endregion

        #region MethodData

        public static Boolean HasQualifier(this MethodData method, String qualifier)
        {
            return method.Qualifiers.Exists(qualifier);
        }

        public static Boolean IsStatic(this MethodData method)
        {
            return HasQualifier(method, "Static");
        }

        public static PropertyData GetReturnValueParameter(this MethodData method)
        {
            foreach (PropertyData parm in method.OutParameters.Properties)
            {
                if (parm.Name.Equals("ReturnValue"))
                    return parm;
            }

            return null;
        }

        public static String GetDescription(this MethodData method)
        {
            return method.HasQualifier("Description") ?
                method.Qualifiers["Description"].Value.ToString() :
                String.Empty;
        }

        #endregion

        #region PropertyData

        private static Boolean HasQualifier(this PropertyData property, String qualifier)
        {
            return Exists(property.Qualifiers, qualifier);
        }

        public static Boolean IsKey(this PropertyData property)
        {
            return property.HasQualifier("key");
        }

        public static Boolean IsReadable(this PropertyData property)
        {
            return property.HasQualifier("read");
        }

        public static Boolean IsWritable(this PropertyData property)
        {
            return property.HasQualifier("write");
        }

        public static Boolean IsNumeric(this PropertyData property)
        {
            return IsNumeric(property.Type);
        }

        public static Boolean IsRealNumeric(this PropertyData property)
        {
            return IsRealNumeric(property.Type);
        }

        public static Boolean IsInteger(this PropertyData property)
        {
            return IsInteger(property.Type);
        }

        public static String GetDescription(this PropertyData property)
        {
            return property.HasQualifier("Description") ?
                property.Qualifiers["Description"].Value.ToString() :
                String.Empty;
        }

        public static String GetRefType(this PropertyData property)
        {
            if (property.HasQualifier("CIMTYPE"))
            {
                String[] cimtype = property.Qualifiers["CIMTYPE"].Value.ToString().Split(':');
                if (cimtype.Length == 2)
                    return cimtype[1];
            }

            return String.Empty;
        }

        public static Boolean Contains(this PropertyDataCollection properties, String name)
        {
            foreach (var p in properties)
                if (p.Name == name)
                    return true;

            return false;
        }

        #endregion
    }
}
