import { useRef, useState } from "react";
import { OPERATOR_OPTIONS } from "../../shared/operator-options.js";

export default function GridColumn({ column, filter, onSetFilter, onClearFilter }) {
	const [operator, setOperator] = useState(filter.operator);
	const [value, setValue] = useState(filter.value);

	function handleSetFilter() {
		onSetFilter({
			name: column,
			operator: operator,
			value: value
		});
	}

	function handleClearFilter() {
		setOperator("eq");
		setValue("");
		onClearFilter(column);
	}

	return (
		<div className="d-flex align-items-center justify-content-between">
			<span>{column}</span>
			<div>
				<span className="dropdown-toggle btn btn-outline-dark border-0 customGridMenu" data-bs-toggle="dropdown">
					<i className="fs-6 bi-filter"></i>
				</span>
				<div className="dropdown-menu border-0">
					<div className="px-2 mb-3">
						<select value={operator} onChange={(e) => setOperator(e.target.value)} id="operatorSelect" className="form-select form-select-sm" aria-label="Operators" >
							{OPERATOR_OPTIONS.map((option) => (
								<option key={option.value} value={option.value}>{option.text}</option>))}
						</select>
					</div>
					<div className="input-group-sm px-2 mb-3">
						<input value={value} onChange={(e) => setValue(e.target.value)} type="text" className="form-control" placeholder="Enter the value" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-sm" required />
					</div>
					<div className="text-end">
						<button className="btn btn-primary btn-sm m-1" onClick={handleSetFilter}>Filter</button>
						<button className="btn btn-secondary btn-sm m-1" onClick={handleClearFilter}>Clear</button>
					</div>
				</div>
			</div>
		</div>
	);
}