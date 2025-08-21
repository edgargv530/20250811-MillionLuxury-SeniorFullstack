import Pagination from "../molecules/Pagination";
import GridHead from "../molecules/GridHead";

export default function Grid({ columns, response, onChangeState }) {
	const pageNumber = Math.ceil((response.Skip / response.Top) + 1);
	const totalPages = Math.ceil(response.TotalRows / response.Top);

	let state = {
		top: response.Top,
		skip: response.Skip,
		filter: null,
		orderBy: null
	}

	function handleChangePage(page) {
		state.skip = (page - 1) * response.Top;
		onChangeState(state);
	}

	function handleChangeItemsPerPage(itemsPerPage) {
		state.top = itemsPerPage;
		state.skip = 0;
		onChangeState(state);
	}

	function handleChangeFilter(filters) {
		state.filter = filters;
		//console.log(filters);
		onChangeState(state);
	}

	return (
		<div className="table-responsive">
			<table className="table table-bordered table-hover table-striped">
				<caption>
					<Pagination pageNumber={pageNumber} totalPages={totalPages} itemsPerPage={response.Top} onChangePage={handleChangePage} onChangeItemsPerPage={handleChangeItemsPerPage} />
				</caption>
				<GridHead columns={columns} onChangeFilter={handleChangeFilter} />
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