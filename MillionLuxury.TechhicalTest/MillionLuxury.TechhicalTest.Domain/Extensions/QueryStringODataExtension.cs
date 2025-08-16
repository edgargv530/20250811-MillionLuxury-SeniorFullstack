using MillionLuxury.TechhicalTest.Domain.Enums;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;
using MillionLuxury.TechhicalTest.Resources.Validations;
using System.Text;
using System.Text.RegularExpressions;

namespace MillionLuxury.TechhicalTest.Domain.Extensions
{
    public static class QueryStringODataExtension
    {
        public static int? CastInt(this string source)
        {
            if (int.TryParse(source, out int result))
            {
                return result;
            }
            return null;
        }

        public static bool? CastBool(this string source)
        {
            if (bool.TryParse(source, out bool result))
            {
                return result;
            }
            return null;
        }

        public static IEnumerable<IQueryFilter> CastQueryFilters(this string source)
        {
            List<IQueryFilter> queryFilters = [];
            ValidateAndThrowIfHasBadFilter(source);
            var expressions = ExtractExpressions(source);

            var totalAnds = expressions.Count(e => e.ToLower().Trim().Equals("and"));
            var totalOrs = expressions.Count(e => e.ToLower().Trim().Equals("or"));

            if (expressions.Count() == 1 && totalAnds == 0 && totalOrs == 0)
            {
                queryFilters.Add(CastQueryFilterSentence(expressions.First()));
                return queryFilters;
            }

            if (totalAnds > 0 && totalOrs > 0)
            {
                int ind = 0;
                foreach (var exp in expressions)
                {
                    List<string> filterPredicates = ["and", "or"];
                    if (!filterPredicates.Contains(exp.ToLower().Trim()))
                    {
                        queryFilters.Add(new QueryFilterComplex()
                        {
                            PredicateType = expressions.Last().Equals(exp) ? FilterPredicateType.None : expressions.ElementAt(ind + 1).Trim().GetFilterPredicate(),
                            Filters = CastQueryFilters(exp)
                        });
                    }
                    ind++;
                }
                return queryFilters;
            }

            queryFilters.Add(new QueryFilterComplex()
            {
                PredicateType = totalAnds > 0 ? FilterPredicateType.And : FilterPredicateType.Or,
                Filters = CastQueryFilterComplex(expressions)
            });
            return queryFilters;
        }

        public static IEnumerable<QueryOrderBy> CastQueryOrderBy(this string source)
        {
            List<QueryOrderBy> orderFields = [];
            var fields = source.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var field in fields)
            {
                var values = field.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (values.Length == 1)
                {
                    orderFields.Add(new QueryOrderBy()
                    {
                        FieldName = values[0],
                        Ascending = true
                    });
                }
                else if (values.Length > 1)
                {
                    orderFields.Add(new QueryOrderBy()
                    {
                        FieldName = values[0],
                        Ascending = !values[1].Equals("DESC", StringComparison.CurrentCultureIgnoreCase)
                    });
                }
            }
            return orderFields;
        }

        private static void ValidateAndThrowIfHasBadFilter(string filter)
        {
            if (filter.Count(f => f.Equals('(')) != filter.Count(f => f.Equals(')')))
            {
                throw new ApplicationException(CommonValidationMessages.ValidationBadQueryStringFilter);
            }
        }

        private static IEnumerable<string> ExtractExpressions(string expression)
        {
            var parts = Regex.Split(expression, @"(?i)\s+(and|or)\s+");
            int totalOpeningParenthesis = 0;
            int totalClosingParenthesis = 0;
            var expressions = new List<string>();
            StringBuilder strExpresion = new();

            foreach (var part in parts)
            {
                strExpresion.Append($"{part} ");
                totalOpeningParenthesis += part.Count(p => p.Equals('('));
                totalClosingParenthesis += part.Count(p => p.Equals(')'));

                if (totalOpeningParenthesis == totalClosingParenthesis)
                {
                    var strSentence = strExpresion.ToString().Trim();
                    if (strSentence.StartsWith('('))
                    {
                        strSentence = strSentence.Remove(0, 1);
                        strSentence = strSentence.Remove(strSentence.Length - 1, 1);
                    }
                    expressions.Add(strSentence);
                    totalOpeningParenthesis = 0;
                    totalClosingParenthesis = 0;
                    strExpresion = new();
                }
            }

            if (expressions.Count() == 1)
            {
                parts = Regex.Split(expression, @"(?i)\s+(and|or)\s+");
                if (parts.Count() > 1)
                {
                    return ExtractExpressions(expressions.First());
                }
            }

            return expressions;
        }

        private static QueryFilterSentence CastQueryFilterSentence(string expression)
        {
            var sentences = MatchLogicalOperators(expression);

            if (!sentences.Any())
            {
                sentences = MatchStringFunctions(expression);
            }

            if (!sentences.Any())
            {
                throw new ApplicationException(CommonValidationMessages.ValidationBadQueryStringFilter);
            }
            return sentences.First();
        }

        private static IEnumerable<QueryFilterSentence> MatchLogicalOperators(string expression)
        {
            var logicalOperatorPattern = @"(?i)(?<field>\w+(?:\.\w+)*)\s+(?<operator>eq|ne|gt|ge|lt|le)\s+'?(?<value>[^']*)'?";
            var sentences = Regex.Matches(expression, logicalOperatorPattern).Cast<Match>().Select(e =>
            {
                var queryFilterSentence = new QueryFilterSentence()
                {
                    FieldName = e.Groups["field"].Value,
                    Operator = CastFilterOperator(e.Groups["operator"].Value, e.Groups["value"].Value),
                    Value = e.Groups["value"].Value
                };

                if (queryFilterSentence.Operator == FilterOperator.IsNull || queryFilterSentence.Operator == FilterOperator.IsNotNull)
                {
                    queryFilterSentence.Value = null!;
                }
                else if (queryFilterSentence.Operator == FilterOperator.IsEmpty || queryFilterSentence.Operator == FilterOperator.IsNotEmpty)
                {
                    queryFilterSentence.Value = string.Empty;
                }

                return queryFilterSentence;
            });
            return sentences;
        }

        private static IEnumerable<QueryFilterSentence> MatchStringFunctions(string expression)
        {
            var stringFunctionPattern = @$"(?i)\b(?<negative>not\s+)?(?<function>startswith|endswith|contains)\((?<field>\w+(?:\.\w+)*),\s*'?(?<value>[^']*)'?\)";
            var sentences = Regex.Matches(expression, stringFunctionPattern).Cast<Match>().Select(e => new QueryFilterSentence()
            {
                FieldName = e.Groups["field"].Value,
                Operator = $"{e.Groups["negative"].Value}{e.Groups["function"].Value}".Trim().GetFilterOperator(),
                Value = e.Groups["value"].Value
            });
            return sentences;
        }

        private static FilterOperator CastFilterOperator(string filterOperator, string value)
        {
            FilterOperator filterOpe = filterOperator.GetFilterOperator();
            if ((filterOpe == FilterOperator.Equal || filterOpe == FilterOperator.NotEqual) && (value is null || string.IsNullOrWhiteSpace(value) || value.Equals("null")))
            {
                filterOpe = $"{filterOperator} {value}".GetFilterOperator();
            }
            return filterOpe;
        }

        private static IEnumerable<IQueryFilter> CastQueryFilterComplex(IEnumerable<string> expressions)
        {
            List<IQueryFilter> queryFilters = [];
            List<string> filterPredicates = ["and", "or"];
            foreach (var exp in expressions)
            {
                if (!filterPredicates.Contains(exp.ToLower().Trim()))
                {
                    queryFilters.AddRange(CastQueryFilters(exp));
                }
            }
            return queryFilters;
        }
    }
}
