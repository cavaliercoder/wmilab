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
