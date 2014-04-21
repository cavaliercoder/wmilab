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
    public enum WqlQueryType
    {
        Unknown,
        Select,
        AssociatorsOf,
        ReferencesOf
    }

    public static class WqlStringExtensions
    {
        public static WqlQueryType GetWqlQueryType(this String query)
        {
            String lc = query.ToLowerInvariant();

            if (lc.StartsWith("select "))
                return WqlQueryType.Select;

            if (lc.StartsWith("associators of "))
                return WqlQueryType.AssociatorsOf;

            if (lc.StartsWith("references of "))
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
    }
}
