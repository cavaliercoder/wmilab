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
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public enum WqlQueryType
    {
        Unknown,
        Select,
        AssociatorsOf,
        ReferencesOf
    }

    public static class StringExtensions
    {
        /// <summary>Regex pattern that matches WQL Select queries.</summary>
        private const String SELECT_QUERY_PATTERN = @"^\s*select\s+";

        /// <summary>Regex pattern that matches WQL Association queries.</summary>
        private const String ASSOC_QUERY_PATTERN = @"^\s*associators\s+of\s+";

        /// <summary>Regex patten that matches WQL Reference queries.</summary>
        private const String REF_QUERY_PATTERN = @"^\s*references\s+of\s+";

        private const String AGGEVENT_CLASS = "__AggregateEvent";

        public static WqlQueryType GetWqlQueryType(this String query)
        {
            if(Regex.IsMatch(query, SELECT_QUERY_PATTERN, RegexOptions.IgnoreCase))
                return WqlQueryType.Select;

            if (Regex.IsMatch(query, ASSOC_QUERY_PATTERN , RegexOptions.IgnoreCase))
                return WqlQueryType.AssociatorsOf;

            if (Regex.IsMatch(query, REF_QUERY_PATTERN, RegexOptions.IgnoreCase))
                return WqlQueryType.ReferencesOf;

            return WqlQueryType.Unknown;
        }

        public static Boolean IsWqlSelectQuery(String query)
        {
            return query.GetWqlQueryType() == WqlQueryType.Select;
        }

        public static Boolean IsWqlAssociatorsOfQuery(String query)
        {
            return query.GetWqlQueryType() == WqlQueryType.AssociatorsOf;
        }

        public static Boolean IsWqlReferencesOfQuery(String query)
        {
            return query.GetWqlQueryType() == WqlQueryType.ReferencesOf;
        }

        /// <summary>
        /// Returns a System.Management.PropertyData[] of properties that will be returned by the system for the specified query string.
        /// </summary>
        /// <param name="query">A valid WQL Select query.</param>
        /// <param name="managementClass">A ManagementBaseObject (class or instance) targeted by this query.</param>
        /// <returns>A System.Management.PropertyData[] of properties that will be returned by the system for the specified query string.</returns>
        /// <remarks>
        /// This function is used to return property data for a query that does not return all of the properties in the associated class.
        /// Specifically, it is used to build grid column configuration for results returned by a query.
        /// </remarks>
        public static PropertyData[] GetWqlQueryProperties(this String query, ManagementBaseObject managementClass)
        {
            // Query has been validated
            Boolean queryValidated = false;

            // Specified managementClass is a __AggregatedEvent class
            Boolean classIsAggregatedEvent = managementClass.ClassPath.ClassName.Equals(AGGEVENT_CLASS, StringComparison.InvariantCultureIgnoreCase);

            // Parse the query with .Net's built in parser
            var selectQuery = new SelectQuery(query);

            // Validate an event query
            WqlEventQuery eventQuery;
            if(managementClass.IsEvent() || classIsAggregatedEvent)
            {
                // Parse the event query with .Net's built in parser
                eventQuery = new WqlEventQuery(query);

                // Make sure the return type for an event query with grouped intervals is __AggregateEvent
                if (eventQuery.GroupWithinInterval.Ticks > 0)
                {
                    if (!queryValidated && !managementClass.ClassPath.ClassName.Equals(AGGEVENT_CLASS, StringComparison.InvariantCultureIgnoreCase))
                        throw new ArgumentException(String.Format("Grouped event queries should return type '{0}'. Object passed was a '{1}'.", AGGEVENT_CLASS, managementClass.ClassPath.ClassName));

                    queryValidated = true;
                }
            }

            // Validate class name
            if (!queryValidated && !selectQuery.ClassName.Equals(managementClass.ClassPath.ClassName, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException(String.Format("The class specified in the query '{0}' does not match the class of the specified object '{1}'.", selectQuery.ClassName, managementClass.ClassPath.ClassName));

            // Extract the list of PropertyData
            var properties = new List<PropertyData>();
            if (selectQuery.SelectedProperties.Count == 0)
            {
                // Grab all properties for a SELECT *... query
                foreach (PropertyData p in managementClass.Properties)
                {
                    properties.Add(p);
                }
            }
            else
            {
                // Grab specified properties for a SELECT field1, field2,... query
                foreach (var propertyName in selectQuery.SelectedProperties)
                {
                    properties.Add(managementClass.Properties[propertyName]);
                }
            }

            return properties.ToArray();
        }

        /// <summary>
        /// Returns a System.Management.PropertyData[] of properties that will be returned by the system for the specified query string.
        /// </summary>
        /// <param name="managementClass">A ManagementBaseObject (class or instance) targeted by this query.</param>
        /// <param name="query">A valid WQL Select query.</param>
        /// <returns>A System.Management.PropertyData[] of properties that will be returned by the system for the specified query string.</returns>
        /// <remarks>
        /// This function is used to return property data for a query that does not return all of the properties in the associated class.
        /// Specifically, it is used to build grid column configuration for results returned by a query.
        /// </remarks>
        public static PropertyData[] GetWqlQueryProperties(this ManagementBaseObject managementBaseObject, String query)
        {
            return GetWqlQueryProperties(query, managementBaseObject);
        }
    }
}
