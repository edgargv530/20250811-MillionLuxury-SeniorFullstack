import { useState } from "react";
import Pagination from "../molecules/Pagination";
import GridHead from "../molecules/GridHead";

export default function Grid({ columns, response, onChangeState }) {
	const [stateGrid, setStateGrid] = useState({
		columns,
		top: response.Top,
		skip: response.Skip,
		filters: [],
		orderBy: null
	});

	const pageNumber = Math.ceil((response.Skip / response.Top) + 1);
	const totalPages = Math.ceil(response.TotalRows / response.Top);

	//let state = {
	//	top: response.Top,
	//	skip: response.Skip,
	//	filter: null,
	//	orderBy: null
	//}

	function handleChangePage(page) {
		stateGrid.skip = (page - 1) * response.Top;
		setStateGrid(stateGrid);
		//console.log('handleChangePage');
		//console.log(stateGrid);
		onChangeState(stateGrid);

	}

	function handleChangeItemsPerPage(itemsPerPage) {
		stateGrid.top = itemsPerPage;
		stateGrid.skip = 0;
		setStateGrid(stateGrid);
		onChangeState(stateGrid);
	}

	function handleChangeFilters(filters) {
		stateGrid.filters = filters;
		//console.log(filters);
		setStateGrid(stateGrid);
		//onChangeState(stateGrid);
	}

	return (
		<div className="table-responsive">
			<table className="table table-bordered table-hover table-striped">
				<caption>
					<Pagination pageNumber={pageNumber} totalPages={totalPages} itemsPerPage={response.Top} onChangePage={handleChangePage} onChangeItemsPerPage={handleChangeItemsPerPage} />
				</caption>
				<GridHead stateGrid={stateGrid} onChangeFilters={handleChangeFilters} />
				<tbody>
					{response.Data && response.Data.map((owner) => (
						<tr key={owner.Id}>
							<td className="column-actions">
								<span className="dropdown-toggle btn btn-outline-dark border-0 customGridMenu" data-bs-toggle="dropdown">
									<i className="fs-6 bi-menu-button-wide-fill"></i>
								</span>
								<ul className="dropdown-menu border-0">
									<li><a className="dropdown-item" href="#">Add</a></li>
									<li><a className="dropdown-item" href="#">Edit</a></li>
									<li><a className="dropdown-item" href="#">Delete</a></li>
								</ul>
							</td>
							{columns && columns.map((col, index) => col !== 'Id' && (<td key={index}>{owner[col]}</td>))}
						</tr>
					))}
				</tbody>
			</table>

		</div>
	);
}