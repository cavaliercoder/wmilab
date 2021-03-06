﻿/*
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
    using System.Collections.Generic;

    public class PropertyDataValueMap : IDictionary<String, String>
    {
        private PropertyDataValueMap()
        { }

        public PropertyDataValueMap(PropertyData propertyData)
        {
            this.PropertyData = propertyData;

            // Get value map qualifiers
            foreach (var qualifer in propertyData.Qualifiers)
            {
                if (qualifer.Name.Equals("ValuesMap", StringComparison.InvariantCultureIgnoreCase))
                {
                    mapKeys = (String[])qualifer.Value;
                    this.IsKeyValMap = true;
                }
                else if (qualifer.Name.Equals("Values", StringComparison.InvariantCultureIgnoreCase))
                    mapValues = (String[])qualifer.Value;

                else if (qualifer.Name.Equals("BitMap", StringComparison.InvariantCultureIgnoreCase))
                {
                    mapKeys = (String[])qualifer.Value;
                    this.IsKeyValMap = true;
                    this.IsBitMap = true;
                }

                else if (qualifer.Name.Equals("BitValues", StringComparison.InvariantCultureIgnoreCase))
                {
                    mapValues = (String[])qualifer.Value;
                    this.IsBitMap = true;
                }
            }

            // Some properties have an implied key which is the index of the value
            if (!this.IsKeyValMap)
            {
                this.IsKeyValMap = true;
                mapKeys = new String[mapValues.Length];

                // Set the key as the index of the value
                for(int i = 0; i < mapValues.Length; i++)
                    mapKeys[i] = i.ToString();
            }

            // Key and value array length should match
            if (mapKeys.Length != mapValues.Length)
                throw new ArgumentException("Property '{0}' contains an invalid value map", propertyData.Name);
        }

        private String[] mapKeys = {};
        private String[] mapValues = {}; 

        /// <summary>Gets the System.Management.PropertyData object to which this value map belongs.</summary>
        public PropertyData PropertyData
        {
            get; 
            private set; 
        }

        /// <summary>Gets a value indicating whether this value map has keys for each value or simply uses a sequential index.</summary>
        /// <remarks>Classes with a Key/Value map will defines both a 'ValuesMap' and 'AvailableValues' qualifier. If no 'ValuesMap' qualifier is set, the value map uses the zero based index of each value as the key.</remarks>
        public Boolean IsKeyValMap
        {
            get;
            private set;
        }

        public Boolean IsBitMap
        {
            get;
            private set;
        }

        public int IndexOfKey(string key)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.mapKeys[i].Equals(key, StringComparison.InvariantCultureIgnoreCase))
                    return i;
            }

            return -1;
        }

        public void Add(string key, string value)
        {
            throw new InvalidOperationException();
        }

        public bool ContainsKey(string key)
        {
            return this.IndexOfKey(key) >= 0;
        }

        public ICollection<string> Keys
        {
            get { return (ICollection<string>)this.mapKeys; }
        }

        public bool Remove(string key)
        {
            throw new InvalidOperationException();
        }

        public bool TryGetValue(string key, out string value)
        {
            int i = this.IndexOfKey(key);
            if (i < 0)
            {
                value = String.Empty;
                return false;
            }

            value = this.mapValues[i];
            return true;
        }

        public ICollection<string> Values
        {
            get { return (ICollection<string>)this.mapValues; }
        }

        public string this[string key]
        {
            get
            {
                int i = this.IndexOfKey(key);
                if (i < 0)
                    throw new KeyNotFoundException();

                return this.mapValues[i];
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            throw new InvalidOperationException();
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            int i = this.IndexOfKey(item.Key);
            return i > 0 && this.mapValues[i].Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Math.Min(array.Length, this.Count); i++)
            {
                array[i] = new KeyValuePair<string, string>(this.mapKeys[i], this.mapValues[i]);
            }
        }

        public int Count
        {
            get { return this.mapKeys.Length; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw new InvalidOperationException();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        /// <summary>
        /// Returns an empty System.Management.PropertyDataValueMap
        /// </summary>
        public static PropertyDataValueMap Empty
        {
            get { return new PropertyDataValueMap(); }
        }
    }

    public class PropertyDataValueMapCollection : Dictionary<String, PropertyDataValueMap>
    {
        public PropertyDataValueMapCollection(ManagementBaseObject obj)
        {
            this.ManagementObject = obj;

            foreach (var p in obj.Properties)
            {
                this.Add(p.Name, p.GetValueMap());
            }
        }

        public ManagementBaseObject ManagementObject
        {
            get;
            private set;
        }
    }

    public static class PropertyDataValueMapHelper
    {
        public static PropertyDataValueMap GetValueMap(this PropertyData propertyData)
        {
            return new PropertyDataValueMap(propertyData);
        }

        /// <summary>
        /// Returns a named collection of all property maps defined in a ManagementClass.
        /// </summary>
        /// <param name="managementClass">The System.Management.ManagementClass for which all property maps will be returned.</param>
        /// <returns>A System.Management.PropertyDataValueMapCollection for the specified System.Management.ManagementClass.</returns>
        public static PropertyDataValueMapCollection GetValueMaps(this ManagementBaseObject obj)
        {
            return new PropertyDataValueMapCollection(obj);
        }
    }
}
