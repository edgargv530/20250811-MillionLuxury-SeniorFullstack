import { useRef, useState, useEffect } from "react";
import Card from "./Card";

export default function GridHead({ columns, onChangeFilter }) {
	const selectOptionsRef = useRef(null);
	const inputValueRef = useRef(null);
	const [operator, setOperator] = useState("eq");
	const [filterValue, setFilterValue] = useState("");

	const [filters, setFilters] = useState([]);

	const operatorOptions = [
		{ value: "eq", text: "Equals" },
		{ value: "ne", text: "Not Equals" },
		{ value: "gt", text: "GreaterThan" },
		{ value: "ge", text: "GreaterThanOrEqual" },
		{ value: "lt", text: "LessThan" },
		{ value: "le", text: "LessThanOrEqual" },
		{ value: "contains", text: "Contains" },
		{ value: "not contains", text: "NotContains" },
		{ value: "startswith", text: "StartsWith" },
		{ value: "not startswith", text: "NotStartsWith" },
		{ value: "endswith", text: "EndsWith" },
		{ value: "not endswith", text: "NotEndsWith" },
	];


	function handleFilter() {
		//setFilterValue(inputValueRef.current.value);

		//const value = inputValueRef.current.value;
		//const operator = selectOptionsRef.current.value;

		//console.log("Columna:", column);
		console.log("Operador:", selectOptionsRef.current.value);
		console.log("Valor:", inputValueRef.current.value);


		//setFilters((prevFilters) => {
		//	const existingFilterIndex = prevFilters.findIndex(f => f.column === column);
		//	if (existingFilterIndex !== -1) {
		//		const updatedFilters = [...prevFilters];
		//		updatedFilters[existingFilterIndex] = { column, operator, value: filterValue };
		//		return updatedFilters;
		//	} else {
		//		return [...prevFilters, { column, operator, value: filterValue }];
		//	}
		//});
	}

	//useEffect(() => {
	//	console.log("Nuevo filters:", filters);
	//	onChangeFilter(filters);
	//}, [filters]);

	return (
		<thead>
			<tr>
				<th scope="col" className="column-actions">
				</th>
				{columns && columns.map((col, index) => col !== 'Id' && (
					<th key={index} scope="col">
						<div className="d-flex align-items-center justify-content-between">
							<span>{col}</span>
							<div>
								<span className="dropdown-toggle btn btn-outline-dark border-0 customGridMenu" data-bs-toggle="dropdown">
									<i className="fs-6 bi-filter"></i>
								</span>
								<div className="dropdown-menu border-0">
									<Card title={`${col} Filter`} >
										<div className="row">
											<div className="col">
												<select ref={selectOptionsRef} id="operatorSelect" className="form-select form-select-sm" aria-label="Operators">
													{operatorOptions.map((option) => (
														<option key={option.value} value={option.value}>{option.text}</option>))}
												</select>
											</div>
											<div className="col">
												<input ref={inputValueRef} type="text" className="form-control" placeholder="Value" aria-label="Value" />
											</div>
										</div>
										<div className="col-12">
											<button className="btn btn-primary" onClick={handleFilter}>Search</button>
										</div>
									</Card>
								</div>
							</div>
						</div>
					</th>
				))}
			</tr>
		</thead>
	);
}