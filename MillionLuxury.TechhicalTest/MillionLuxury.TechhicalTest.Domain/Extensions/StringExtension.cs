using MillionLuxury.TechhicalTest.Domain.Enums;
using System.Text;
using System.Text.RegularExpressions;

namespace MillionLuxury.TechhicalTest.Domain.Extensions
{
    public static class StringExtension
    {
        public static FilterOperator GetFilterOperator(this string source)
        {
            var filterOperator = source.ToLower() switch
            {
                "eq" or "equal" => FilterOperator.Equal,
                "ne" or "notequal" => FilterOperator.NotEqual,
                "gt" or "greaterthan" => FilterOperator.GreaterThan,
                "ge" or "greaterthanorequal" => FilterOperator.GreaterThanOrEqual,
                "lt" or "lessthan" => FilterOperator.LessThan,
                "le" or "lessthanorequal" => FilterOperator.LessThanOrEqual,
                "contains" or "like" => FilterOperator.Contains,
                "not contains" or "doesnotcontain" => FilterOperator.NotContains,
                "startswith" => FilterOperator.StartsWith,
                "not startswith" or "doesnotstartwith" => FilterOperator.NotStartsWith,
                "endswith" => FilterOperator.EndsWith,
                "not endswith" or "doesnotendwith" => FilterOperator.NotEndsWith,
                "eq " or "isempty" => FilterOperator.IsEmpty,
                "ne " or "isnotempty" => FilterOperator.IsNotEmpty,
                "eq null" or "isnull" => FilterOperator.IsNull,
                "ne null" or "isnotnull" => FilterOperator.IsNotNull,
                _ => FilterOperator.None,
            };
            return filterOperator;
        }

        public static FilterPredicateType GetFilterPredicate(this string value)
        {
            FilterPredicateType filterPredicate = FilterPredicateType.None;
            if (string.IsNullOrEmpty(value))
            {
                return filterPredicate;
            }

            filterPredicate = value.ToUpper() switch
            {
                "AND" => FilterPredicateType.And,
                "OR" => FilterPredicateType.Or,
                _ => FilterPredicateType.None,
            };
            return filterPredicate;
        }

        public static string FormatSql(this string source)
        {
            var sql = new StringBuilder();
            foreach (var line in source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                string patron = "(?i)^(SELECT|FROM|INNER|LEFT|RIGHT|JOIN|WHERE|ORDER BY|OFFSET)";

                Regex regex = new(patron);
                Match match = regex.Match(line);

                if (match.Success)
                {
                    sql.AppendLine(line);
                }
                else
                {
                    sql.AppendLine($"\t{line}");
                }
            }
            return sql.ToString();
        }
    }
}
