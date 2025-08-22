export const OPERATOR_OPTIONS = [
	{ value: "eq", text: "Equals", template: "{{field}} eq '{{value}}'" },
	{ value: "ne", text: "Not Equals", template: "{{field}} ne '{{value}}'" },
	{ value: "gt", text: "GreaterThan", template: "{{field}} gt '{{value}}'" },
	{ value: "ge", text: "GreaterThanOrEqual", template: "{{field}} ge '{{value}}'" },
	{ value: "lt", text: "LessThan", template: "{{field}} lt '{{value}}'" },
	{ value: "le", text: "LessThanOrEqual", template: "{{field}} le '{{value}}'" },
	{ value: "contains", text: "Contains", template: "contains({{field}}, '{{value}}')" },
	{ value: "not contains", text: "NotContains", template: "not contains({{field}}, '{{value}}')" },
	{ value: "startswith", text: "StartsWith", template: "startswith({{field}}, '{{value}}')" },
	{ value: "not startswith", text: "NotStartsWith", template: "not startswith({{field}}, '{{value}}')" },
	{ value: "endswith", text: "EndsWith", template: "endswith({{field}}, '{{value}}')" },
	{ value: "not endswith", text: "NotEndsWith", template: "not endswith({{field}}, '{{value}}')" },
];