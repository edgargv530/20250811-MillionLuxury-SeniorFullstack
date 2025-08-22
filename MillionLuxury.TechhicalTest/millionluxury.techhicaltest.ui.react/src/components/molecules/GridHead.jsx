import { useState } from "react";
import GridColumn from "./GridColumn";

export default function GridHead({ stateGrid, onChangeFilters }) {
	const [filters, setFilters] = useState(stateGrid.filters);
	console.log('GridHead' + JSON.stringify(stateGrid));

	function handleSetFilter(filter) {
		const newFilters = [...filters];
		const index = newFilters.findIndex((f) => f.name === filter.name);

		if (index === -1) {
			newFilters.push(filter);
		} else {
			newFilters[index] = filter;
		}

		setFilters(newFilters);
		onChangeFilters(filters);
	}

	function handleClearFilter(columnName) {
		const newFilters = filters.filter((f) => f.name !== columnName);
		setFilters(newFilters);
		onChangeFilters(filters);
	}

	function getFilterForColumn(columnName) {
		const filter = filters.find((f) => f.name === columnName);
		//console.log(columnName);
		//console.log(filter);
		//console.log('folters');
		//console.log(filters.length);
		//console.log(filters);
		return filter || { name: columnName, operator: 'eq', value: '' };
	}

	return (
		<thead>
			<tr>
				<th scope="col" className="column-actions">
				</th>
				{stateGrid.columns && stateGrid.columns.map((col, index) => col !== 'Id' && (
					<th key={index} scope="col">
						<GridColumn column={col} filter={getFilterForColumn(col)} onSetFilter={handleSetFilter} onClearFilter={handleClearFilter} />
					</th>
				))}
			</tr>
		</thead>
	);
}